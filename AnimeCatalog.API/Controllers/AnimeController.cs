using AnimeCatalog.Application.Features.Anime.Commands.CreateAnime;
using AnimeCatalog.Application.Features.Anime.Commands.DeleteAnime;
using AnimeCatalog.Application.Features.Anime.Commands.UpdateAnime;
using AnimeCatalog.Application.Features.Anime.DTOs;
using AnimeCatalog.Application.Features.Anime.Queries;
using AnimeCatalog.Domain.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimeCatalog.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnimeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PageList<AnimeDto>>> GetAllAnimes([FromQuery] PageParams pageParams)
    {
        var query = new GetAllAnimesQuery(pageParams);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AnimeDto>> GetAnimeById(int id)
    {
        var query = new GetAnimeByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<PageList<AnimeDto>>> SearchAnimes(
        [FromQuery] PageParams pageParams, 
        [FromQuery] int? id = null, 
        [FromQuery] string? name = null, 
        [FromQuery] string? director = null)
    {
        var query = new SearchAnimesQuery(pageParams, id, name, director);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AnimeDto>> CreateAnime([FromBody] CreateAnimeDto animeDto)
    {
        var command = new CreateAnimeCommand(animeDto);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAnimeById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AnimeDto>> UpdateAnime(int id, [FromBody] UpdateAnimeDto animeDto)
    {
        var command = new UpdateAnimeCommand(id, animeDto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAnime(int id)
    {
        var command = new DeleteAnimeCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}