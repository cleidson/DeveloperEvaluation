using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature.GetSale
{
    /// <summary>
    /// Request model for retrieving a sale by ID.
    /// </summary>
    public class GetSaleRequest
    {
        public Guid SaleId { get; set; }
    }
}
