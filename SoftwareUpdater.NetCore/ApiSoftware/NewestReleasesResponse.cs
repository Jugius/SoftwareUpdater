using SoftwareUpdater.ApiSoftware.Entities;

namespace SoftwareUpdater.ApiSoftware;

public class NewestReleasesResponse
{
    public Release[] Releases { get; set; }
    public virtual string RawJson { get; set; }
    public virtual string Status { get; set; }
    public virtual string ErrorMessage { get; set; }
}
