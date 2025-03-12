using Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature;

/// <summary>
/// AutoMapper profile for branch mappings.
/// </summary>
public class BranchProfile : Profile
{
    public BranchProfile()
    { 
        CreateMap<Branch, BranchResponse>(); 
        CreateMap<BranchRequest, CreateBranchCommand>();

         
        CreateMap<Guid, BranchResponse>()  
            .ConvertUsing(src => new BranchResponse { Id = src });
    }
}
