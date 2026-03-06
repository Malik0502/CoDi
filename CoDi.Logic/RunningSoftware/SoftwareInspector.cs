using System.Diagnostics;
using CoDi.Logic.Contracts.RunningSoftware;
using CoDi.Logic.Contracts.RunningSoftware.Model;

namespace CoDi.Logic.RunningSoftware;

public class SoftwareInspector : ISoftwareInspector
{
    public async Task<IList<SoftwareModel>?> GetRunningSoftwareAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var localProcesses = Process.GetProcesses().Where(p => p.MainWindowHandle != IntPtr.Zero).ToArray();
        List<SoftwareModel>? software = [];

        foreach (var process in localProcesses)
        {
            Console.WriteLine(process.ProcessName);
            var softwareModel = new SoftwareModel() { Name = process.ProcessName };
            software.Add(softwareModel);
        }

        return software;
    }

    private IList<string> FilterWindowsCommonSoftware()
    {
        return new List<string>();
    }
}