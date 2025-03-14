﻿using Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.CreateBranch;

/// <summary>
/// AutoMapper profile for branch mappings.
/// </summary>
public class GetBranchProfile : Profile
{
    public GetBranchProfile()
    { 
        CreateMap<Branch, CreateBranchResponse>(); 
        CreateMap<CreateBranchRequest, CreateBranchCommand>();

         
        CreateMap<Guid, CreateBranchResponse>()  
            .ConvertUsing(src => new CreateBranchResponse { Id = src });
    }
}
