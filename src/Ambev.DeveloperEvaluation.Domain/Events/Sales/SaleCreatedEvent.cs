using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    /// <summary>
    /// Evento disparado quando uma venda é criada.
    /// </summary>
    public record SaleCreatedEvent(Guid SaleId, DateTime SaleDate, Guid CustomerId, decimal TotalAmount) : INotification;
}
