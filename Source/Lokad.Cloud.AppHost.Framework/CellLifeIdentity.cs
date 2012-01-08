using System;

namespace Lokad.Cloud.AppHost.Framework
{
    [Serializable]
    public class CellLifeIdentity
    {
        public HostLifeIdentity Host { get; private set; }
        public string SolutionName { get; private set; }
        public string CellName { get; private set; }
        public string UniqueCellInstanceName { get; set; }

        public CellLifeIdentity(HostLifeIdentity host, string solutionName, string cellName, string uniqueCellInstanceName)
        {
            Host = host;
            SolutionName = solutionName;
            CellName = cellName;
            UniqueCellInstanceName = uniqueCellInstanceName;
        }
    }
}
