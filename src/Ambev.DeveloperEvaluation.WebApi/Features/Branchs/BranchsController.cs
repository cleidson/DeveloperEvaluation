using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.CreateBranch;

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
    [Authorize(Roles = "Admin")] //  Apenas administradores podem criar filiais
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

        return Created("api/branches/" + response.Id, new ApiResponseWithData<CreateBranchResponse>
        {
            Success = true,
            Message = "Branch created successfully",
            Data = response
        });

    }


    /// <summary>
    /// Retrieves a branch by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateBranchResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBranch([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBranchQuery { BranchId = id };
        var branch = await _mediator.Send(query, cancellationToken);

        if (branch == null)
            return NotFound("Filial não encontrada.");

        return Ok(_mapper.Map<CreateBranchResponse>(branch));
    }

    /// <summary>
    /// Updates a branch. Only Managers and Admins can update branches.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Manager,Admin")] //  Apenas gerentes e administradores podem atualizar
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateBranch([FromRoute] Guid id, [FromBody] CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateBranchRequestValidator();
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
