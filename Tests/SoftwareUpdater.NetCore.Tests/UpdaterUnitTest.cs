using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SoftwareUpdater.NetCore.Tests;

public class UpdaterUnitTest
{
    private readonly ITestOutputHelper output;
    public UpdaterUnitTest(ITestOutputHelper output)
    {
        this.output = output;
    }
    [Fact]
    public async Task GetReleasesAsyncReturnsNotNull()    
    {
        var testApp = new TestApplication();
        var updater = new Updater(testApp);
        var releases = await updater.GetReleasesAsync();        
        Assert.NotNull(releases);
        output.WriteLine("Releases count: " + releases.Length);
    }
    [Fact]
    public async Task GetRelevantReleaseReturnsNotNull()
    {
        var testApp = new TestApplication();
        var updater = new Updater(testApp);
        
        var releases = await updater.GetReleasesAsync();
        Assert.NotNull(releases);
        Assert.True(releases.Length > 0);
        output.WriteLine($"Releases count: {releases.Length}");

        var relevant = updater.GetRelevantRelease(releases);
        Assert.NotNull(relevant);
        output.WriteLine("Last release version: " + relevant.Version.ToString());

        var file = updater.GetRelevantFile(relevant);
        Assert.NotNull(file);
        output.WriteLine($"File: {file.Name} Type: {file.Kind}");

    }
    public class TestApplication : Interfaces.IUpdatableApplication
    {
        public Guid ApplicationID => new Guid("d883f1ea-0104-41da-2705-08da141f0ba0");

        public Assembly ApplicationAssembly => this.ApplicationAssembly;

        public string ApplicationName => "GridsDownloader";

        public Version CurrentVersion => new Version(1, 6, 1);
    }
}
