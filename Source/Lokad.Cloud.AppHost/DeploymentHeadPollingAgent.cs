#region Copyright (c) Lokad 2009-2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using Lokad.Cloud.AppHost.Framework;
using Lokad.Cloud.AppHost.Framework.Commands;
using Lokad.Cloud.AppHost.Framework.Definition;

namespace Lokad.Cloud.AppHost
{
    internal class DeploymentHeadPollingAgent
    {
        private readonly IDeploymentReader _deploymentReader;

        private readonly Action<IHostCommand> _sendCommand;
        private SolutionHead _knownHead;
        private string _knownHeadEtag;

        public DeploymentHeadPollingAgent(IDeploymentReader deploymentReader, Action<IHostCommand> sendCommand)
        {
            _deploymentReader = deploymentReader;
            _sendCommand = sendCommand;
        }

        public void PollForChanges(SolutionHead currentlyLoadedDeoplyment = null)
        {
            string newEtag;
            var head = _deploymentReader.GetHeadIfModified(_knownHeadEtag, out newEtag);
            if (head == null)
            {
                if (_knownHead != null && currentlyLoadedDeoplyment != null && _knownHead.SolutionId != currentlyLoadedDeoplyment.SolutionId)
                {
                    // HEAD has not changed (or is missing), yet the provided current deployment
                    // doesn't match the HEAD as we know it and have last seen it -> LOAD
                    _sendCommand(new LoadDeploymentCommand(_knownHead));
                }

                return;
            }

            _knownHeadEtag = newEtag;
            _knownHead = head;

            if (currentlyLoadedDeoplyment == null || currentlyLoadedDeoplyment.SolutionId != head.SolutionId)
            {
                _sendCommand(new LoadDeploymentCommand(head));
            }
        }
    }
}
