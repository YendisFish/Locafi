using PhotinoNET;
using Locafi.Controllers;
using Locafi.Middleware;
using Locafi.Models;

namespace Locafi
{
    class Program
    {
        public static volatile bool IsRunning = true;
        public static volatile bool ApiStarted = false;
        
        [STAThread]
        static void Main(string[] args)
        {
            if ((new DirectoryInfo(Directory.GetCurrentDirectory())).GetDirectories().Where(x => x.Name.Contains("yt-dl")).Count() < 1)
            {
                YoutubeController.DownloadDependencies().GetAwaiter().GetResult();
            }

            Thread tr = new Thread(new ThreadStart(WebHostMain));
            tr.Start();

            while (!ApiStarted) ;
            
            // Window title declared here for visibility
            string windowTitle = "Locafi";

            // Creating a new PhotinoWindow instance with the fluent API
            var window = new PhotinoWindow()
                .SetTitle(windowTitle)
                // Resize to a percentage of the main monitor work area
                .SetUseOsDefaultSize(true)
                //.SetSize(new Size(600, 400))
                // Center window in the middle of the screen
                .Center()
                // Users can resize windows by default.
                // Let's make this one fixed instead.
                .SetResizable(true)
                /*.RegisterCustomSchemeHandler("app", (object sender, string scheme, string url, out string contentType) =>
                {
                    contentType = "text/javascript";
                    return new MemoryStream(Encoding.UTF8.GetBytes(@"
                        (() =>{
                            window.setTimeout(() => {
                                alert(`🎉 Dynamically inserted JavaScript.`);
                            }, 1000);
                        })();
                    "));
                })*/
                // Most event handlers can be registered after the
                // PhotinoWindow was instantiated by calling a registration 
                // method like the following RegisterWebMessageReceivedHandler.
                // This could be added in the PhotinoWindowOptions if preferred.
                .RegisterWebMessageReceivedHandler((object sender, string message) => {
                    var window = (PhotinoWindow)sender;

                    // The message argument is coming in from sendMessage.
                    // "window.external.sendMessage(message: string)"
                    string response = $"Received message: \"{message}\"";

                    // Send a message back the to JavaScript event handler.
                    // "window.external.receiveMessage(callback: Function)"
                    window.SendWebMessage(response);
                })
                .Load("wwwroot/index.html"); // Can be used with relative path strings or "new URI()" instance to load a website.

            window.WaitForClose(); // Starts the application event loop
            
            IsRunning = false;
        }

        static void WebHostMain()
        {
            var builder = WebApplication.CreateBuilder();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //app.UseSwagger();
            //app.UseSwaggerUI();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
            });
            
            app.MapPost("/DOWNLOAD", (YoutubeDownloadInfo info) => 
                YoutubeController.DownloadVideo(new(info.link, null, null, info.playlist, info.name)));
            app.MapPost("/DOWNLOAD_PLAYLIST", (PlaylistDownloadInfo info) => YoutubeController.DownloadPlaylist(info));
            
            app.MapControllers();

            Thread tr = new(new ThreadStart(app.Run));
            tr.Start();

            ApiStarted = true;
            while (IsRunning) ;

            lock (app)
            {
                app.StopAsync().GetAwaiter().GetResult();
            }
        } 
    }
}