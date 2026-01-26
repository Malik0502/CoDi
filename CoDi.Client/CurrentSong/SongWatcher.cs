using CoDi.Logic.Contracts.CurrentSong;

namespace CoDi.Client.CurrentSong;

public class SongWatcher(ICurrentSongInspector currentSongInspector)
{
    public async Task<string> WatchSongs(CancellationToken cancellationToken)
    {
        var currentSong = await currentSongInspector.GetCurrentSongAsync(cancellationToken);

        if (currentSong == null)
        {
            return string.Empty;
        }

        if (string.IsNullOrEmpty(currentSong.Artist) || string.IsNullOrEmpty(currentSong.Name))
        {
            return string.Empty;
        }

        var songInfo = $"Artist: {currentSong.Artist} Song Played: {currentSong.Name}";

        return songInfo;
    }
}