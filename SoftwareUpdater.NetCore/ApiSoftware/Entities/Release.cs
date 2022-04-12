namespace SoftwareUpdater.ApiSoftware.Entities;

public class Release
{
    public Guid Id { get; set; }
    public string Version { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Kind { get; set; }
    public bool IsFinal { get; set; }
    public Guid ApplicationId { get; set; }
    public List<Detail> Details { get; set; }
    public List<File> Files { get; set; }
}
