namespace Ambev.DeveloperEvaluation.Domain.Enums;

/// <summary>
/// Defines different payment methods available in the system.
/// </summary>
public enum PaymentMethod
{
    None = 0,
    CreditCard = 1,   // Cartão de crédito
    DebitCard = 2,    // Cartão de débito
    Cash = 3,         // Dinheiro
    Pix = 4,          // Pix
    BankTransfer = 5  // Transferência bancária
}
