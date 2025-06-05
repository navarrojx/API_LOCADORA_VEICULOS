using SistemaDeVendaDeVeiculo;
using SistemaVendaVeiculo.Model;

namespace SistemaVendaVeiculo.Dtos
{
    public class VeiculoDto
    {
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int Quilometragem { get; set; }
        public string Cor { get; set; }
        public string Placa { get; set; }
        public string TipoCombustivel { get; set; }
        
        public int IdFabricante { get; set; }

        
    }
}
