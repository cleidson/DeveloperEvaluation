using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    /// <summary>
    /// Query to retrieve a list of products with optional filters.
    /// </summary>
    public class GetProductsQuery : IRequest<List<GetProductsResult>>
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        //public GetProductsQuery(string? name = null, decimal? minPrice = null, decimal? maxPrice = null)
        //{
        //    Name = name;
        //    MinPrice = minPrice;
        //    MaxPrice = maxPrice;
        //}
    }
}
