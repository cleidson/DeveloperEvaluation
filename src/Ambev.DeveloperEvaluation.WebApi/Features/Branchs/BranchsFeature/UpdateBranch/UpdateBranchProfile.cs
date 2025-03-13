using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.UpdateBranch;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.UpdateBranch
{
    /// <summary>
    /// AutoMapper profile for UpdateBranch.
    /// </summary>
    public class UpdateBranchProfile : Profile
    {
        public UpdateBranchProfile()
        {
            CreateMap<UpdateBranchRequest, UpdateBranchCommand>();
            CreateMap<UpdateBranchResult, UpdateBranchResponse>();
        }
    }
}
