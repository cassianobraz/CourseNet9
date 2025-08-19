using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoGameApiVsa.Data;

namespace VideoGameApiVsa.Features.VideoGames;

public static class UpdateGame
{
    public record Command(int Id, string Title, string Genre, int ReleaseYear) : IRequest<Response?>;
    public record Response(int Id, string Title, string Genre, int ReleaseYear);

    public class Handler(VideoGameDbContext context) : IRequestHandler<Command, Response?>
    {
        public async Task<Response?> Handle(Command request, CancellationToken ct)
        {
            var videoGame = await context.VideoGames.FindAsync(request.Id);

            if (videoGame is null)
                return null;

            videoGame.Title = request.Title;
            videoGame.Genre = request.Genre;
            videoGame.ReleaseYear = request.ReleaseYear;

            await context.SaveChangesAsync(ct);

            return new Response(videoGame.Id, videoGame.Title, videoGame.Genre, videoGame.ReleaseYear);
        }
    }
}

[ApiController]
[Route("api/games")]
public class UpdateGameController(ISender sender) : ControllerBase
{
    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateGame.Response>> GetAllGames(int id, UpdateGame.Command command)
    {
        if (id != command.Id)
            return BadRequest("Id in the route and body do not match.");

        var response = await sender.Send(command);
        if (response is null)
            return NotFound("Video game with given Id not found.");

        return Ok(response);
    }
}