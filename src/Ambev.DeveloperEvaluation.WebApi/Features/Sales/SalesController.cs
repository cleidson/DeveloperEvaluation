﻿using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature.GetSales;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneOf.Types;

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
    /// <summary>
    /// Creates a new sale. Only customers can create sales.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Customer")] 
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateSaleCommand>(request);
        var saleResult = await _mediator.Send(command, cancellationToken);

        if (saleResult == null)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "Não foi possível criar a venda."
            });
        }

        var response = _mapper.Map<CreateSaleResult>(saleResult);

        return Ok(response);
    }


    /// <summary>
    /// Retrieves a sale by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Customer,Manager,Admin")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSaleQuery { SaleId = id };
        var sale = await _mediator.Send(query, cancellationToken);

        if (sale == null)
            return NotFound(new ApiResponse { Success = false, Message = "Venda não encontrada." });

        return Ok(sale);
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


    /// <summary>
    /// Retorna a lista de vendas com filtros opcionais.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Manager,Admin")] // Apenas gerentes e administradores podem visualizar vendas
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetSales([FromQuery] GetSalesRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetSalesQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null || result.Count == 0)
            return NotFound(new { Message = "Nenhuma venda encontrada para os filtros informados." });

        return Ok(result);
    }


    /// <summary>
    /// Updates a sale.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Manager,Admin")] 
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        if (id != request.SaleId)
            return BadRequest("O ID da venda na URL deve ser igual ao ID da requisição.");

        var command = _mapper.Map<UpdateSaleCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);


        if (!result.Success)
            return BadRequest(result);


        return Ok(result);
    }

}
