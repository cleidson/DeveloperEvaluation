using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.GetProducts
{
    /// <summary>
    /// AutoMapper profile for mapping GetProductsResult to GetProductsResponse.
    /// </summary>
    public class GetProductsProfile : Profile
    {
        public GetProductsProfile()
        {
            CreateMap<GetProductsResult, GetProductsResponse>();
        }
    }
}
