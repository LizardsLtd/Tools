using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Picums.Tests
{
    public sealed class TestEnviroment : IHostingEnvironment
    {
        public string EnvironmentName { get; set; }
        public string ApplicationName { get; set; }
        public string WebRootPath { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
    }

    //public sealed class TestStartup : StartupBase
    //{
    //	public TestStartup() : base(new TestEnviroment())
    //	{
    //	}
    //}
}