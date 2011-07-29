#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System.Threading;
using System.Xml.Linq;

namespace Lokad.Cloud.AppHost.Framework
{
    /// <summary>
    /// This is your application's entry point, where you'd usually set up and build
    /// your IoC container, resolve your application and then run it. All your assemblies
    /// are already loaded into the AppDomain at the point where the CellRunner is activated.
    /// </summary>
    /// <remarks>Must have a public constructor with not arguments.</remarks>
    public interface ICellRunner
    {
        /// <summary>
        /// Run is expected to block as long as the application is running, until a cancellation is requested on the provided token.
        /// </summary>
        void Run(XElement settings, IDeploymentReader deploymentReader, IApplicationEnvironment environment, CancellationToken cancellationToken);

        void ApplyChangedSettings(XElement settings);
    }
}
