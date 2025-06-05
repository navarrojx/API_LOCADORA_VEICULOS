using SistemaVendaVeiculo.Model;
using System.ComponentModel.DataAnnotations;
using System;

namespace SistemaVendaVeiculo.Dtos
{
    public class PagamentoDtos
    {
        [Required]
        public int IdAluguel { get; set; }  

        [Required]
        public DateTime DataPagamento { get; set; } = DateTime.Now;

        [Required]
        public MetodoPagamento MetodoPagamento { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor pago deve ser maior que zero.")]
        public decimal ValorPago { get; set; }

        [Required]
        public StatusPagamento StatusPagamento { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quilometragem final deve ser um valor positivo.")]
        public int? QuilometragemFinal { get; set; }  
    }
}
