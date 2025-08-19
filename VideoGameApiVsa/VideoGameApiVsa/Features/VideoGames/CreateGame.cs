using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoGameApiVsa.Data;
using VideoGameApiVsa.Entities;

namespace VideoGameApiVsa.Features.VideoGames;

public static class CreateGame
{
    public record Command(string Title, string Genre, int ReleaseYear) : IRequest<Response>;

    public record Response(int Id, string Title, string Genre, int ReleaseYear);
    public class Handler(VideoGameDbContext context) : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(Command request, CancellationToken ct)
        {
            var videoGame = new VideoGame
            {
                Title = request.Title,
                Genre = request.Genre,
                ReleaseYear = request.ReleaseYear
            };
            context.VideoGames.Add(videoGame);

            await context.SaveChangesAsync(ct);

            return new Response(videoGame.Id, videoGame.Title, videoGame.Genre, videoGame.ReleaseYear);
        }
    }
}

[ApiController]
[Route("api/games")]
public class CreateGameController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateGame.Response>> GetAllGames(CreateGame.Command command)
    {
        var response = await sender.Send(command);
        return Ok(response);
    }
}