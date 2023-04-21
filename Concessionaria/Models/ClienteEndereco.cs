using System;
using System.Collections.Generic;

namespace Concessionaria.Models;

public class ClienteEndereco
{
    public virtual Endereco Endereco { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;
}
