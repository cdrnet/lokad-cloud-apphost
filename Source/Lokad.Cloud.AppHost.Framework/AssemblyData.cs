namespace Lokad.Cloud.AppHost.Framework
{
    public sealed class AssemblyData
    {
        public string Name { get; private set; }
        public byte[] Bytes { get; private set; }

        public AssemblyData(string name, byte[] bytes)
        {
            Name = name;
            Bytes = bytes;
        }
    }
}
