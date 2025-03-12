using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.CreateProduct;
/// <summary>
/// AutoMapper profile for product mappings.
/// </summary>
public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        // Mapeia ProductRequest para CreateProductCommand
        CreateMap<CreateProductRequest, CreateProductCommand>();

        // Mapeia Product para ProductResponse, garantindo que Id seja mapeado para ProductId
        CreateMap<Product, CreateProductResponse>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
    }
}