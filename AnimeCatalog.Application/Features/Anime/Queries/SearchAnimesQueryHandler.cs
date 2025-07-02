using AnimeCatalog.Application.Features.Anime.DTOs;
using AnimeCatalog.Domain.Interfaces;
using AnimeCatalog.Domain.Pagination;
using AnimeCatalog.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnimeCatalog.Application.Features.Anime.Queries;

public class SearchAnimesQueryHandler : IRequestHandler<SearchAnimesQuery, PageList<AnimeDto>>
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

    public async Task<PageList<AnimeDto>> Handle(SearchAnimesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Buscando animes com filtros - ID: {Id}, Nome: {Name}, Diretor: {Director}, Página: {PageNumber}", 
                request.Id, request.Name, request.Director, request.PageParams.PageNumber);
            
            var pagedAnimes = await _animeRepository.SearchAsync(
                request.PageParams, 
                request.Id, 
                request.Name, 
                request.Director);
            
            var animeDtos = _mapper.Map<List<AnimeDto>>(pagedAnimes.Items);
            
            var result = new PageList<AnimeDto>(
                animeDtos, 
                pagedAnimes.TotalCount, 
                pagedAnimes.CurrentPage, 
                pagedAnimes.PageSize);
            
            _logger.LogInformation("Encontrados {Count} animes de {TotalCount} total com os filtros aplicados", 
                animeDtos.Count, pagedAnimes.TotalCount);
                
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao buscar animes com filtros. Erro: {Error}", ex.Message);
            throw new DatabaseException("search", "Erro ao buscar animes no banco de dados", ex);
        }
    }
}