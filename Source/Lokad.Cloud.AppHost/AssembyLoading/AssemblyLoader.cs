#region Copyright (c) Lokad 2009-2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lokad.Cloud.AppHost.AssembyLoading
{
    internal sealed class AssemblyLoader
    {
        public void LoadAssembliesIntoAppDomain(IEnumerable<Tuple<string, byte[]>> assembliesAndSymbols, string path)
        {
            var resolver = new AssemblyResolver();
            resolver.Attach();

            var assemblies = Directory.GetFiles(path, "*.dll").Union(Directory.GetFiles(path, "*.exe")).ToList();

            foreach (var assembly in assemblies)
            {
                Assembly.LoadFile(assembly);
            }
        }
    }
}
