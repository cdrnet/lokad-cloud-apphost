#region Copyright (c) Lokad 2011-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Xml.Linq;

namespace Lokad.Cloud.AppHost.Framework.Instrumentation.Events
{
    [Serializable]
    public class HostStoppedEvent : IHostEvent
    {
        public HostEventLevel Level { get { return HostEventLevel.Trace; } }
        public HostLifeIdentity Host { get; private set; }

        public HostStoppedEvent(HostLifeIdentity host)
        {
            Host = host;
        }

        public string Describe()
        {
            return string.Format("AppHost stopped on {0}.", Host.WorkerName);
        }

        public XElement[] DescribeMeta()
        {
            return new[]
                {
                    new XElement("AppHost", new XElement("Host", Host.WorkerName))
                };
        }
    }
}
