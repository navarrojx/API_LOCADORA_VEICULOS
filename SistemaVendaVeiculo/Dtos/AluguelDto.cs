using System;

public class AluguelDto
{
    public string CPF { get; set; }
    public int IdVeiculo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public int QuilometragemInicial { get; set; }
    public int QuilometragemFinal { get; set; }
    public decimal ValorLocacao { get; set;  }
    public decimal ValorDiaria { get; set; } = 50; // ✅ valor padrão definido aqui

    public string Observacoes { get; set; }
}
