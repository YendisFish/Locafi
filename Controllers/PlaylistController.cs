using Locafi.Models;

namespace Locafi.Controllers;

public static class PlaylistController
{
    public static List<Playlist> GetPlaylists()
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

    public static List<Song> GetSongsFromPlaylist(Playlist playlist)
    {
        DirectoryInfo dir = new DirectoryInfo(playlist.location);

        List<Song> songs = new();
        foreach(FileInfo file in dir.GetFiles())
        {
            songs.Add(new Song(file.Name, file.FullName));
        }

        return songs;
    }

    public static void CreatePlaylist(dynamic data)
    {
        Directory.CreateDirectory("./" + data.name);
    }
}