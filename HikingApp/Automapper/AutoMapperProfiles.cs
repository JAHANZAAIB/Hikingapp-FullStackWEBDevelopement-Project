using AutoMapper;
using HikingApp.Models.Domain;
using HikingApp.Models.DTO;
using System.Linq;

namespace HikingApp.Automapper;


public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDTO>().ReverseMap();
        CreateMap<Region, AddRequestDto>().ReverseMap();
        CreateMap<Region, UpdateRequestDto>().ReverseMap();
        CreateMap<Walk,AddWalkRequestDto>().ReverseMap();
        CreateMap<Walk, WalkDto>()
            .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region.Name))
            .ForMember(dest => dest.DifficultyName, opt => opt.MapFrom(src => src.difficulty.Name))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Ratings.Any() ? src.Ratings.Average(r => r.Rating) : 0))
            .ForMember(dest => dest.RatingsCount, opt => opt.MapFrom(src => src.Ratings.Count));
        CreateMap<WalkDto, Walk>();
        CreateMap<Difficulty,DifficultyDto>().ReverseMap();
        CreateMap<Walk,UpdateWalksDto>().ReverseMap();
        CreateMap<Image,ImageResponseDto>().ReverseMap();

    }
}
