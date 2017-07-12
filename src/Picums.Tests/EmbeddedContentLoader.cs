using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Picums.Tests
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
            string fullResourceHandle = CreateFullyQualifiedResourceName(resourceHandle);
            using (var reader = new StreamReader(assembly.GetManifestResourceStream(fullResourceHandle)))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private string CreateFullyQualifiedResourceName(string resourceHandle)
            => string.Join(".", this.GetAssemblyName(), resourceHandle);

        private string GetAssemblyName() => this.assembly.GetName().Name;
    }
}