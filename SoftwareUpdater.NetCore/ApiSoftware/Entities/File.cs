namespace SoftwareUpdater.ApiSoftware.Entities;

public class File
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Kind { get; set; }
    public string CheckSum { get; set; }
    public ulong Size { get; set; }
    public DateTime Uploaded { get; set; }
    public string Description { get; set; }
    public Guid ReleaseId { get; set; }
}
