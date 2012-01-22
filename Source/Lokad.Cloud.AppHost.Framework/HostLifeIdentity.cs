using System;
using System.Runtime.Serialization;

namespace Lokad.Cloud.AppHost.Framework
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.lokad.com/lokad-cloud/apphost/1.0")]
    public sealed class HostLifeIdentity
    {
        [DataMember(Order = 1)]
        public string WorkerName { get; private set; }

        [DataMember(Order = 2)]
        public string UniqueWorkerInstanceName { get; private set; }

        public HostLifeIdentity(string workerName, string uniqueWorkerInstanceName)
        {
            WorkerName = workerName;
            UniqueWorkerInstanceName = uniqueWorkerInstanceName;
        }
    }
}
