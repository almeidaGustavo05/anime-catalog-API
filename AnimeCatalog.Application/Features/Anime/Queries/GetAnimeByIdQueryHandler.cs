using AnimeCatalog.Application.Features.Anime.DTOs;
using AnimeCatalog.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnimeCatalog.Application.Features.Anime.Queries;

public class GetAnimeByIdQueryHandler : IRequestHandler<GetAnimeByIdQuery, AnimeDto?>
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAnimeByIdQueryHandler> _logger;

    public GetAnimeByIdQueryHandler(
        IAnimeRepository animeRepository,
        IMapper mapper,
        ILogger<GetAnimeByIdQueryHandler> logger)
    {
        _animeRepository = animeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AnimeDto?> Handle(GetAnimeByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Buscando anime com ID: {AnimeId}", request.Id);
            
            var anime = await _animeRepository.GetByIdAsync(request.Id);
            
            if (anime == null)
            {
                _logger.LogWarning("Anime com ID {AnimeId} não encontrado", request.Id);
                return null;
            }
            
            var animeDto = _mapper.Map<AnimeDto>(anime);
            _logger.LogInformation("Anime {AnimeName} encontrado com sucesso", anime.Name);
            
            return animeDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar anime com ID: {AnimeId}", request.Id);
            throw;
        }
    }
}