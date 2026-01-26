using CoDi.Logic.Contracts.CurrentSong.Model;

namespace CoDi.Logic.Contracts.CurrentSong;

public interface ICurrentSongInspector
{
    public Task<SongModel?> GetCurrentSongAsync(CancellationToken cancellationToken);
}