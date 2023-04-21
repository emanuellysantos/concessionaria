using System;
using System.Collections.Generic;

namespace Concessionaria.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Telefone { get; set; }

    public string Email { get; set; } = null!;

    public DateTime DataNasc { get; set; }

    public string Cpf { get; set; } = null!;

    public int EnderecoId { get; set; }

    public virtual Endereco Endereco { get; set; }

    public virtual ICollection<Venda> Venda { get; set; } = new List<Venda>();
}
