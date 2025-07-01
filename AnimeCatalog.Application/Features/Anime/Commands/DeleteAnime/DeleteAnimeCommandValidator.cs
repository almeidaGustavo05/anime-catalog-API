using FluentValidation;

namespace AnimeCatalog.Application.Features.Anime.Commands.DeleteAnime;

public class DeleteAnimeCommandValidator : AbstractValidator<DeleteAnimeCommand>
{
    public DeleteAnimeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("ID do anime inválido");
    }
}