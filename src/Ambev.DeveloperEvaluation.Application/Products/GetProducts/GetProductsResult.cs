using System;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    /// <summary>
    /// Represents a product in the response list.
    /// </summary>
    public class GetProductsResult
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
