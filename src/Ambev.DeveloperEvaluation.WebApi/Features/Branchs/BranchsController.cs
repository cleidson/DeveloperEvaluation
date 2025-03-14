﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.DeleteBranch;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.GetBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.GetBranchs;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.GetBranchs;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature.UpdateBranch;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches;

/// <summary>
/// Controller for managing branches.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize] //  Todos os endpoints exigem autenticação
public class BranchsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BranchsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new branch. Only Admins can create branches.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateBranchResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateBranchRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateBranchCommand>(request);
        var branch = await _mediator.Send(command, cancellationToken); // Aqui deve retornar um objeto Branch

        // Converte para resposta corretamente
        var response = _mapper.Map<CreateBranchResponse>(branch);

        return Ok(response);

    }


    /// <summary>
    /// Retrieves a branch by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetBranchResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBranch([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBranchQuery { BranchId = id };
        var branch = await _mediator.Send(query, cancellationToken);

        if (branch == null)
            return NotFound("Filial não encontrada.");

        return Ok(_mapper.Map<GetBranchResponse>(branch));
    }

    /// <summary>
    /// Retrieves a list of branches based on optional filters.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetBranchsResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBranchs([FromQuery] GetBranchsRequest request, CancellationToken cancellationToken)
    {
        var query = new GetBranchsQuery
        {
            Name = request.Name,
            Location = request.Location
        };

        var branchs = await _mediator.Send(query, cancellationToken);

        if (branchs == null || branchs.Count == 0)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = "Nenhuma filial encontrada."
            });
        }

        var response = _mapper.Map<List<GetBranchsResponse>>(branchs);

        return Ok(response);
    }

    /// <summary>
    /// Updates a branch. Only Managers and Admins can update branches.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateBranchResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBranch([FromRoute] Guid id, [FromBody] UpdateBranchRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateBranchCommand>(request);
        command.BranchId = id;

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Success)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = result.Message
            });
        }

        return Ok(_mapper.Map<UpdateBranchResponse>(result));
    }



    /// <summary>
    /// Deletes a branch by its ID.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(ApiResponseWithData<DeleteBranchResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteBranch([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteBranchCommand { BranchId = id };
        var result = await _mediator.Send(command, cancellationToken);

    
        var response = _mapper.Map<DeleteBranchResponse>(result);

        return Ok(response); 
    }

}

