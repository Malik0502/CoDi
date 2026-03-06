
using CoDi.Logic.Contracts.RunningSoftware.Model;

namespace CoDi.Logic.Contracts.RunningSoftware;

public interface ISoftwareInspector
{
    public Task<IList<SoftwareModel>?> GetRunningSoftwareAsync(CancellationToken cancellationToken);
}