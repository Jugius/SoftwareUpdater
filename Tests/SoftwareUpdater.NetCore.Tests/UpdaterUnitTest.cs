using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace SoftwareUpdater.NetCore.Tests;

public class UpdaterUnitTest
{
    [Fact]
    public async Task GetReleasesAsyncReturnsNotNull()    
    {
        var testApp = new TestApplication();
        var updater = new Updater(testApp);
        var releases = await updater.GetReleasesAsync();
        Assert.NotNull(releases);
    }
    public class TestApplication : Interfaces.IUpdatableApplication
    {
        public Guid ApplicationID => new Guid("d883f1ea-0104-41da-2705-08da141f0ba0");

        public Assembly ApplicationAssembly => this.ApplicationAssembly;

        public string ApplicationName => "GridsDownloader";

        public Version CurrentVersion => new Version(1, 0);
    }
}
