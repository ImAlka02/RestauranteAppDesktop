using Microsoft.EntityFrameworkCore;
using ProyectoRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Catalogos
{
    public class UsuarioCatalogo
    {
        RestauranteContext context = new();

        public void Recargar(Usuario u)
        {
            context.Entry(u).Reload();
        }
        public int spIniciarSesion(string correo, string contrasena)
        {
            string cadena = $"select fnIniciarSesion('{correo}','{contrasena}')";

            var x = ((IEnumerable<int>)context.Database.SqlQueryRaw<int>(cadena, correo, contrasena).AsAsyncEnumerable<int>()).First();
            if (x == 1)
            {
                var user = context.Usuario.Include(x => x.IdRolNavigation).FirstOrDefault(x => x.Correo == correo);
                if (user != null)
                {
                    EstablecerTipoUsuario(user);
                }
            }
            return x;
        }

        private void EstablecerTipoUsuario(Usuario us)
        {
            GenericIdentity user = new(us.Nombre);
            if (us != null)
            {
                string[] roles = new string[] { us.IdRolNavigation.Nombre };
                GenericPrincipal usprincipal = new(user, roles);
                Thread.CurrentPrincipal = usprincipal;
            }
        }
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
