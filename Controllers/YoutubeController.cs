using Locafi.Models;
using Microsoft.AspNetCore.Mvc;
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
        
        File.Copy(res.Data, Path.Join((new FileInfo(res.Data)).DirectoryName, settings.name));
        File.Delete(res.Data);
    }
}

public record VideoDownloadSettings(string url, int? start, int? end, Playlist playlist, string name);
public record YoutubeDownloadInfo(string link, Playlist playlist, string name);