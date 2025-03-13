namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.GetBranch;

 
public class GetBranchResponse
{
    public Guid Id { get; set; }  // Certifique-se de que existe essa propriedade
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}