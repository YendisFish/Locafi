using System.Net.Http;
using System.Web;
using Locafi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Locafi.Controllers;

[ApiController]
[Route("/song")]
public class SongController : ControllerBase
{
    [HttpGet]
    [Route("/song/GET_SONG_STREAM")]
    public async Task<ActionResult<StreamContent>> GetSong([FromQuery]string song)
    {
        try
        {
            Song s = JsonConvert.DeserializeObject<Song>(song)!;

            FileStream fs = System.IO.File.OpenRead(s.location);
            return new FileStreamResult(fs, "audio/mpeg");
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}