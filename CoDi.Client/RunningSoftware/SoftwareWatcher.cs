using CoDi.Logic.Contracts.RunningSoftware;

namespace CoDi.Client.RunningSoftware;

public class SoftwareWatcher(ISoftwareInspector softwareInspector)
{
    public async Task<string> WatchSoftware(CancellationToken cancellationToken)
    {
        var runningSoftware = await softwareInspector.GetRunningSoftwareAsync(cancellationToken);

        var softwareInfo = string.Empty;

        if (runningSoftware == null || runningSoftware.Count == 0)
        {
            return string.Empty;
        }

        // NOT FINISHED
        // There has to be a way to say what the metadata is for each software, but for now we will just return the name of the software

        foreach (var software in runningSoftware)
        {
            softwareInfo += software.Name;
        }

        return softwareInfo;

    }
}