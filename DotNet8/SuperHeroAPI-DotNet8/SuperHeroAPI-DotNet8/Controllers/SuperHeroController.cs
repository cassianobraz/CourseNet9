using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet8.Data;
using SuperHeroAPI_DotNet8.Entities;

namespace SuperHeroAPI_DotNet8.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{
    private readonly DataContext _context;

    public SuperHeroController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> GetAllHeroes(CancellationToken ct)
    {
        var heroes = await _context.SuperHeroes.ToListAsync(ct);

        return Ok(heroes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SuperHero>> GetHero(int id, CancellationToken ct)
    {
        var hero = await _context.SuperHeroes.FindAsync(id, ct);

        if (hero is null)
            return NotFound("Hero not found.");

        return Ok(hero);
    }

    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody] SuperHero request, CancellationToken ct)
    {
        _context.SuperHeroes.Add(request);
        await _context.SaveChangesAsync(ct);

        return Ok(await GetAllHeroes(ct));
    }

    [HttpPut]
    public async Task<ActionResult<List<SuperHero>>> UpdateHero([FromBody] SuperHero request, CancellationToken ct)
    {
        var hero = await _context.SuperHeroes.FindAsync(request.Id, ct);

        if (hero is null)
            return NotFound("Hero not found.");

        hero.Name = request.Name;
        hero.FirstName = request.FirstName;
        hero.LastName = request.LastName;
        hero.Place = request.Place;

        await _context.SaveChangesAsync(ct);
        return Ok(await _context.SuperHeroes.ToListAsync(ct));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id, CancellationToken ct)
    {
        var hero = await _context.SuperHeroes.FindAsync(id, ct);
        if (hero is null)
            return NotFound("Hero not found.");

        _context.SuperHeroes.Remove(hero);
        await _context.SaveChangesAsync(ct);
        return Ok(await _context.SuperHeroes.ToListAsync(ct));
    }
}
