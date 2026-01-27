using CoDi.Data.Contracts.Entities;

namespace CoDi.Data.Contracts.CurrentSong;

public interface IDailySongStatsRepository
{
    public Task<DailySongStats> GetDailySongStatsAsync(int songId, CancellationToken cancellationToken);
    public Task<bool> AddOrUpdateDailySongStatsAsync(DailySongStats dailySongStats, CancellationToken cancellationToken);
}