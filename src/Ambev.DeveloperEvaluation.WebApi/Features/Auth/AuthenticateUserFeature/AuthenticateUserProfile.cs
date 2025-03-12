using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Mapping;

/// <summary>
/// AutoMapper profile for authentication-related mappings.
/// </summary>
public sealed class AuthenticateUserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateUserProfile"/> class.
    /// </summary>
    public AuthenticateUserProfile()
    {
        // Mapeamento de User para AuthenticateUserResponse
        CreateMap<User, AuthenticateUserResponse>()
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

        // Mapeamento de AuthenticateUserRequest para AuthenticateUserCommand
        CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();

        // Mapeamento de AuthenticateUserResult para AuthenticateUserResponse (RESOLVE O ERRO)
        CreateMap<AuthenticateUserResult, AuthenticateUserResponse>();
    }
}
