using Locafi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Locafi.Controllers;

[ApiController]
[Route("/playlist")]
public class PlaylistController : ControllerBase
{
    [HttpGet]
    [Route("/playlist/GET_PLAYLIST_LIST")]
    public List<Playlist> GetPlaylists()
    {
        if (!Directory.Exists("./playlists"))
        {
            Directory.CreateDirectory("./playlists");
        }
        
        DirectoryInfo playlistDir = new DirectoryInfo("./playlists");

        List<Playlist> ret = new();

        foreach (DirectoryInfo info in playlistDir.GetDirectories())
        {
            ret.Add(new Playlist(info.Name, info.FullName));
        }
        
        return ret;
    }

    [HttpPost]
    [Route("/playlist/GET_PLAYLIST_SONGS")]
    public List<Song> GetSongsFromPlaylist(Playlist playlist)
    {
        DirectoryInfo dir = new DirectoryInfo(playlist.location);

        List<Song> songs = new();
        foreach(FileInfo file in dir.GetFiles())
        {
            songs.Add(new Song(file.Name, file.FullName));
        }

        return songs;
    }

    [HttpPost]
    [Route("/playlist/CREATE_PLAYLIST")]
    public void CreatePlaylist(CreatePlaylistData data)
    {
        Directory.CreateDirectory("./playlists/" + data.name);
    }
}

public record CreatePlaylistData(string name);