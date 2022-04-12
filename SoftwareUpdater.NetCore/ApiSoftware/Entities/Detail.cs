namespace SoftwareUpdater.ApiSoftware.Entities;
public class Detail
{
    public Guid Id { get; set; }
    public string Kind { get; set; }
    public string Description { get; set; }
    public Guid ReleaseId { get; set; }
}
