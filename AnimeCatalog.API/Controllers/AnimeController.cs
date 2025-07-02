using AnimeCatalog.Application.Features.Anime.Commands.CreateAnime;
using AnimeCatalog.Application.Features.Anime.Commands.DeleteAnime;
using AnimeCatalog.Application.Features.Anime.Commands.UpdateAnime;
using AnimeCatalog.Application.Features.Anime.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeCatalog.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AnimeController> _logger;

    public AnimeController(IMediator mediator, ILogger<AnimeController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnimes()
    {
        _logger.LogInformation("Buscando todos os animes");
        var query = new GetAllAnimesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAnimeById(int id)
    {
        _logger.LogInformation("Buscando anime com ID: {AnimeId}", id);
        var query = new GetAnimeByIdQuery(id);
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFound($"Anime com ID {id} não encontrado");
        }
        
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAnimes([FromQuery] int? id = null, [FromQuery] string? name = null, [FromQuery] string? director = null)
    {
        _logger.LogInformation("Buscando animes com critérios - ID: {Id}, Nome: {Name}, Diretor: {Director}", id, name, director);
        var query = new SearchAnimesQuery(id, name, director);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnime([FromBody] CreateAnimeCommand command)
    {
        _logger.LogInformation("Criando novo anime: {AnimeName}", command.AnimeDto.Name);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAnimeById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAnime(int id, [FromBody] UpdateAnimeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID do anime não confere com o ID da URL");
        }

        _logger.LogInformation("Atualizando anime: {AnimeId}", id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAnime(int id)
    {
        _logger.LogInformation("Removendo anime: {AnimeId}", id);
        var command = new DeleteAnimeCommand(id);
        var result = await _mediator.Send(command);
        
        if (!result)
        {
            return NotFound($"Anime com ID {id} não encontrado");
        }
        
        return NoContent();
    }
}