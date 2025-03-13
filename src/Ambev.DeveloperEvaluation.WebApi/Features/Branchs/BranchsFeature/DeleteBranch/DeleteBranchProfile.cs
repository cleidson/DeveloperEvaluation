using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Branchs.DeleteBranch;

public class DeleteBranchProfile : Profile
{
    public DeleteBranchProfile()
    {
        CreateMap<DeleteBranchResult, DeleteBranchResponse>();
    }
}
