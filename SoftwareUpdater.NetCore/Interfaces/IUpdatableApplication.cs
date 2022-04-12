using System.Reflection;

namespace SoftwareUpdater.Interfaces;

public interface IUpdatableApplication
{
    Guid ApplicationID { get; }
    Assembly ApplicationAssembly { get; }
    string ApplicationName { get; }
    Version CurrentVersion { get; }
}
