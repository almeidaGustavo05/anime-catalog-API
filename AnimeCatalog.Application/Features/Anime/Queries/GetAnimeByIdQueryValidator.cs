using FluentValidation;

namespace AnimeCatalog.Application.Features.Anime.Queries;

public class GetAnimeByIdQueryValidator : AbstractValidator<GetAnimeByIdQuery>
{
    public GetAnimeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("ID do anime inválido");
    }
}