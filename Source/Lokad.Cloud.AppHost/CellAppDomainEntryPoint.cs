#region Copyright (c) Lokad 2009-2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Threading;
using System.Xml.Linq;
using Lokad.Cloud.AppHost.AssembyLoading;
using Lokad.Cloud.AppHost.Framework;
using Lokad.Cloud.AppHost.Util;

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
        public void Run(string cellDefinitionXml, IDeploymentReader deploymentReader, ApplicationEnvironment environment)
        {
            var cellDefinition = XElement.Parse(cellDefinitionXml);

            // Load Assemblies into AppDomain
            var loader = new AssemblyLoader();
            var assemblies = deploymentReader.GetAssembliesAndSymbols(cellDefinition.SettingsElementAttributeValue("Assemblies", "name"));
            loader.LoadAssembliesIntoAppDomain(assemblies);

            // Create the EntryPoint
            var entryPointTypeName = cellDefinition.SettingsElementAttributeValue("EntryPoint", "typeName");
            var entryPointType = string.IsNullOrEmpty(entryPointTypeName) ? Type.GetType("Lokad.Cloud.Services.AppEntryPoint.EntryPoint, Lokad.Cloud.Services.AppEntryPoint") : Type.GetType(entryPointTypeName);
            _appEntryPoint = (IApplicationEntryPoint)Activator.CreateInstance(entryPointType);

            // Run
            _appEntryPoint.Run((cellDefinition.SettingsElement("Settings") ?? new XElement("Settings")), deploymentReader, environment, _externalCancellationTokenSource.Token);
        }

        public void Cancel()
        {
            _externalCancellationTokenSource.Cancel();
        }

        public void AppplyChangedSettings(string settingsXml)
        {
            _appEntryPoint.ApplyChangedSettings(XElement.Parse(settingsXml));
        }
    }
}