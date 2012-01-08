#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using Lokad.Cloud.AppHost.Framework.Definition;

namespace Lokad.Cloud.AppHost.Framework.Events
{
    [Serializable]
    public class NewDeploymentDetectedEvent : IHostEvent
    {
        public HostLifeIdentity Host { get; private set; }
        public SolutionHead Deployment { get; private set; }
        public SolutionDefinition Solution { get; private set; }

        public NewDeploymentDetectedEvent(HostLifeIdentity host, SolutionHead deployment, SolutionDefinition solution)
        {
            Host = host;
            Deployment = deployment;
            Solution = solution;
        }
    }
}
