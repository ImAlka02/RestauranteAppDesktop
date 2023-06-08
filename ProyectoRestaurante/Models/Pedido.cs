using System;
using System.Collections.Generic;

namespace ProyectoRestaurante.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? MontoPagar { get; set; }

    public int? IdEstado { get; set; }

    public virtual ICollection<Detallespedido> Detallespedido { get; set; } = new List<Detallespedido>();

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
