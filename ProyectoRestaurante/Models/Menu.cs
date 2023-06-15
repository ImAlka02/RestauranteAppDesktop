using System;
using System.Collections.Generic;

namespace ProyectoRestaurante.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public string Descripcion { get; set; } = null!;
}
