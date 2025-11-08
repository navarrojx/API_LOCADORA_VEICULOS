using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaVendaVeiculo.Model;

namespace SistemaVendaVeiculo.Model
{
    public enum MetodoPagamento
    {
        CartaoCredito = 0,
        Boleto=1,
        Transferencia=2,
        Dinheiro=3,
        Outro=4
    }

    public enum StatusPagamento
    {
        Pendente=0,
        Pago=1,
        Cancelado =2,
        EmAtraso=3

    }

    public class Pagamento
    {
        [Key]
        public int IdPagamento { get; set; }

    
        public int IdAluguel { get; set; }
        [ForeignKey("IdAluguel")]
        public Aluguel? Aluguel { get; set; }

        public DateTime DataPagamento { get; set; }
        public MetodoPagamento MetodoPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
    }
}
