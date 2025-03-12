namespace Ambev.DeveloperEvaluation.Domain.Enums;

/// <summary>
/// Defines different types of discounts that can be applied to a sale item.
/// </summary>
public enum DiscountType
{
    None = 0,            // Sem desconto
    QuantityBased = 1,   // Desconto por quantidade comprada
    Promotional = 2,     // Desconto promocional manual
    Special = 3          // Desconto especial (ex: para clientes VIP)
}
