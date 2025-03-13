using Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branches
{
    public class UpdateBranchHandlerTests
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IValidator<UpdateBranchCommand> _validator;
        private readonly UpdateBranchHandler _handler;

        public UpdateBranchHandlerTests()
        {
            _branchRepository = Substitute.For<IBranchRepository>();
            _validator = Substitute.For<IValidator<UpdateBranchCommand>>();
            _handler = new UpdateBranchHandler(_branchRepository, _validator);
        }

        [Fact(DisplayName = "Given a valid branch update When handling Then updates branch successfully")]
        public async Task Handle_ValidUpdate_Success()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var command = new UpdateBranchCommand { BranchId = branchId, Name = "Nova Filial", Address = "Rua Nova, 456" };
            var branch = new Branch { Id = branchId, Name = "Antiga Filial", Address = "Rua Velha, 123" };

            _validator.ValidateAsync(command, CancellationToken.None).Returns(new ValidationResult()); // Validação sem erros
            _branchRepository.GetByIdAsync(branchId).Returns(branch);
            _branchRepository.UpdateAsync(Arg.Any<Branch>()).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Filial atualizada com sucesso.");
            result.BranchId.Should().Be(branchId);
            branch.Name.Should().Be(command.Name);
            branch.Address.Should().Be(command.Address);
            await _branchRepository.Received(1).UpdateAsync(branch);
        }

        [Fact(DisplayName = "Given a non-existing branch When updating Then returns error")]
        public async Task Handle_BranchNotFound_Failure()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var command = new UpdateBranchCommand { BranchId = branchId, Name = "Nova Filial", Address = "Rua Nova, 456" };

            _validator.ValidateAsync(command, CancellationToken.None).Returns(new ValidationResult());
            _branchRepository.GetByIdAsync(branchId).Returns((Branch)null); // Simulando que a filial não existe

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Filial não encontrada.");
            await _branchRepository.DidNotReceive().UpdateAsync(Arg.Any<Branch>());
        }

        [Fact(DisplayName = "Given an invalid request When updating Then returns validation error")]
        public async Task Handle_InvalidRequest_ValidationFailure()
        {
            // Arrange
            var command = new UpdateBranchCommand { BranchId = Guid.Empty, Name = "", Address = "" };
            var validationErrors = new ValidationResult(new[]
            {
                new ValidationFailure("BranchId", "O ID da filial é inválido."),
                new ValidationFailure("Name", "O nome da filial é obrigatório."),
                new ValidationFailure("Address", "O endereço da filial é obrigatório.")
            });

            _validator.ValidateAsync(command, CancellationToken.None).Returns(validationErrors);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Requisição inválida:");
            await _branchRepository.DidNotReceive().UpdateAsync(Arg.Any<Branch>());
        }
    }
}
