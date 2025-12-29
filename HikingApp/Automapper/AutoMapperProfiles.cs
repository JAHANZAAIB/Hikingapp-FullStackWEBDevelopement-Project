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

        // Walk -> AddWalkRequestDto
        CreateMap<Walk, AddWalkRequestDto>()
            .ForMember(dest => dest.RouteGeometry, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.RouteGeometry : null))
            .ForMember(dest => dest.ElevationGainMeters, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.ElevationGainMeters : null))
            .ForMember(dest => dest.EstimatedDurationMinutes, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.EstimatedDurationMinutes : null))
            .ForMember(dest => dest.IsAccessible, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.IsAccessible : true))
            .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.Features : null));

        // AddWalkRequestDto -> Walk
        CreateMap<AddWalkRequestDto, Walk>()
            .ForMember(dest => dest.WalkDetails, opt => opt.MapFrom(src => new WalkDetails
            {
                RouteGeometry = src.RouteGeometry,
                ElevationGainMeters = src.ElevationGainMeters,
                EstimatedDurationMinutes = src.EstimatedDurationMinutes,
                IsAccessible = src.IsAccessible,
                Features = src.Features ?? new List<string>()
            }));

        CreateMap<Walk, WalkDto>()
            .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region.Name))
            .ForMember(dest => dest.DifficultyName, opt => opt.MapFrom(src => src.difficulty.Name))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Ratings.Any() ? src.Ratings.Average(r => r.Rating) : 0))
            .ForMember(dest => dest.RatingsCount, opt => opt.MapFrom(src => src.Ratings.Count))
            // Map from WalkDetails
            .ForMember(dest => dest.RouteGeometry, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.RouteGeometry : null))
            .ForMember(dest => dest.ElevationGainMeters, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.ElevationGainMeters : null))
            .ForMember(dest => dest.EstimatedDurationMinutes, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.EstimatedDurationMinutes : null))
            .ForMember(dest => dest.IsAccessible, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.IsAccessible : true))
            .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.Features : null));

        CreateMap<WalkDto, Walk>()
             .ForMember(dest => dest.WalkDetails, opt => opt.MapFrom(src => new WalkDetails
             {
                 RouteGeometry = src.RouteGeometry,
                 ElevationGainMeters = src.ElevationGainMeters,
                 EstimatedDurationMinutes = src.EstimatedDurationMinutes,
                 IsAccessible = src.IsAccessible,
                 Features = src.Features ?? new List<string>()
             }));

        CreateMap<Difficulty, DifficultyDto>().ReverseMap();

        // Walk -> UpdateWalksDto
        CreateMap<Walk, UpdateWalksDto>()
            .ForMember(dest => dest.RouteGeometry, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.RouteGeometry : null))
            .ForMember(dest => dest.ElevationGainMeters, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.ElevationGainMeters : null))
            .ForMember(dest => dest.EstimatedDurationMinutes, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.EstimatedDurationMinutes : null))
            .ForMember(dest => dest.IsAccessible, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.IsAccessible : true))
            .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.WalkDetails != null ? src.WalkDetails.Features : null));

        // UpdateWalksDto -> Walk
        CreateMap<UpdateWalksDto, Walk>()
            .ForMember(dest => dest.WalkDetails, opt => opt.MapFrom(src => new WalkDetails
            {
                RouteGeometry = src.RouteGeometry,
                ElevationGainMeters = src.ElevationGainMeters,
                EstimatedDurationMinutes = src.EstimatedDurationMinutes,
                IsAccessible = src.IsAccessible,
                Features = src.Features ?? new List<string>()
            }));

        CreateMap<Image, ImageResponseDto>().ReverseMap();

    }
}
