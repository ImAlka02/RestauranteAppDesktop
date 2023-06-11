using ProyectoRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Catalogos
{
    public class RolesCatalogo
    {
        RestauranteContext context = new();
        public Rol? GetRol(String nombre)
        {
            return context.Rol.FirstOrDefault(x => x.Nombre == nombre);
        }
        public IEnumerable<Rol> GetRoles()
        {
            return context.Rol.OrderBy(x => x.Nombre);
        }

    }
}
