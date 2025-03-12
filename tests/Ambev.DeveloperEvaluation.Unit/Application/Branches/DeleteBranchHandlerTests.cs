using Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;
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

public class DeleteBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly DeleteBranchHandler _handler;

    public DeleteBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _handler = new DeleteBranchHandler(_branchRepository);
    }

    [Fact(DisplayName = "Given a valid branch When deleting Then deletes successfully")]
    public async Task Handle_ValidDelete_Success()
    {
        var command = new DeleteBranchCommand { BranchId = Guid.NewGuid() };
        _branchRepository.GetByIdAsync(command.BranchId).Returns(new Branch());

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().BeTrue();
        await _branchRepository.Received(1).DeleteAsync(Arg.Any<Branch>());
    }
}