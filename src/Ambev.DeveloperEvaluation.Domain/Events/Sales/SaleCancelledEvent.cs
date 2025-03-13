using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    /// <summary>
    /// Evento disparado quando uma venda é cancelada.
    /// </summary>
    public record SaleCancelledEvent(Guid SaleId) : INotification;
}
