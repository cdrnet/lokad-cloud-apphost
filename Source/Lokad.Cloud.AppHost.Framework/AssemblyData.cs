#region Copyright (c) Lokad 2011-2012
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

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
