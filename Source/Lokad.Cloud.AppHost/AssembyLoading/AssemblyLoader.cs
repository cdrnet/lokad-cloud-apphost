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
        public void LoadAssembliesIntoAppDomain(IEnumerable<Tuple<string, byte[]>> assembliesAndSymbols, ApplicationEnvironment environment)
        {
            var resolver = new AssemblyResolver();
            resolver.Attach();

            // Store files locally, because only pure IL assemblies can be loaded directly from memory
            var path = Path.Combine(
                environment.GetLocalResourcePath("CellAssemblies"),
                environment.CurrentDeploymentName,
                environment.CellName);

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Directory.CreateDirectory(path);
            foreach (var assembly in assembliesAndSymbols)
            {
                File.WriteAllBytes(Path.Combine(path, assembly.Item1), assembly.Item2);
            }

            var assemblies = Directory.EnumerateFiles(path, "*.dll").Concat(Directory.EnumerateFiles(path, "*.exe"));
            foreach (var assembly in assemblies)
            {
                Assembly.LoadFile(assembly);
            }
        }
    }
}
