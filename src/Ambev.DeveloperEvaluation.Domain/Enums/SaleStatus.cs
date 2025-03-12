namespace Ambev.DeveloperEvaluation.Domain.Enums;

/// <summary>
/// Defines the status of a sale transaction.
/// </summary>
public enum SaleStatus
{
    Pending = 0,    // Venda iniciada, mas não finalizada
    Completed = 1,  // Venda finalizada com sucesso
    Cancelled = 2   // Venda cancelada
}
