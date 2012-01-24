#region Copyright (c) Lokad 2009-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lokad.Cloud.AppHost.Framework;
using Lokad.Cloud.AppHost.Framework.Commands;
using Lokad.Cloud.AppHost.Framework.Definition;
using Lokad.Cloud.AppHost.Framework.Instrumentation.Events;
using Lokad.Cloud.AppHost.Util;

namespace Lokad.Cloud.AppHost
{
    public sealed class Host
    {
        private readonly IHostContext _hostContext;
        private readonly Dictionary<string, Cell> _cells;
        private readonly BlockingCollection<IHostCommand> _commandQueue;

        private readonly int _autoLoadHeadDeploymentIntervalMs;
        private readonly Timer _autoLoadHeadDeploymentTimer;

        private string _currentDeploymentEtag;
        private SolutionHead _currentDeployment;
        private SolutionDefinition _currentSolution;

        /// <param name="autoLoadHeadDeploymentIntervalMs">AutoLoad is disabled if set to Timeout.Infinite</param>
        public Host(IHostContext context, int autoLoadHeadDeploymentIntervalMs = 30000)
        {
            _hostContext = context;
            _cells = new Dictionary<string, Cell>();
            _commandQueue = new BlockingCollection<IHostCommand>();

            _autoLoadHeadDeploymentIntervalMs = autoLoadHeadDeploymentIntervalMs;
            _autoLoadHeadDeploymentTimer = new Timer(o => _commandQueue.Add(new LoadCurrentHeadDeploymentCommand()), null, Timeout.Infinite, _autoLoadHeadDeploymentIntervalMs);
        }

        public void RunSync(CancellationToken cancellationToken)
        {
            _hostContext.Observer.TryNotify(() => new HostStartedEvent(_hostContext.Identity));

            try
            {
                _currentDeployment = null;
                _currentSolution = null;
                _currentDeploymentEtag = null;

                _autoLoadHeadDeploymentTimer.Change(0, _autoLoadHeadDeploymentIntervalMs);

                foreach (var command in _commandQueue.GetConsumingEnumerable(cancellationToken))
                {
                    Do((dynamic)command, cancellationToken);
                }
            }
            finally
            {
                _autoLoadHeadDeploymentTimer.Change(Timeout.Infinite, _autoLoadHeadDeploymentIntervalMs);
                _hostContext.Observer.TryNotify(() => new HostStoppedEvent(_hostContext.Identity));
            }
        }

        public Task Run(CancellationToken cancellationToken)
        {
            var completionSource = new TaskCompletionSource<object>();

            var thread = new Thread(() =>
                {
                    try
                    {
                        RunSync(cancellationToken);
                        completionSource.TrySetResult(null);
                    }
                    catch (ThreadAbortException)
                    {
                        Thread.ResetAbort();
                        completionSource.TrySetCanceled();
                    }
                    catch (Exception exception)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            // assuming the exception was caused by the cancellation
                            completionSource.TrySetCanceled();
                        }
                        else
                        {
                            completionSource.TrySetException(exception);
                        }
                    }
                })
                {
                    Name = "Lokad.Cloud AppHost Main"
                };

            thread.Start();

            return completionSource.Task;
        }

        public void LoadHeadDeployment()
        {
            _commandQueue.Add(new LoadCurrentHeadDeploymentCommand());
        }

        // Greatly simplified command handling for internal use only, can easily be refactored later if necessary
        // (pattern mainly used for simpler handling and queueing, not for cqs/cqrs-like ideas)

        void Do(LoadCurrentHeadDeploymentCommand command, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            string newEtag;
            var newDeployment = _hostContext.DeploymentReader.GetDeploymentIfModified(_currentDeploymentEtag, out newEtag);
            if (newDeployment == null || newEtag == _currentDeploymentEtag)
            {
                return;
            }

            var newSolution = _hostContext.DeploymentReader.GetSolution(newDeployment);
            if (newSolution == null)
            {
                // TODO: NOTIFY/LOG invalid deployment
                return;
            }

            if (_currentSolution == null || !_currentSolution.Equals(newSolution))
            {
                _currentDeploymentEtag = null;
                _hostContext.Observer.TryNotify(() => new NewDeploymentDetectedEvent(_hostContext.Identity, newDeployment, newSolution));
                OnDeploymentChanged(newDeployment, newSolution, cancellationToken);
                _currentDeploymentEtag = newEtag;
            }
        }

        void Do(LoadDeploymentCommand command, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (_currentDeployment != null && _currentDeployment.Equals(command.Deployment))
            {
                // already on requested deployment
                return;
            }

            var newSolution = _hostContext.DeploymentReader.GetSolution(command.Deployment);
            if (newSolution == null)
            {
                // TODO: NOTIFY/LOG invalid deployment
                return;
            }

            if (_currentSolution == null || !_currentSolution.Equals(newSolution))
            {
                _currentDeploymentEtag = null;
                _hostContext.Observer.TryNotify(() => new NewDeploymentDetectedEvent(_hostContext.Identity, command.Deployment, newSolution));
                OnDeploymentChanged(command.Deployment, newSolution, cancellationToken);
            }
        }

        void OnDeploymentChanged(SolutionHead newDeployment, SolutionDefinition newSolution, CancellationToken cancellationToken)
        {
            // 0. ANALYZE CELL LAYOUT CHANGES

            var removed = new Dictionary<string, Cell>(_cells);
            var added = new List<CellDefinition>();
            var remaining = new List<CellDefinition>();

            Dictionary<string, CellDefinition> old;
            if (_currentSolution == null || _currentSolution.SolutionName != newSolution.SolutionName)
            {
                // we do not reuse cells in completely unrelated solutions (i.e. solution name changed)
                _hostContext.Observer.TryNotify(() => new NewUnrelatedSolutionDetectedEvent(_hostContext.Identity, newSolution));
                old = new Dictionary<string, CellDefinition>();
                added.AddRange(newSolution.Cells);
            }
            else
            {
                // keep remaining cells (only touch cells that actually change in some way)
                old = _currentSolution.Cells.ToDictionary(cellDefinition => cellDefinition.CellName);
                foreach (var newCellDefinition in newSolution.Cells)
                {
                    if (old.ContainsKey(newCellDefinition.CellName))
                    {
                        removed.Remove(newCellDefinition.CellName);
                        remaining.Add(newCellDefinition);
                    }
                    else
                    {
                        added.Add(newCellDefinition);
                    }
                }
            }

            // 1. UPDATE

            _currentSolution = newSolution;
            _currentDeployment = newDeployment;

            // 2. REMOVE CELLS NO LONGER PRESENT

            foreach (var cell in removed)
            {
                _cells.Remove(cell.Key);
                cell.Value.Cancel();
            }
            //Task.WaitAll(removed.Select(c => c.Value.Task).ToArray());

            // 3. UPDATE CELLS STILL PRESENT

            foreach (var newCellDefinition in remaining)
            {
                var cellName = newCellDefinition.CellName;
                var oldCellDefinition = old[cellName];
                if (!newCellDefinition.Equals(oldCellDefinition))
                {
                    _cells[cellName].OnCellDefinitionChanged(newCellDefinition, newDeployment);
                }
            }

            // 4. ADD NEW CELLS

            foreach (var cellDefinition in added)
            {
                var cellName = cellDefinition.CellName;
                _cells.Add(cellName, Cell.Run(_hostContext, _commandQueue.Add, cellDefinition, newDeployment, newSolution.SolutionName, cancellationToken));
            }
        }
    }
}
