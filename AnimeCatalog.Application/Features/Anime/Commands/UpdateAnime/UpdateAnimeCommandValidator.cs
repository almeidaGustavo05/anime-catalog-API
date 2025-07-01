using FluentValidation;

namespace AnimeCatalog.Application.Features.Anime.Commands.UpdateAnime;

public class UpdateAnimeCommandValidator : AbstractValidator<UpdateAnimeCommand>
{
    public UpdateAnimeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID do anime inválido");

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