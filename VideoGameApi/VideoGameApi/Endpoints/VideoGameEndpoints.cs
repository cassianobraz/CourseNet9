using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;

namespace VideoGameApi.Endpoints;

public static class VideoGameEndpoints
{
    public static RouteGroupBuilder MapVideoGameEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (VideoGameDbContext context, CancellationToken ct) =>
            await context.VideoGames.ToListAsync(ct));

        group.MapGet("/{id:int}", async (VideoGameDbContext context, int id, CancellationToken ct) =>
        {
            var game = await context.VideoGames.FindAsync(id, ct);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        });

        group.MapPost("/", async (VideoGameDbContext context, VideoGame newGame, CancellationToken ct) =>
        {
            if (newGame is null)
                return Results.BadRequest();

            context.VideoGames.Add(newGame);
            await context.SaveChangesAsync(ct);

            return Results.Created($"/api/videoGames/{newGame.Id}", newGame);
        });

        group.MapPut("/{id:int}", async (VideoGameDbContext context, int id, VideoGame updatedGame, CancellationToken ct) =>
        {
            var game = await context.VideoGames.FindAsync(id, ct);
            if (game is null)
                return Results.NotFound();

            game.Title = updatedGame.Title;
            game.Platform = updatedGame.Platform;
            game.Developer = updatedGame.Developer;
            game.Publisher = updatedGame.Publisher;

            await context.SaveChangesAsync(ct);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int}", async (VideoGameDbContext context, int id, CancellationToken ct) =>
        {
            var game = await context.VideoGames.FindAsync(id, ct);
            if (game is null)
                return Results.NotFound();

            context.VideoGames.Remove(game);
            await context.SaveChangesAsync(ct);

            return Results.NoContent();
        });

        return group;
    }
}
