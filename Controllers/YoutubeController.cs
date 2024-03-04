using Locafi.Models;
using Microsoft.AspNetCore.Mvc;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace Locafi.Middleware;

public static class YoutubeController
{
    internal static DownloadProgress progress { get; set; }

    public static IProgress<DownloadProgress>? Progress = new Progress<DownloadProgress>(p =>
    {
        progress = p;
    });
    
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

    public static async Task DownloadPlaylist(PlaylistDownloadInfo data)
    {
        Directory.CreateDirectory(Path.Join("./playlists/", data.name));
        
        YoutubeDL dl = new();
        dl.OutputFolder = Path.Join("./playlists/", data.name);
        RunResult<string[]>? res = await dl.RunAudioPlaylistDownload(data.link, format: AudioConversionFormat.Mp3, progress: YoutubeController.Progress) 
                                   ?? throw new NullReferenceException();
    }

    public static async Task<ActionResult<ProgressInfo>> GetYoutubeDownloadProcess()
    {
        return new ProgressInfo(progress.Progress, progress.ETA);
    }
}

public record VideoDownloadSettings(string url, int? start, int? end, Playlist playlist, string name);
public record YoutubeDownloadInfo(string link, Playlist playlist, string name);
public record PlaylistDownloadInfo(string link, string name);
public record ProgressInfo(float progress, string eta);