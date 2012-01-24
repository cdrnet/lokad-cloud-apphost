#region Copyright (c) Lokad 2011-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System.Collections.Generic;
using Lokad.Cloud.AppHost.Framework.Definition;

namespace Lokad.Cloud.AppHost.Framework
{
    /// <summary>
    /// <para>
    /// Deployment reader abstracts all storage access by the application host for polling
    /// and loading application deployments. An instances of the provided class will be
    /// forwarded to your application's EntryPoint, so you can extend the class freely
    /// to provide additional features. However, it will be serialized and then
    /// deserialized again before it reaches the EntryPoint, in order to be able to cross
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
        SolutionHead GetDeploymentIfModified(string knownETag, out string newETag);
        SolutionDefinition GetSolution(SolutionHead deployment);
        IEnumerable<AssemblyData> GetAssembliesAndSymbols(AssembliesHead assemblies);

        /// <summary>
        /// Expected to support at least byte[] and XElement for T.
        /// </summary>
        T GetItem<T>(string itemName) where T : class;
    }
}
