using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
using Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches;

/// <summary>
/// Controller for managing branches.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize] //  Todos os endpoints exigem autenticação
public class BranchesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BranchesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new branch. Only Admins can create branches.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")] //  Apenas administradores podem criar filiais
    [ProducesResponseType(typeof(ApiResponseWithData<BranchResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBranch([FromBody] BranchRequest request, CancellationToken cancellationToken)
    {
        var validator = new BranchRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateBranchCommand>(request);
        var branch = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetBranch), new { id = branch }, _mapper.Map<BranchResponse>(branch));
    }

    /// <summary>
    /// Retrieves a branch by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<BranchResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBranch([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBranchQuery { BranchId = id };
        var branch = await _mediator.Send(query, cancellationToken);

        if (branch == null)
            return NotFound("Filial não encontrada.");

        return Ok(_mapper.Map<BranchResponse>(branch));
    }

    /// <summary>
    /// Updates a branch. Only Managers and Admins can update branches.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Manager,Admin")] //  Apenas gerentes e administradores podem atualizar
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateBranch([FromRoute] Guid id, [FromBody] BranchRequest request, CancellationToken cancellationToken)
    {
        var validator = new BranchRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateBranchCommand>(request);
        command.BranchId = id;
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
            return BadRequest("Não foi possível atualizar a filial.");

        return Ok(new { Message = "Filial atualizada com sucesso." });
    }
}
