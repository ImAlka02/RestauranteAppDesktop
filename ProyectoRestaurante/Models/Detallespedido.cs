using System;
using System.Collections.Generic;

namespace ProyectoRestaurante.Models;

public partial class Detallespedido
{
    public int Id { get; set; }

    public int? IdPedido { get; set; }

    public int? IdMenu { get; set; }

    public int Cantidad { get; set; }

    public decimal? PrecioPlatillo { get; set; }

    public string? Descripcion { get; set; }

    public virtual Menu? IdMenuNavigation { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }
}
