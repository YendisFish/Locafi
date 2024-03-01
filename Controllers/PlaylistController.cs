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
}