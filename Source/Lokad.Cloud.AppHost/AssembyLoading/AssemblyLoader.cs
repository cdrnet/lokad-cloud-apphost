#region Copyright (c) Lokad 2009-2011
// This code is released under the terms of the new BSD licence.
// URL: http://www.lokad.com/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Lokad.Cloud.AppHost.AssembyLoading
{
    internal sealed class AssemblyLoader
    {
        public void LoadAssembliesIntoAppDomain(IEnumerable<Tuple<string, byte[]>> assembliesAndSymbols)
        {
            var assemblyBytes = new List<Tuple<string, byte[]>>();
            var symbolBytes = new Dictionary<string, byte[]>();

            foreach (var assemblyOrSymbol in assembliesAndSymbols)
            {
                string name = assemblyOrSymbol.Item1.ToLowerInvariant();
                var extension = Path.GetExtension(name).ToLowerInvariant();
                switch (extension)
                {
                    case ".dll":
                        assemblyBytes.Add(Tuple.Create(name, assemblyOrSymbol.Item2));
                        break;
                    case ".pdb":
                        symbolBytes.Add(name, assemblyOrSymbol.Item2);
                        break;
                }
            }

            var resolver = new AssemblyResolver();
            resolver.Attach();

            foreach (var assembly in assemblyBytes)
            {
                byte[] symbol;
                if (symbolBytes.TryGetValue(assembly.Item1, out symbol))
                {
                    Assembly.Load(assembly.Item2, symbol);
                }
                else
                {
                    Assembly.Load(assembly.Item2);
                }
            }
        }
    }
}
