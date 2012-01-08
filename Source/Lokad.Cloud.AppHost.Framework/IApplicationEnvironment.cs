#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System.Net;
using System.Security.Cryptography.X509Certificates;
using Lokad.Cloud.AppHost.Framework.Definition;

namespace Lokad.Cloud.AppHost.Framework
{
    public interface IApplicationEnvironment
    {
        string WorkerName { get; }
        string UniqueWorkerInstanceName { get; }

        string SolutionName { get; }
        string CellName { get; }
        string UniqueCellInstanceName { get; }

        SolutionHead CurrentDeployment { get;  }
        AssembliesHead CurrentAssemblies { get; }
        void LoadDeployment(SolutionHead deployment);
        void LoadCurrentHeadDeployment();

        int CurrentWorkerInstanceCount { get; }
        void ProvisionWorkerInstances(int numberOfInstances);
        void ProvisionWorkerInstancesAtLeast(int minNumberOfInstances);

        string GetSettingValue(string settingName);
        X509Certificate2 GetCertificate(string thumbprint);
        string GetLocalResourcePath(string resourceName);

        IPEndPoint GetEndpoint(string endpointName);

        void SendCommand(IHostCommand command);
    }
}
