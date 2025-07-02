using AnimeCatalog.Application.Features.Anime.DTOs;
using AnimeCatalog.Domain.Interfaces;
using AnimeCatalog.Domain.Pagination;
using AnimeCatalog.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnimeCatalog.Application.Features.Anime.Queries;

public class GetAllAnimesQueryHandler : IRequestHandler<GetAllAnimesQuery, PageList<AnimeDto>>
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

    public async Task<PageList<AnimeDto>> Handle(GetAllAnimesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Buscando todos os animes - Página: {PageNumber}, Tamanho: {PageSize}", 
                request.PageParams.PageNumber, request.PageParams.PageSize);
            
            var pagedAnimes = await _animeRepository.GetAllAsync(request.PageParams);
            var animeDtos = _mapper.Map<List<AnimeDto>>(pagedAnimes.Items);
            
            var result = new PageList<AnimeDto>(
                animeDtos, 
                pagedAnimes.TotalCount, 
                pagedAnimes.CurrentPage, 
                pagedAnimes.PageSize);
            
            _logger.LogInformation("Encontrados {Count} animes de {TotalCount} total", 
                animeDtos.Count, pagedAnimes.TotalCount);
                
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao buscar todos os animes. Erro: {Error}", ex.Message);
            throw new DatabaseException("getAll", "Erro ao buscar animes no banco de dados", ex);
        }
    }
}