using Ambev.DeveloperEvaluation.Application.Branchs.DeleteBranch;
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
    public class DeleteBranchHandlerTests
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IValidator<DeleteBranchCommand> _validator;
        private readonly DeleteBranchHandler _handler;

        public DeleteBranchHandlerTests()
        {
            _branchRepository = Substitute.For<IBranchRepository>();
            _validator = Substitute.For<IValidator<DeleteBranchCommand>>();
            _handler = new DeleteBranchHandler(_branchRepository, _validator);
        }

        [Fact(DisplayName = "Given a valid branch When deleting Then deletes successfully")]
        public async Task Handle_ValidDelete_Success()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var command = new DeleteBranchCommand { BranchId = branchId };

            _validator.ValidateAsync(command, CancellationToken.None)
                      .Returns(new ValidationResult()); // Simula validação bem-sucedida

            _branchRepository.GetByIdAsync(branchId).Returns(new Branch { Id = branchId });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.BranchId.Should().Be(branchId);
            await _branchRepository.Received(1).DeleteAsync(Arg.Any<Branch>());
        }

        [Fact(DisplayName = "Given a non-existing branch When deleting Then throws exception")]
        public async Task Handle_BranchNotFound_ThrowsException()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var command = new DeleteBranchCommand { BranchId = branchId };

            _validator.ValidateAsync(command, CancellationToken.None)
                      .Returns(new ValidationResult());

            _branchRepository.GetByIdAsync(branchId).Returns((Branch)null); // Filial não encontrada

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Filial não encontrada.");
            await _branchRepository.DidNotReceive().DeleteAsync(Arg.Any<Branch>());
        }

        [Fact(DisplayName = "Given an invalid request When deleting Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var command = new DeleteBranchCommand { BranchId = Guid.Empty }; // ID inválido
            var validationErrors = new ValidationResult(new[]
            {
                new ValidationFailure("BranchId", "O ID da filial é inválido.")
            });

            _validator.ValidateAsync(command, CancellationToken.None).Returns(validationErrors);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
            await _branchRepository.DidNotReceive().DeleteAsync(Arg.Any<Branch>());
        }
    }
}
