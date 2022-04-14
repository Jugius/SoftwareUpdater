using SoftwareUpdater.Interfaces;

namespace SoftwareUpdater;

public class Updater : UpdaterBase
{
    public Updater(IUpdatableApplication application) : base(application)
    {
    }
}
