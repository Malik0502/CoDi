using CoDi.Data.Contracts.Entities;

namespace CoDi.Data.Contracts.CurrentSong;

public interface ISongRepository
{
    public Task<Song?> GetSongAsync(string artist, string songName, CancellationToken cancellationToken);

    public Task<bool> AddSongAsync(Song song, CancellationToken cancellationToken);
}