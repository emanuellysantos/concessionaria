using System;
using System.Collections.Generic;

namespace Concessionaria.Models;

public partial class Veiculos
{
    public int Id { get; set; }

    public int Ano { get; set; }

    public string Modelo { get; set; } = null!;

    public string Cor { get; set; } = null!;

    public string Chassi { get; set; } = null!;

    public decimal Valor { get; set; }

    public virtual ICollection<Venda> Venda { get; set; } = new List<Venda>();
}
