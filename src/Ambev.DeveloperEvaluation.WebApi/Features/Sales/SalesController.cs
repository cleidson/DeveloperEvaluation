using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize] //  Exige autenticação para acessar qualquer endpoint
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale. Only customers can create sales.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Customer")] // Apenas clientes podem criar vendas
    [ProducesResponseType(typeof(ApiResponseWithData<SaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] SaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new SaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var sale = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, _mapper.Map<SaleResponse>(sale));
    }

    /// <summary>
    /// Retrieves a sale by its ID. Customers and Managers can view sales.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Customer,Manager")] // 🔒 Clientes e gerentes podem visualizar vendas
    [ProducesResponseType(typeof(ApiResponseWithData<SaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSaleQuery { SaleId = id };
        var sale = await _mediator.Send(query, cancellationToken);

        if (sale == null)
            return NotFound("Venda não encontrada.");

        return Ok(_mapper.Map<SaleResponse>(sale));
    }

    /// <summary>
    /// Cancels a sale. Only managers or admins can cancel sales.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager,Admin")] // Apenas gerentes e administradores podem cancelar vendas
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CancelSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new CancelSaleCommand { SaleId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
            return BadRequest("Não foi possível cancelar a venda.");

        return Ok(new { Message = "Venda cancelada com sucesso." });
    }
}
