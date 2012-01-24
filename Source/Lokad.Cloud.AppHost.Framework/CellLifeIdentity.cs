#region Copyright (c) Lokad 2011-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Runtime.Serialization;

namespace Lokad.Cloud.AppHost.Framework
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.lokad.com/lokad-cloud/apphost/1.0")]
    public sealed class CellLifeIdentity
    {
        [DataMember(Order = 1)]
        public HostLifeIdentity Host { get; private set; }

        [DataMember(Order = 2)]
        public string SolutionName { get; private set; }

        [DataMember(Order = 3)]
        public string CellName { get; private set; }

        [DataMember(Order = 4)]
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
