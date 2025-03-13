using System;

namespace Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch
{
    /// <summary>
    /// Response for UpdateBranchCommand.
    /// </summary>
    public class UpdateBranchResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
    }
}
