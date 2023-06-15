using Microsoft.EntityFrameworkCore;
using ProyectoRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoRestaurante.Catalogos
{
    public class MenuCatalogo
    {
        RestauranteContext context = new RestauranteContext();
        public IEnumerable<Menu> GetMenus()
        {
            return context.Menu;
        }
        
        public Menu ObtenerMenus(int Id)
        {
            return context.Menu.Where(x => x.Id == Id).FirstOrDefault();
        }
        
        public void Delete(Menu a)
        {
            context.Menu.Remove(a);
            context.SaveChanges();
            context.Entry(a).Reload();
        }
        public void Update(Menu a)
        {
            context.Menu.Update(a);
            context.SaveChanges();
            context.Entry(a).Reload();
        }
        public void Create(Menu a)
        {
            context.Menu.Add(a);
            context.SaveChanges();
            context.Entry(a).Reload();
        }
        public bool Validar(Menu u, out List<string> errores)
        {
            errores = new List<string>();
            if (u != null)
            {
                if (string.IsNullOrWhiteSpace(u.Nombre))
                    errores.Add("Necesita escribir el nombre");
                else if (!Regex.IsMatch(u.Nombre, @"^[a-z A-ZñÑ]+$"))
                    errores.Add("Escriba bien el nombre del usuario, no puede estar formado por carácteres especiales o números.");
                //if (string.IsNullOrEmpty(u.Correo))
                //    errores.Add("Escriba el correo electrónico");
                //else if (!Regex.IsMatch(u.Correo, @"^[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{2,5}$"))
                //    errores.Add("Escriba bien el correo electrónico.");
                //if (string.IsNullOrEmpty(u.Contrasena))
                //    errores.Add("Escriba la contraseña");
                //if (context.Usuario.Any(x => x.Correo == u.Correo && x.Id != u.Id))
                //    errores.Add("El correo electrónico ya ha sido registrado");
            }
            return errores.Count() == 0;
        }
    }
}
