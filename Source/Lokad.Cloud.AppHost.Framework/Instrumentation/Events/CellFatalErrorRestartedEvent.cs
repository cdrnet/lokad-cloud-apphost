#region Copyright (c) Lokad 2011-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Xml.Linq;

namespace Lokad.Cloud.AppHost.Framework.Instrumentation.Events
{
    [Serializable]
    public class CellFatalErrorRestartedEvent : IHostEvent
    {
        public HostEventLevel Level { get { return HostEventLevel.FatalError; } }
        public CellLifeIdentity Cell { get; private set; }
        public Exception Exception { get; private set; }

        public CellFatalErrorRestartedEvent(CellLifeIdentity cell, Exception exception)
        {
            Cell = cell;
            Exception = exception;
        }

        public string Describe()
        {
            return string.Format("AppHost: Fatal error in {0} cell of {1} solution on {2}: {3}.",
                Cell.CellName, Cell.SolutionName, Cell.Host.WorkerName, Exception != null ? Exception.Message : string.Empty);
        }

        public XElement DescribeMeta()
        {
            var meta = new XElement("Meta",
                new XElement("Component", "Lokad.Cloud.AppHost"),
                new XElement("Event", "CellFatalErrorRestartedEvent"),
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
