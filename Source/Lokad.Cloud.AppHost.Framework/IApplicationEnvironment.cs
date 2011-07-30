#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System.Security.Cryptography.X509Certificates;

namespace Lokad.Cloud.AppHost.Framework
{
    public interface IApplicationEnvironment
    {
        string MachineName { get; }
        string CellName { get; }

        string CurrentDeploymentName { get; }
        string CurrentAssembliesName { get; }
        void LoadDeployment(string deploymentName);
        void LoadCurrentHeadDeployment();

        int CurrentWorkerInstanceCount { get; }
        void ProvisionWorkerInstances(int numberOfInstances);
        void ProvisionWorkerInstancesAtLeast(int minNumberOfInstances);

        string GetSettingValue(string settingName);
        X509Certificate2 GetCertificate(string thumbprint);
        string GetLocalResourcePath(string resourceName);

        void SendCommand(IHostCommand command);
    }
}
