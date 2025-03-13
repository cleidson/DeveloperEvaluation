using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature.GetSale
{
    /// <summary>
    /// Profile for mapping Sale entity to GetSaleResult and GetSaleResponse.
    /// </summary>
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            // Mapeia Sale para GetSaleResult
            CreateMap<Sale, GetSaleResult>()
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems));

            CreateMap<SaleItem, GetSaleItemResult>();

            // Mapeia GetSaleResult para GetSaleResponse
            CreateMap<GetSaleResult, GetSaleResponse>();
            CreateMap<GetSaleItemResult, GetSaleItemResponse>();
        }
    }
}
