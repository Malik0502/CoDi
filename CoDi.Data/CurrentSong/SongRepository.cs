using CoDi.Data.Contracts.CurrentSong;
using CoDi.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoDi.Data.CurrentSong;

public class SongRepository(CoDiContext context) : ISongRepository
{
    public async Task<Song?> GetSongAsync(string artist, string songName, CancellationToken cancellationToken)
    {
        var song = await context.Song.FirstOrDefaultAsync(x => x.Artist == artist && x.Name == songName, cancellationToken);

        return song;
    }

    public async Task<Song?> AddSongAsync(Song song, CancellationToken cancellationToken)
    {
        var dbEntry = await GetSongAsync(song.Artist, song.Name, cancellationToken);

        if (dbEntry != null)
        {
            return dbEntry;
        }

        var result = await context.AddAsync(song, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
}