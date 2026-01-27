using CoDi.Data.Contracts.Entities;

namespace CoDi.Data.Contracts.CurrentSong;

public interface IDailySongStatsRepository
{
    public Task<DailySongStats?> GetSongStatsBySongIdAsync(int songId, CancellationToken cancellationToken);
    public Task AddOrUpdateDailySongStatsAsync(int songId, CancellationToken cancellationToken);
}