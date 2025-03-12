using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branches;

/// <summary>
/// Contains unit tests for the <see cref="CreateUserHandler"/> class.
/// </summary>
public class GetBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly GetBranchHandler _handler;

    public GetBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _handler = new GetBranchHandler(_branchRepository);
    }

    [Fact(DisplayName = "Given a valid branch ID When retrieving Then returns branch details")]
    public async Task Handle_ValidRequest_ReturnsBranch()
    {
        var branchId = Guid.NewGuid();
        var branch = new Branch { Id = branchId, Name = "Filial Centro", Address = "Rua A, 123" };

        _branchRepository.GetByIdAsync(branchId).Returns(branch);

        var result = await _handler.Handle(new GetBranchQuery { BranchId = branchId }, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(branch.Id);
    }
}
