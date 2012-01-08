using System;

namespace Lokad.Cloud.AppHost.Framework
{
    [Serializable]
    public class CellInstanceIdentity
    {
        public HostInstanceIdentity Host { get; private set; }
        public string SolutionName { get; private set; }
        public string CellName { get; private set; }
        public string UniqueCellInstanceName { get; set; }

        public CellInstanceIdentity(HostInstanceIdentity host, string solutionName, string cellName, string uniqueCellInstanceName)
        {
            Host = host;
            SolutionName = solutionName;
            CellName = cellName;
            UniqueCellInstanceName = uniqueCellInstanceName;
        }
    }
}
