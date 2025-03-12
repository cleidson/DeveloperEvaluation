using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.GetProducts
{
    /// <summary>
    /// Response model for listing products.
    /// </summary>
    public class GetProductsResponse
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
