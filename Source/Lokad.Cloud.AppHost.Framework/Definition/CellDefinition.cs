using System;

namespace Lokad.Cloud.AppHost.Framework.Definition
{
    [Serializable]
    public class CellDefinition : IEquatable<CellDefinition>
    {
        public string CellName { get; private set; }
        public AssembliesHead Assemblies { get; private set; }
        public string EntryPointTypeName { get; private set; }
        public string SettingsXml { get; private set; }

        public CellDefinition(string name, AssembliesHead assemblies, string entryPointTypeName, string settingsXml)
        {
            CellName = name;
            Assemblies = assemblies;
            EntryPointTypeName = entryPointTypeName;
            SettingsXml = settingsXml;
        }

        // Generated Equality Code:

        public bool Equals(CellDefinition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StringComparer.Ordinal.Equals(CellName, other.CellName)
                && Assemblies.Equals(other.Assemblies)
                && StringComparer.Ordinal.Equals(EntryPointTypeName, other.EntryPointTypeName)
                && StringComparer.Ordinal.Equals(SettingsXml, other.SettingsXml);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (CellDefinition)) return false;
            return Equals((CellDefinition) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = CellName.GetHashCode();
                result = (result*397) ^ Assemblies.GetHashCode();
                result = (result*397) ^ EntryPointTypeName.GetHashCode();
                result = (result*397) ^ SettingsXml.GetHashCode();
                return result;
            }
        }
    }
}
