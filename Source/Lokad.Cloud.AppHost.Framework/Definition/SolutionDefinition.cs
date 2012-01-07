using System;
using System.Linq;

namespace Lokad.Cloud.AppHost.Framework.Definition
{
    [Serializable]
    public class SolutionDefinition : IEquatable<SolutionDefinition>
    {
        public string SolutionName { get; private set; }
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
