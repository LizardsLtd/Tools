using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheLizzards.Tests
{
	public sealed class EmbeddedContentLoader
	{
		private readonly Assembly assembly;

		public EmbeddedContentLoader(string assemblyName)
		{
			this.assembly = Assembly.Load(new AssemblyName(assemblyName));
		}
		public async Task<string> LoadTextFileAsync(string resourceHandle)
		{
			using (var reader = new StreamReader(assembly.GetManifestResourceStream(resourceHandle)))
			{
				return await reader.ReadToEndAsync();
			}
		}
	}
}
