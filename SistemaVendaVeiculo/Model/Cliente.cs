using System.Collections.Generic;
using System;
using SistemaVendaVeiculo.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaDeVendaDeVeiculo
{
    public class Cliente
{
        [Key]
        public int IdCliente { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Endereco { get; set; }
        
        [JsonIgnore]
        public List<Aluguel> Alugueis { get; set; }
}
}
