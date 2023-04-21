using System;
using System.Collections.Generic;

namespace Concessionaria.Models;

public partial class Endereco
{
    public int Id { get; set; }

    public string Logradouro { get; set; } = null!;

    public int Numero { get; set; }

    public string Bairro { get; set; } = null!;

    public string? Complemento { get; set; }

    public string Cidade { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Cep { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
