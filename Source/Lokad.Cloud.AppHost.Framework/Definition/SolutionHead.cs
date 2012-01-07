using System;

namespace Lokad.Cloud.AppHost.Framework.Definition
{
    [Serializable]
    public class SolutionHead : IEquatable<SolutionHead>
    {
        public string SolutionId { get; private set; }

        public SolutionHead(string id)
        {
            SolutionId = id;
        }

        public bool Equals(SolutionHead other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StringComparer.Ordinal.Equals(SolutionId, other.SolutionId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SolutionHead)) return false;
            return Equals((SolutionHead) obj);
        }

        public override int GetHashCode()
        {
            return SolutionId.GetHashCode();
        }
    }
}
