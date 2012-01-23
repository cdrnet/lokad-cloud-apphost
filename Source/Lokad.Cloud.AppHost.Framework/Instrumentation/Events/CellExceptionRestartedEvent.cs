#region Copyright (c) Lokad 2011-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Xml.Linq;

namespace Lokad.Cloud.AppHost.Framework.Instrumentation.Events
{
    [Serializable]
    public class CellExceptionRestartedEvent : IHostEvent
    {
        public HostEventLevel Level { get { return HostEventLevel.Error; } }
        public CellLifeIdentity Cell { get; private set; }
        public Exception Exception { get; private set; }
        public bool FloodPrevention { get; private set; }

        public CellExceptionRestartedEvent(CellLifeIdentity cell, Exception exception, bool floodPrevention)
        {
            Cell = cell;
            Exception = exception;
            FloodPrevention = floodPrevention;
        }

        public string Describe()
        {
            return string.Format("Cell {0} of solution {1} exception: {2}.",
                Cell.CellName, Cell.SolutionName, Exception != null ? Exception.Message : string.Empty);
        }

        public XElement DescribeMeta()
        {
            var meta = new XElement("Meta",
                new XElement("AppHost",
                    new XElement("Host", Cell.Host.WorkerName),
                    new XElement("Solution", Cell.SolutionName),
                    new XElement("Cell", Cell.CellName)));

            if (Exception != null)
            {
                meta.Add(new XElement("Exception",
                    new XAttribute("typeName", Exception.GetType().FullName),
                    new XAttribute("message", Exception.Message),
                    Exception.ToString()));
            }

            return meta;
        }
    }
}
