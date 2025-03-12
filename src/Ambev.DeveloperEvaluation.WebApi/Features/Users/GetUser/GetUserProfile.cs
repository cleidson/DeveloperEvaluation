using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Profile for mapping GetUser feature requests to commands
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<GetUserRequest, GetUserCommand>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        CreateMap<GetUserResult, GetUserResponse>(); // Garantindo que a resposta também seja mapeada corretamente
    }
}
