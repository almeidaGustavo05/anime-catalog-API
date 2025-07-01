using AnimeCatalog.Application.Features.Anime.DTOs;
using AnimeCatalog.Domain.Entities;
using AnimeCatalog.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnimeCatalog.Application.Features.Anime.Commands.CreateAnime;

public class CreateAnimeHandler : IRequestHandler<CreateAnimeCommand, AnimeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAnimeHandler> _logger;

    public CreateAnimeHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateAnimeHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AnimeDto> Handle(CreateAnimeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Criando novo anime: {AnimeName}", request.AnimeDto.Name);

            var anime = _mapper.Map<Domain.Entities.Anime>(request.AnimeDto);
            anime.CreatedAt = DateTime.UtcNow;
            anime.UpdatedAt = DateTime.UtcNow;

            var createdAnime = await _unitOfWork.AnimeRepository.CreateAsync(anime);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Anime criado com sucesso. ID: {AnimeId}", createdAnime.Id);

            return _mapper.Map<AnimeDto>(createdAnime);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar anime: {AnimeName}", request.AnimeDto.Name);
            throw;
        }
    }
}