using CoDi.Common.Constants;
using CoDi.Data.Contracts.CurrentSong;
using CoDi.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoDi.Data.CurrentSong;

public class DailySongStatsRepository(CoDiContext context) : IDailySongStatsRepository
{
    public async Task<DailySongStats?> GetSongStatsBySongIdAsync(int songId, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var songStats = await context.DailySongStats.FirstOrDefaultAsync(x => x.SongId == songId && x.Day == today, cancellationToken);

        return songStats;
    }

    public async Task AddOrUpdateDailySongStatsAsync(int songId, CancellationToken cancellationToken)
    {
        var dbEntry = await GetSongStatsBySongIdAsync(songId, cancellationToken);

        if (dbEntry != null)
        {
            dbEntry.TimePlayedSec += TimeSpan.FromSeconds(WatcherCallInterval.SongWatcherInterval);
        }
        else
        {
            var dailySongStats = new DailySongStats
            {
                Day = DateOnly.FromDateTime(DateTime.Today),
                SongId = songId,
                FirstPlayedAt = TimeOnly.FromDateTime(DateTime.Now),
                TimePlayedSec = TimeSpan.FromSeconds(WatcherCallInterval.SongWatcherInterval)
            };

            await context.DailySongStats.AddAsync(dailySongStats, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}