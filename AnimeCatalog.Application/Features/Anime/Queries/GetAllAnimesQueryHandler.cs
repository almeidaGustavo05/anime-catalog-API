using AnimeCatalog.Application.DTOs;
using AnimeCatalog.Application.Features.Anime.Queries;
using AnimeCatalog.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnimeCatalog.Application.Features.Anime.Queries;

public class GetAllAnimesQueryHandler : IRequestHandler<GetAllAnimesQuery, IEnumerable<AnimeDto>>
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllAnimesQueryHandler> _logger;

    public GetAllAnimesQueryHandler(
        IAnimeRepository animeRepository,
        IMapper mapper,
        ILogger<GetAllAnimesQueryHandler> logger)
    {
        _animeRepository = animeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<AnimeDto>> Handle(GetAllAnimesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Buscando todos os animes");
            
            var animes = await _animeRepository.GetAllAsync();
            var animeDtos = _mapper.Map<IEnumerable<AnimeDto>>(animes);
            
            _logger.LogInformation("Encontrados {Count} animes", animeDtos.Count());
            return animeDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar todos os animes");
            throw;
        }
    }
}