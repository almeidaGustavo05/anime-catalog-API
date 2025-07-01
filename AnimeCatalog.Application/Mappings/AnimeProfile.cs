using AnimeCatalog.Application.DTOs;
using AnimeCatalog.Domain.Entities;
using AutoMapper;

namespace AnimeCatalog.Application.Mappings;

public class AnimeProfile : Profile
{
    public AnimeProfile()
    {
        CreateMap<Anime, AnimeDto>();
        CreateMap<CreateAnimeDto, Anime>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        CreateMap<UpdateAnimeDto, Anime>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}