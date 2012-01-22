#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using Lokad.Cloud.AppHost.Framework.Definition;

namespace Lokad.Cloud.AppHost.Framework.Instrumentation.Events
{
    [Serializable]
    public class NewUnrelatedSolutionDetectedEvent : IHostEvent
    {
        public HostLifeIdentity Host { get; private set; }
        public SolutionDefinition Solution { get; private set; }

        public NewUnrelatedSolutionDetectedEvent(HostLifeIdentity host, SolutionDefinition solution)
        {
            Host = host;
            Solution = solution;
        }
    }
}
