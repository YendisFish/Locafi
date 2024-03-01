using YoutubeDLSharp;

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
        RunResult<string>? res = await dl.RunAudioDownload(settings.url) ?? throw new NullReferenceException();
    }
}

public record VideoDownloadSettings(string url, int? start, int? end);