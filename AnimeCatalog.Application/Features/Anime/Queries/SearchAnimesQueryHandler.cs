using AnimeCatalog.Application.Features.Anime.DTOs;
using AnimeCatalog.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnimeCatalog.Application.Features.Anime.Queries;

public class SearchAnimesQueryHandler : IRequestHandler<SearchAnimesQuery, IEnumerable<AnimeDto>>
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SearchAnimesQueryHandler> _logger;

    public SearchAnimesQueryHandler(
        IAnimeRepository animeRepository,
        IMapper mapper,
        ILogger<SearchAnimesQueryHandler> logger)
    {
        _animeRepository = animeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<AnimeDto>> Handle(SearchAnimesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Buscando animes com filtros - ID: {Id}, Nome: {Name}, Diretor: {Director}", 
                request.Id, request.Name, request.Director);
            
            var animes = await _animeRepository.SearchAsync(request.Id, request.Name, request.Director);
            var animeDtos = _mapper.Map<IEnumerable<AnimeDto>>(animes);
            
            _logger.LogInformation("Encontrados {Count} animes com os filtros aplicados", animeDtos.Count());
            return animeDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar animes com filtros");
            throw;
        }
    }
}