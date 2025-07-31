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

    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<VideoGame> GetVideoGameById(int id)
    {
        var game = videoGames.FirstOrDefault(g => g.Id == id);

        if (game is null)
            return NotFound();

        return Ok(game);
    }

    [HttpPost]
    public ActionResult<VideoGame> AddVideoGame(VideoGame request)
    {
        if (request is null)
            return BadRequest();

        request.Id = videoGames.Max(g => g.Id) + 1;
        videoGames.Add(request);
        return CreatedAtAction(nameof(GetVideoGameById), new { id = request.Id }, request);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateVideoGame(int id, VideoGame request)
    {
        var game = videoGames.FirstOrDefault(g => g.Id == id);

        if (game is null)
            return NotFound();

        game.Title = request.Title;
        game.Platform = request.Platform;
        game.Developer = request.Developer;
        game.Publisher = request.Publisher;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteVideoGame(int id)
    {
        var game = videoGames.FirstOrDefault(g => g.Id == id);

        if (game is null)
            return NotFound();

        videoGames.Remove(game);
        return NoContent();
    }
}
