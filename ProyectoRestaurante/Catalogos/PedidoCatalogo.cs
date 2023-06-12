using ProyectoRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Catalogos
{
    public class PedidoCatalogo
    {
        RestauranteContext context = new RestauranteContext();
        public IEnumerable<Detallespedido> GetDetalles()
        {
            return context.Detallespedido;
        }
        public IEnumerable<Pedido> GetPedidos()
        {
            return context.Pedido;
        }

        public Detallespedido ObtenerDetallesPedido(int Id)
        {
            return context.Detallespedido.Where(x => x.Id == Id).FirstOrDefault();
        }
        public Pedido ObtenerPedido(int Id)
        {
            return context.Pedido.Where(x => x.Id == Id).FirstOrDefault();
        }
     
        public void Delete(Pedido a)
        {
            context.Pedido.Remove(a);
            context.SaveChanges();
            context.Entry(a).Reload();
        }
        public void Update(Pedido a)
        {
            context.Pedido.Update(a);
            context.SaveChanges();
            context.Entry(a).Reload();
        }
        public void Create(Pedido a)
        {
            context.Pedido.Add(a);
            context.SaveChanges();
            context.Entry(a).Reload();
        }
    }
}
