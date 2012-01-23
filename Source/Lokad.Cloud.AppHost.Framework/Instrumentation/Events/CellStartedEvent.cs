#region Copyright (c) Lokad 2011-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Xml.Linq;

namespace Lokad.Cloud.AppHost.Framework.Instrumentation.Events
{
    [Serializable]
    public class CellStartedEvent : IHostEvent
    {
        public HostEventLevel Level { get { return HostEventLevel.Trace; } }
        public CellLifeIdentity Cell { get; private set; }

        public CellStartedEvent(CellLifeIdentity cell)
        {
            Cell = cell;
        }

        public string Describe()
        {
            return string.Format("Cell {0} of solution {1} started on {2}.",
                Cell.CellName, Cell.SolutionName, Cell.Host.WorkerName);
        }

        public XElement DescribeMeta()
        {
            return new XElement("Meta",
                new XElement("AppHost",
                    new XElement("Host", Cell.Host.WorkerName),
                    new XElement("Solution", Cell.SolutionName),
                    new XElement("Cell", Cell.CellName)));
        }
    }
}
