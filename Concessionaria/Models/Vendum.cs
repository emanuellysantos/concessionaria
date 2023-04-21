using System;
using System.Collections.Generic;

namespace Concessionaria.Models;

public partial class Vendum
{
    public int Id { get; set; }

    public int VeiculoId { get; set; }

    public int VendedorId { get; set; }

    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Veiculo Veiculo { get; set; } = null!;

    public virtual Vendedor Vendedor { get; set; } = null!;
}
