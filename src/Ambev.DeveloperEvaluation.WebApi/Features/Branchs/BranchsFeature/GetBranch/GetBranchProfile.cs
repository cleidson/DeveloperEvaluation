using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Branchs.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.GetBranch;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.GetBranch
{
    /// <summary>
    /// AutoMapper profile for mapping Branch entity to GetBranchResponse.
    /// </summary>
    public class GetBranchProfile : Profile
    {
        public GetBranchProfile()
        {
            CreateMap<Branch, GetBranchResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

            CreateMap<GetBranchResult, GetBranchResponse>();
        }
    }
}
