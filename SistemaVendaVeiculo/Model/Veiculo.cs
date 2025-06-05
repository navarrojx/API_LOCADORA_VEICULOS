using SistemaDeVendaDeVeiculo;
using SistemaVendaVeiculo.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Veiculo
{
    [Key]
    public int IdVeiculo { get; set; }
    public string? Modelo { get; set; }
    public int AnoFabricacao { get; set; }
    public int? Quilometragem { get; set; }
    public string? Cor { get; set; }
    public string? Placa { get; set; }
    public string? TipoCombustivel { get; set; }
    public string? Status { get; set; }

    public int IdFabricante { get; set; }

    [ForeignKey("IdFabricante")]
    public Fabricante? Fabricante { get; set; }

    public List<Aluguel>? Alugueis { get; set; }
}
