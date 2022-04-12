using System.Text.Json.Serialization;

namespace SoftwareUpdater.ApiSoftware;

internal class NewestReleasesRequest
{

    [JsonIgnore]
    internal string RequestUrl => @"https://software.oohelp.net/software/api/applications/getnewest";
    public Guid ApplicationId { get; set; }
    public string Version { get; set; }
}
