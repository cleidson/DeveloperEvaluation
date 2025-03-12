using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.DeleteProduct;

/// <summary>
/// AutoMapper profile for DeleteProduct mapping.
/// </summary>
public class DeleteProductProfile : Profile
{
    public DeleteProductProfile()
    {
        CreateMap<DeleteProductResult, DeleteProductResponse>();
    }
}