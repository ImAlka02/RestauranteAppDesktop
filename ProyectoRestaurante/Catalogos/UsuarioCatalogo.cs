using Microsoft.EntityFrameworkCore;
using ProyectoRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Catalogos
{
    public class UsuarioCatalogo
    {
        RestauranteContext context = new();
        public Usuario? GetUsuario(string correo)
        {
            return context.Usuario.FirstOrDefault(x=> x.Correo == correo);
        }
        public IEnumerable<Usuario> GetUsuarios()
        {
            return context.Usuario.Include(x => x.IdRolNavigation);
        }
        public void Agregar(Usuario u)
        {
            context.Add(u);
            context.SaveChanges();
        }
        public void Eliminar(Usuario u)
        {
            context.Remove(u);
            context.SaveChanges();
        }
        public void Editar(Usuario u)
        {
            context.Update(u);
            context.SaveChanges();
        }

    }
}
