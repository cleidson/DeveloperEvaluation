namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.BranchsFeature.GetBranchs
{
    public class GetBranchsResponse
    {
        public Guid BranchId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
