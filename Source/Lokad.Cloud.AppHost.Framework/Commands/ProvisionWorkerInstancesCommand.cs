#region Copyright (c) Lokad 2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;

namespace Lokad.Cloud.AppHost.Framework.Commands
{
    [Serializable]
    public sealed class ProvisionWorkerInstancesCommand : IHostCommand
    {
        public int NumberOfWorkerInstances { get; private set; }

        public ProvisionWorkerInstancesCommand(int numberOfWorkerInstances)
        {
            NumberOfWorkerInstances = numberOfWorkerInstances;
        }
    }
}
