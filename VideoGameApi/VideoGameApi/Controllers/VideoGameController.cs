using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;

namespace VideoGameApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VideoGameController(VideoGameDbContext context) : ControllerBase
{
    private readonly VideoGameDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
    {
        var videoGames = await _context.VideoGames.ToListAsync();

        return Ok(videoGames);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<VideoGame>> GetVideoGameById(int id, CancellationToken ct)
    {
        var game = await _context.VideoGames.FirstOrDefaultAsync(g => g.Id == id, ct);

        if (game is null)
            return NotFound();

        return Ok(game);
    }

    [HttpPost]
    public async Task<ActionResult<VideoGame>> AddVideoGame(VideoGame request, CancellationToken ct)
    {
        if (request is null)
            return BadRequest();

        request.Id = await _context.VideoGames.MaxAsync(g => g.Id) + 1;

        _context.VideoGames.Add(request);
        await _context.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetVideoGameById), new { id = request.Id }, request);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateVideoGame(int id, VideoGame request, CancellationToken ct)
    {
        var game = await _context.VideoGames.FirstOrDefaultAsync(g => g.Id == id, ct);

        if (game is null)
            return NotFound();

        game.Title = request.Title;
        game.Platform = request.Platform;
        game.Developer = request.Developer;
        game.Publisher = request.Publisher;

        _context.VideoGames.Update(game);
        await _context.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteVideoGame(int id, CancellationToken ct)
    {
        var game = await _context.VideoGames.FirstOrDefaultAsync(g => g.Id == id, ct);

        if (game is null)
            return NotFound();

        _context.VideoGames.Remove(game);
        await _context.SaveChangesAsync(ct);
        return NoContent();
    }
}
