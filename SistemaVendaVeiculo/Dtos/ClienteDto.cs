using SistemaVendaVeiculo.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SistemaVendaVeiculo.Dtos
{
    public class ClienteDto
    {
       
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
    }
}

