using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.GetProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature.UpdateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller for managing products.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize] //  Todos os endpoints exigem autenticação
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new product. Only Admins can create products.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")] //  Apenas administradores podem criar produtos
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateProductCommand>(request);
        var productId = await _mediator.Send(command, cancellationToken);

        var productResponse = new CreateProductResponse
        {
            ProductId = productId, 
            Name = request.Name,
            Price = request.Price
        };

        return Ok(productResponse);
    }

     

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")] 
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductQuery { ProductId = id };
        var product = await _mediator.Send(query, cancellationToken);

        if (product == null)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = "Produto não encontrado."
            });
        }

        var response = _mapper.Map<GetProductResponse>(product);

        return Ok(response);
    }




    /// <summary>
    /// Retrieves a list of products based on optional filters.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")] // Apenas administradores podem listar produtos
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetProductsResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request, CancellationToken cancellationToken)
    {
        var query = new GetProductsQuery
        {
            Name = request.Name,
            MinPrice = request.MinPrice,
            MaxPrice = request.MaxPrice
        };

        var products = await _mediator.Send(query, cancellationToken);

        if (products == null || products.Count == 0)
        {
            return Ok(new ApiResponseWithData<List<GetProductsResponse>>
            {
                Success = false,
                Message = "Nenhum produto encontrado.",
                Data = new List<GetProductsResponse>()
            });
        }

        var response = _mapper.Map<List<GetProductsResponse>>(products);

        return Ok(response); 
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] // Apenas administradores podem deletar produtos
    [ProducesResponseType(typeof(ApiResponseWithData<DeleteProductResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand { ProductId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Success)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = result.Message
            });
        }

        var response = _mapper.Map<DeleteProductResult>(result);

        return Ok(response); 
    }

    /// <summary>
    /// Updates a product. Only Admins can update products.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] // Apenas administradores podem atualizar produtos
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateProductCommand>(request);
        command.ProductId = id;

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Success)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = result.Message
            });
        }
        var response = _mapper.Map<UpdateProductResult>(result);

        return Ok(response);
    }
} 
