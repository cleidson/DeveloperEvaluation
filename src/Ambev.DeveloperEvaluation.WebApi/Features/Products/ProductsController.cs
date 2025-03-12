using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature;

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
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateProductCommand>(request);
        var productId = await _mediator.Send(command, cancellationToken);

        var productResponse = new ProductResponse
        {
            ProductId = productId, // Ajustando para o nome correto
            Name = request.Name,
            Price = request.Price
        };

        return CreatedAtAction(nameof(GetProduct), new { id = productId }, new ApiResponseWithData<ProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = productResponse
        });
    }

    /// <summary>
    /// Retrieves a product by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductQuery { ProductId = id };
        var product = await _mediator.Send(query, cancellationToken);

        if (product == null)
            return NotFound("Produto não encontrado.");

        return Ok(_mapper.Map<ProductResponse>(product));
    }

    /// <summary>
    /// Updates a product. Only Admins can update products.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] //  Apenas administradores podem atualizar produtos
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] ProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new ProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateProductCommand>(request);
        command.ProductId = id;
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
            return BadRequest("Não foi possível atualizar o produto.");

        return Ok(new { Message = "Produto atualizado com sucesso." });
    }
}
