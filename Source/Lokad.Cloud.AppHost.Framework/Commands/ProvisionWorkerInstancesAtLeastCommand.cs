#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;

namespace Lokad.Cloud.AppHost.Framework.Commands
{
    [Serializable]
    public sealed class ProvisionWorkerInstancesAtLeastCommand : IHostCommand
    {
        public int MinimumNumberOfWorkerInstances { get; private set; }

        public ProvisionWorkerInstancesAtLeastCommand(int minNumberOfWorkerInstances)
        {
            MinimumNumberOfWorkerInstances = minNumberOfWorkerInstances;
        }
    }
}
