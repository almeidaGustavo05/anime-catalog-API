using FluentValidation;

namespace AnimeCatalog.Application.Features.Anime.Commands.CreateAnime;

public class CreateAnimeCommandValidator : AbstractValidator<CreateAnimeCommand>
{
    public CreateAnimeCommandValidator()
    {
        RuleFor(x => x.AnimeDto.Name)
            .NotEmpty().WithMessage("O nome do anime é obrigatório")
            .MaximumLength(200).WithMessage("O nome do anime não pode ter mais que 200 caracteres");

        RuleFor(x => x.AnimeDto.Director)
            .NotEmpty().WithMessage("O diretor do anime é obrigatório")
            .MaximumLength(100).WithMessage("O nome do diretor não pode ter mais que 100 caracteres");

        RuleFor(x => x.AnimeDto.Summary)
            .NotEmpty().WithMessage("O resumo do anime é obrigatório")
            .MaximumLength(1000).WithMessage("O resumo não pode ter mais que 1000 caracteres");
    }
}