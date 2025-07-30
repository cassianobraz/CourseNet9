using Microsoft.AspNetCore.Mvc;

namespace VideoGameApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VideoGameController : ControllerBase
{
    static private List<VideoGame> videoGames = new List<VideoGame>
    {
        new VideoGame
        {
            Id = 1,
            Title = "Spider-Man 2",
            Platform = "PS5",
            Developer = "Insomniac Games",
            Publisher = "Sony Interactive Entertainment"
        },
        new VideoGame
        {
            Id = 2,
            Title = "The LEgend of Zelda: Breath of the Wild",
            Platform = "Nintendo Switch",
            Developer = "Nintendo EPD",
            Publisher = "Nintendo"
        }
    };

    [HttpGet]
    public ActionResult<List<VideoGame>> GetVideoGames()
    {
        return Ok(videoGames);
    }
}
