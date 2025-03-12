using Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch;
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

public class UpdateBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly UpdateBranchHandler _handler;

    public UpdateBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _handler = new UpdateBranchHandler(_branchRepository);
    }

    [Fact(DisplayName = "Given a valid branch update When handling Then updates branch successfully")]
    public async Task Handle_ValidUpdate_Success()
    {
        var command = new UpdateBranchCommand { BranchId = Guid.NewGuid(), Name = "Nova Filial", Address = "Rua Nova, 456" };
        var branch = new Branch { Id = command.BranchId, Name = "Antiga Filial", Address = "Rua Velha, 123" };

        _branchRepository.GetByIdAsync(command.BranchId).Returns(branch);
        _branchRepository.UpdateAsync(Arg.Any<Branch>()).Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().BeTrue();
        branch.Name.Should().Be(command.Name);
        await _branchRepository.Received(1).UpdateAsync(branch);
    }
}