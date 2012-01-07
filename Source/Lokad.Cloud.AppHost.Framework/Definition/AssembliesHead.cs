using System;

namespace Lokad.Cloud.AppHost.Framework.Definition
{
    [Serializable]
    public class AssembliesHead : IEquatable<AssembliesHead>
    {
        public string AssembliesId { get; private set; }

        public AssembliesHead(string id)
        {
            AssembliesId = id;
        }

        public bool Equals(AssembliesHead other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StringComparer.Ordinal.Equals(AssembliesId, other.AssembliesId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (AssembliesHead)) return false;
            return Equals((AssembliesHead) obj);
        }

        public override int GetHashCode()
        {
            return AssembliesId.GetHashCode();
        }
    }
}
