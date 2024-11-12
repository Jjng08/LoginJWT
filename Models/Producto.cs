using System;
using System.Collections.Generic;

namespace LoginJWT.Models;

public partial class Producto
{
    public int Idproducto { get; set; }

    public string? Nombre { get; set; }

    public string? Marca { get; set; }

    public decimal? Precio { get; set; }
}
