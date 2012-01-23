#region Copyright (c) Lokad 2011-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Xml.Linq;
using Lokad.Cloud.AppHost.Framework.Definition;

namespace Lokad.Cloud.AppHost.Framework.Instrumentation.Events
{
    [Serializable]
    public class NewUnrelatedSolutionDetectedEvent : IHostEvent
    {
        public HostEventLevel Level { get { return HostEventLevel.Information; } }
        public HostLifeIdentity Host { get; private set; }
        public SolutionDefinition Solution { get; private set; }

        public NewUnrelatedSolutionDetectedEvent(HostLifeIdentity host, SolutionDefinition solution)
        {
            Host = host;
            Solution = solution;
        }

        public string Describe()
        {
            return string.Format("New unrelated solution {0} detected on {1}.",
                Solution.SolutionName, Host.WorkerName);
        }

        public XElement DescribeMeta()
        {
            return new XElement("Meta",
                new XElement("AppHost",
                    new XElement("Host", Host.WorkerName),
                    new XElement("Solution", Solution.SolutionName)));
        }
    }
}
