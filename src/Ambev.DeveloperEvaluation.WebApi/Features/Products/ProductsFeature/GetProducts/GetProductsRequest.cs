namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.GetProducts
{
    /// <summary>
    /// Request model to filter the product list.
    /// </summary>
    public class GetProductsRequest
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
