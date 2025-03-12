namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature;

 
public class BranchResponse
{
    public Guid Id { get; set; }  // Certifique-se de que existe essa propriedade
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}