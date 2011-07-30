#region Copyright (c) Lokad 2009-2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Security.Cryptography.X509Certificates;
using Lokad.Cloud.AppHost.Framework;
using Lokad.Cloud.AppHost.Framework.Commands;

namespace Lokad.Cloud.AppHost
{
    /// <remarks>This class need to be able to cross AppDomains by reference, so all method arguments need to be serializable!</remarks>
    internal class ApplicationEnvironment : MarshalByRefObject, IApplicationEnvironment
    {
        private readonly IHostContext _hostContext;
        private readonly CellHandle _cellHandle;
        private readonly Action<IHostCommand> _sendCommand;

        internal ApplicationEnvironment(IHostContext hostContext, CellHandle cellHandle, Action<IHostCommand> sendCommand)
        {
            _hostContext = hostContext;
            _cellHandle = cellHandle;
            _sendCommand = sendCommand;
        }

        public string MachineName
        {
            get { return _cellHandle.MachineName.Value; }
        }

        public string CellName
        {
            get { return _cellHandle.CellName; }
        }

        public string CurrentDeploymentName
        {
            get { return _cellHandle.CurrentDeploymentName; }
        }

        public string CurrentAssembliesName
        {
            get { return _cellHandle.CurretAssembliesName; }
        }

        public void LoadDeployment(string deploymentName)
        {
            _sendCommand(new LoadDeploymentCommand(deploymentName));
        }

        public void LoadCurrentHeadDeployment()
        {
            _sendCommand(new LoadCurrentHeadDeploymentCommand());
        }

        public int CurrentWorkerInstanceCount
        {
            get { return _hostContext.CurrentWorkerInstanceCount; }
        }

        public void ProvisionWorkerInstances(int numberOfInstances)
        {
            _sendCommand(new ProvisionWorkerInstancesCommand(numberOfInstances));
        }

        public void ProvisionWorkerInstancesAtLeast(int minNumberOfInstances)
        {
            _sendCommand(new ProvisionWorkerInstancesAtLeastCommand(minNumberOfInstances));
        }

        public string GetConfigurationSettingValue(string settingName)
        {
            return _hostContext.GetConfigurationSettingValue(settingName);
        }

        public X509Certificate2 GetConfigurationCertificate(string thumbprint)
        {
            return _hostContext.GetConfigurationCertificate(thumbprint);
        }

        public void SendCommand(IHostCommand command)
        {
            _sendCommand(command);
        }
    }
}
