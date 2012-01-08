#region Copyright (c) Lokad 2009-2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using Lokad.Cloud.AppHost.Framework;
using Lokad.Cloud.AppHost.Framework.Definition;

namespace Lokad.Cloud.AppHost
{ 
    /// <summary>
    /// Cell-specific counterpart of the HostHandle.
    /// Handle to communicate with the application host from the outside:
    /// - Send commands to the host
    /// - Query (stale) info about the host (like current deployment)
    /// </summary>
    internal class CellHandle
    {
        internal readonly string SolutionName;
        internal readonly string CellName;

        internal SolutionHead CurrentDeployment { get; set; }
        internal AssembliesHead CurrentAssemblies { get; set; }
        internal CellInstanceIdentity CurrentIdentity { get; set; }

        internal CellHandle(string solutionName, string cellName)
        {
            SolutionName = solutionName;
            CellName = cellName;
        }
    }
}
