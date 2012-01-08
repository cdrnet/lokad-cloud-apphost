using System;

namespace Lokad.Cloud.AppHost.Framework
{
    [Serializable]
    public class HostLifeIdentity
    {
        public string WorkerName { get; private set; }
        public string UniqueWorkerInstanceName { get; private set; }

        public HostLifeIdentity(string workerName, string uniqueWorkerInstanceName)
        {
            WorkerName = workerName;
            UniqueWorkerInstanceName = uniqueWorkerInstanceName;
        }
    }
}
