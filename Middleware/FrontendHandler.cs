using Locafi.Controllers;
using Locafi.Models;
using Newtonsoft.Json;
using PhotinoNET;

namespace Locafi.Middleware;

public static class FrontendHandler
{
    public static void HandleIncoming(PhotinoWindow window, string message)
    {
        FrontendMessage msg = JsonConvert.DeserializeObject<FrontendMessage>(message) ?? 
                              throw new NullReferenceException();

        switch (msg.type)
        {
            case "GET_PLAYLIST_LIST":
            {
                window.SendWebMessage(JsonConvert.SerializeObject(new BackendMessage("GET_PLAYLIST_LIST", 
                    PlaylistController.GetPlaylists())));
                
                break;
            }
        }
    }
}