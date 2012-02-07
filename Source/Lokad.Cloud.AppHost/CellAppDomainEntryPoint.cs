#region Copyright (c) Lokad 2009-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Lokad.Cloud.AppHost.AssembyLoading;
using Lokad.Cloud.AppHost.Framework;
using Lokad.Cloud.AppHost.Framework.Definition;

namespace Lokad.Cloud.AppHost
{
    /// <summary>
    /// AppDomain Entry Point for the cell process (single use).
    /// </summary>
    internal sealed class CellAppDomainEntryPoint : MarshalByRefObject
    {
        private readonly CancellationTokenSource _externalCancellationTokenSource = new CancellationTokenSource();
        private IApplicationEntryPoint _appEntryPoint;

        /// <remarks>Never run a cell process entry point more than once per AppDomain.</remarks>
        public void Run(CellDefinition cellDefinition, IDeploymentReader deploymentReader, ApplicationEnvironment environment)
        {
            // Load Assemblies into AppDomain
            var assemblies = deploymentReader.GetAssembliesAndSymbols(cellDefinition.Assemblies).ToList();
            var loader = new AssemblyLoader();
            loader.LoadAssembliesIntoAppDomain(assemblies, environment);

            // Create the EntryPoint
            var entryPointTypeName = cellDefinition.EntryPointTypeName;
            if (string.IsNullOrEmpty(entryPointTypeName))
            {
                entryPointTypeName = "Lokad.Cloud.Services.AppEntryPoint.EntryPoint, Lokad.Cloud.Services.AppEntryPoint";
            }

            var entryPointType = Type.GetType(entryPointTypeName);
            if (entryPointType == null)
            {
                throw new InvalidOperationException("Type " + entryPointTypeName + " not found.");
            }

            _appEntryPoint = (IApplicationEntryPoint)Activator.CreateInstance(entryPointType);

            var settings = string.IsNullOrEmpty(cellDefinition.SettingsXml) ? new XElement("Settings") : XElement.Parse(cellDefinition.SettingsXml);

            // Run
            _appEntryPoint.Run(settings, deploymentReader, environment, _externalCancellationTokenSource.Token);
        }

        public void Cancel()
        {
            _externalCancellationTokenSource.Cancel();
        }

        public void AppplyChangedSettings(string settingsXml)
        {
            _appEntryPoint.OnSettingsChanged(XElement.Parse(settingsXml));
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}