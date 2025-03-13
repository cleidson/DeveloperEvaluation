using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature;
using AutoMapper;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>()
            .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.Items));

        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
    }
}
