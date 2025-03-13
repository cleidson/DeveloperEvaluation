using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    /// <summary>
    /// Evento disparado quando uma venda é modificada.
    /// </summary>
    public record SaleModifiedEvent(Guid SaleId, decimal NewTotalAmount) : INotification;
}
