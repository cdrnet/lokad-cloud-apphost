#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System.Security.Cryptography.X509Certificates;

namespace Lokad.Cloud.AppHost.Framework
{
    /// <summary>
    /// Provides the host with access to its environment (i.e. the RoleEnvironment in Windows Azure),
    /// and most importantly with a reader to poll and read deployments from some arbitrary storage provider.
    /// </summary>
    /// <remarks>Implementation does not need to be serializable (other than the deployment reader).</remarks>
    public interface IHostContext
    {
        IDeploymentReader DeploymentReader { get; }

        /// <remarks>Can be <c>null</c>.</remarks>
        IHostObserver Observer { get; }

        int CurrentWorkerInstanceCount { get; }
        string GetConfigurationSettingValue(string settingName);
        X509Certificate2 GetConfigurationCertificate(string thumbprint);
    }
}
