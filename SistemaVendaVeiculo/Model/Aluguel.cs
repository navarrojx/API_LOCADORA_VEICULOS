using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.Json;


namespace SistemaVendaVeiculo.Model
{
    public class Aluguel
    {
        [Key]
        public int IdAluguel { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }

        [ForeignKey("Veiculo")]
        public int IdVeiculo { get; set; }
        public Veiculo Veiculo { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int QuilometragemInicial { get; set; }
        public int? QuilometragemFinal { get; set; }
        public decimal ValorDiaria { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorLocacao { get; set; }
        public bool Devolvido { get; set; }
        public string Observacoes { get; set; }
        [JsonIgnore]
        public List<Pagamento> Pagamentos { get; set; }
    }
}
