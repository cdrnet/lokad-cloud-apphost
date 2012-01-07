#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using Lokad.Cloud.AppHost.Framework.Definition;

namespace Lokad.Cloud.AppHost.Framework.Commands
{
    [Serializable]
    public sealed class LoadDeploymentCommand : IHostCommand
    {
        public SolutionHead Deployment { get; private set; }

        public LoadDeploymentCommand(SolutionHead deployment)
        {
            Deployment = deployment;
        }
    }
}
