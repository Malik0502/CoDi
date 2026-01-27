using CoDi.Data.Contracts.CurrentSong;
using CoDi.Data.Contracts.Entities;

namespace CoDi.Data.CurrentSong;

public class DailySongStatsRepository : IDailySongStatsRepository
{
    public Task<DailySongStats> GetDailySongStatsAsync(int songId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddOrUpdateDailySongStatsAsync(DailySongStats dailySongStats, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}