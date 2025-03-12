using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class BranchTests
{
    [Fact(DisplayName = "Given valid data When creating branch Then initializes correctly")]
    public void CreateBranch_ValidData_InitializesCorrectly()
    {
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = "Filial Centro",
            Address = "Rua Principal, 123"
        };

        branch.Should().NotBeNull();
        branch.Name.Should().Be("Filial Centro");
        branch.Address.Should().Be("Rua Principal, 123");
    }

    [Fact(DisplayName = "Given a branch When updating details Then changes values correctly")]
    public void UpdateBranch_ChangesValues()
    {
        var branch = new Branch { Id = Guid.NewGuid(), Name = "Old Name", Address = "Old Address" };

        branch.Name = "New Name";
        branch.Address = "New Address";

        branch.Name.Should().Be("New Name");
        branch.Address.Should().Be("New Address");
    }
}
