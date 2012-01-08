using System;

namespace Lokad.Cloud.AppHost.Framework
{
    [Serializable]
    public class HostInstanceIdentity
    {
        public string WorkerName { get; private set; }
        public string UniqueWorkerInstanceName { get; private set; }

        public HostInstanceIdentity(string workerName, string uniqueWorkerInstanceName)
        {
            WorkerName = workerName;
            UniqueWorkerInstanceName = uniqueWorkerInstanceName;
        }
    }
}
