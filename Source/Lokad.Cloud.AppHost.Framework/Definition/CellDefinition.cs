using System;
using System.Runtime.Serialization;

namespace Lokad.Cloud.AppHost.Framework.Definition
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.lokad.com/lokad-cloud/apphost/1.0")]
    public class CellDefinition : IEquatable<CellDefinition>
    {
        [DataMember(Order = 1)]
        public string CellName { get; private set; }

        [DataMember(Order = 2)]
        public AssembliesHead Assemblies { get; private set; }

        [DataMember(Order = 3)]
        public string EntryPointTypeName { get; private set; }

        [DataMember(Order = 4)]
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
