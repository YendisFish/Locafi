using PhotinoNET;
using System;
using System.Drawing;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore;
using System.ComponentModel;
using System.Text;
using Locafi.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace Locafi
{
    class Program
    {
        public static volatile bool IsRunning = true;
        
        [STAThread]
        static void Main(string[] args)
        {
            Thread tr = new Thread(new ThreadStart(WebHostMain));
            tr.Start();
            
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

            IServiceCollection services = builder.Services;

            var app = builder.Build();

            //app.UseSwagger();
            //app.UseSwaggerUI();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
            });

            app.MapGet("/GET_PLAYLST_LIST", () => PlaylistController.GetPlaylists());
            
            app.MapControllers();

            Thread tr = new(new ThreadStart(app.Run));
            tr.Start();

            while (IsRunning) ;

            lock (app)
            {
                app.StopAsync().GetAwaiter().GetResult();
            }
        } 
    }
}
