using AnimeCatalog.Application.Features.Anime.DTOs;
using AnimeCatalog.Domain.Interfaces;
using AnimeCatalog.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnimeCatalog.Application.Features.Anime.Commands.UpdateAnime;

public class UpdateAnimeCommandHandler : IRequestHandler<UpdateAnimeCommand, AnimeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateAnimeCommandHandler> _logger;

    public UpdateAnimeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateAnimeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AnimeDto> Handle(UpdateAnimeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Atualizando anime ID: {AnimeId}", request.Id);

            var existingAnime = await _unitOfWork.AnimeRepository.GetByIdAsync(request.Id);
            if (existingAnime == null)
            {
                _logger.LogWarning("Anime não encontrado para atualização. ID: {AnimeId}", request.Id);
                throw new AnimeNotFoundException(request.Id);
            }

            var duplicateAnime = await _unitOfWork.AnimeRepository.GetByNameAsync(request.AnimeDto.Name);
            if (duplicateAnime != null && duplicateAnime.Id != request.Id)
            {
                throw new AnimeAlreadyExistsException(request.AnimeDto.Name);
            }

            _mapper.Map(request.AnimeDto, existingAnime);
            existingAnime.UpdatedAt = DateTime.UtcNow;

            var updatedAnime = await _unitOfWork.AnimeRepository.UpdateAsync(existingAnime);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<AnimeDto>(updatedAnime);
            _logger.LogInformation("Anime atualizado com sucesso: {AnimeName}", updatedAnime.Name);

            return result;
        }
        catch (AnimeNotFoundException)
        {
            throw;
        }
        catch (AnimeAlreadyExistsException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao atualizar anime ID: {AnimeId}. Erro: {Error}", request.Id, ex.Message);
            throw new DatabaseException("update", "Erro ao atualizar anime no banco de dados", ex);
        }
    }
}