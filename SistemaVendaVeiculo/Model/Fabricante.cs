﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeVendaDeVeiculo
{
    public class Fabricante
    {

        [Key]
        public int IdFabricante { get; set; }


        public string Nome { get; set; }
    public string PaisOrigem { get; set; }
    public DateTime Fundacao { get; set; }
}
}
