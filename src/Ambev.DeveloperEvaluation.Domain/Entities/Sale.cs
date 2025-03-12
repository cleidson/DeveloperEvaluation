using Ambev.DeveloperEvaluation.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty; // Número da venda
        public DateTime SaleDate { get; set; } = DateTime.UtcNow; // Data da venda

        // Cliente que fez a compra (User com Role Customer)
        public Guid CustomerId { get; set; }
        public User Customer { get; set; } = null!;

        // Filial onde foi realizada a venda
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = null!;

        // Total da venda
        public decimal TotalAmount { get; set; }

        public bool IsCancelled { get; set; } = false; // Status da venda

        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

        // Usuário do sistema que registrou a venda
        public Guid RegisteredByUserId { get; set; }
        public User RegisteredByUser { get; set; } = null!;
        public SaleStatus Status { get; set; }
    }
}
