#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Lokad.Cloud.AppHost.Framework
{
    /// <summary>
    /// <para>
    /// Deployment reader abstracts all storage access by the application host for polling
    /// and loading application deployments. An instances of the provided class will be
    /// forwarded to your application's CellRunner, so you can extend the class freely
    /// to provide additional features. However, it will be serialized and then
    /// deserialized again before it reaches CellRunner, in order to be able to cross
    /// AppDomains.
    /// </para>
    /// <para>
    /// All implementations are required to be serializable.
    /// Do NOT derive from MarshalByRefObject.
    /// Remember that Azure SDK Credential and Account classes are NOT serializable.
    /// </para>
    /// <para>
    /// For convenience, the Lokad.Cloud Services Framework provides a complete
    /// implementation based on Lokad.Cloud.Storage.
    /// </para>
    /// </summary>
    /// <remarks>IMPORTANT: Implementations must be [Serializable].</remarks>
    public interface IDeploymentReader
    {
        XElement GetHeadIfModified(string knownETag, out string newETag);
        XElement GetDeployment(string deploymentName);
        IEnumerable<Tuple<string, byte[]>> GetAssembliesAndSymbols(string assembliesName);

        /// <summary>
        /// Expected to support at least byte[] and XElement for T.
        /// </summary>
        T GetItem<T>(string itemName) where T : class;
    }
}
