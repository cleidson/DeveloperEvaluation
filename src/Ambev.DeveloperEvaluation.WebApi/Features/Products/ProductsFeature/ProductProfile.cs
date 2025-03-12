using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature;
/// <summary>
/// AutoMapper profile for product mappings.
/// </summary>
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Mapeia ProductRequest para CreateProductCommand
        CreateMap<ProductRequest, CreateProductCommand>();

        // Mapeia Product para ProductResponse, garantindo que Id seja mapeado para ProductId
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id));
    }
}