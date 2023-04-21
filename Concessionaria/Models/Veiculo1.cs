using System;
using System.Collections.Generic;

namespace Concessionaria.Models;

public partial class Veiculo1
{
    public string Id { get; set; } = null!;

    public decimal Ano { get; set; }

    public string Modelo { get; set; } = null!;

    public string Cor { get; set; } = null!;

    public string Chassi { get; set; } = null!;

    public decimal Valor { get; set; }
}
