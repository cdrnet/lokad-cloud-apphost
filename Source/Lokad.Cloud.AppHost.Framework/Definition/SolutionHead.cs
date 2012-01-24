#region Copyright (c) Lokad 2011-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Runtime.Serialization;

namespace Lokad.Cloud.AppHost.Framework.Definition
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.lokad.com/lokad-cloud/apphost/1.0")]
    public class SolutionHead : IEquatable<SolutionHead>
    {
        [DataMember(Order = 1)]
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
