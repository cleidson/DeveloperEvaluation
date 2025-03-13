using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    /// <summary>
    /// Evento disparado quando um item de venda é cancelado.
    /// </summary>
    public record ItemCancelledEvent(Guid SaleId, Guid ProductId, int Quantity) : INotification;
}
