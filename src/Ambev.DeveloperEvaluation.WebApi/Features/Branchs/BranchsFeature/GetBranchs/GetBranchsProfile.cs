using Ambev.DeveloperEvaluation.Application.Branchs.GetBranchs;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.GetBranchs;
using AutoMapper;


namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.GetBranches
{
    /// <summary>
    /// AutoMapper profile for mapping GetBranchesResult to GetBranchesResponse.
    /// </summary>
    public class GetBranchsProfile : Profile
    {
        public GetBranchsProfile()
        {
            CreateMap<GetBranchsResult, GetBranchsResponse>()
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId));
        }
    }
}
