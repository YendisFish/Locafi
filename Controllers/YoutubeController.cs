using Locafi.Models;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace Locafi.Middleware;

public static class YoutubeController
{
    public static async Task DownloadDependencies()
    {
        await YoutubeDLSharp.Utils.DownloadYtDlp();
        await YoutubeDLSharp.Utils.DownloadFFmpeg();
    }

    public static async Task DownloadVideo(VideoDownloadSettings settings)
    {
        YoutubeDL dl = new YoutubeDL();
        dl.OutputFolder = settings.playlist.location;
        RunResult<string>? res = await dl.RunAudioDownload(settings.url, AudioConversionFormat.Mp3) ?? throw new NullReferenceException();
    }
}

public record VideoDownloadSettings(string url, int? start, int? end, Playlist playlist);
public record YoutubeDownloadInfo(string link, Playlist playlist);