using System;
using System.Collections.Generic;

namespace Concessionaria.Models;

public partial class Vendedor
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefone { get; set; }

    public virtual ICollection<Venda> Venda { get; set; } = new List<Venda>();
}
