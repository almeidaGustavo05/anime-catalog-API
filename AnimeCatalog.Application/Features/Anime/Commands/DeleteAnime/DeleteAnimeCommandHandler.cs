using AnimeCatalog.Application.Features.Anime.Commands.DeleteAnime;
using AnimeCatalog.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnimeCatalog.Application.Features.Anime.Commands.DeleteAnime;

public class DeleteAnimeCommandHandler : IRequestHandler<DeleteAnimeCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteAnimeCommandHandler> _logger;

    public DeleteAnimeCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteAnimeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAnimeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Excluindo anime ID: {AnimeId}", request.Id);

            var exists = await _unitOfWork.AnimeRepository.ExistsAsync(request.Id);
            if (!exists)
            {
                _logger.LogWarning("Anime não encontrado para exclusão. ID: {AnimeId}", request.Id);
                return false;
            }

            var result = await _unitOfWork.AnimeRepository.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();

            if (result)
            {
                _logger.LogInformation("Anime excluído com sucesso. ID: {AnimeId}", request.Id);
            }
            else
            {
                _logger.LogWarning("Falha ao excluir anime. ID: {AnimeId}", request.Id);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir anime ID: {AnimeId}", request.Id);
            throw;
        }
    }
}