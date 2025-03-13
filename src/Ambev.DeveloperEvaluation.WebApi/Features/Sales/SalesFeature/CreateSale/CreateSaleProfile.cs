using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature
{
    /// <summary>
    /// AutoMapper profile for CreateSale mapping.
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>();

            // Mapeamento correto dos itens da venda
            CreateMap<CreateSaleItemRequest, SaleItem>()
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()); // Será calculado no handler
        }
    }
}
