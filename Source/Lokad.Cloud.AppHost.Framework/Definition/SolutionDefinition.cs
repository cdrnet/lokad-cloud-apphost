using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Lokad.Cloud.AppHost.Framework.Definition
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.lokad.com/lokad-cloud/apphost/1.0")]
    public class SolutionDefinition : IEquatable<SolutionDefinition>
    {
        [DataMember(Order = 1)]
        public string SolutionName { get; private set; }

        [DataMember(Order = 2)]
        public CellDefinition[] Cells { get; private set; }

        public SolutionDefinition(string name, CellDefinition[] cells)
        {
            SolutionName = name;
            Cells = cells;
        }

        public bool Equals(SolutionDefinition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StringComparer.Ordinal.Equals(SolutionName, other.SolutionName)
                   && Cells.SequenceEqual(other.Cells);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SolutionDefinition)) return false;
            return Equals((SolutionDefinition) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (SolutionName.GetHashCode()*397) ^ Cells.GetHashCode();
            }
        }
    }
}
