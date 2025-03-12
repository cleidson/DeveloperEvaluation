using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature;

/// <summary>
/// AutoMapper profile for branch mappings.
/// </summary>
public sealed class BranchProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BranchProfile"/> class.
    /// </summary>
    public BranchProfile()
    {
        CreateMap<Branch, BranchResponse>()
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
    }
}