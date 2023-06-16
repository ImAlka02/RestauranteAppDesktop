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
        public IEnumerable<Menu> GetMenuXNombre(string palabra)
        {
            return context.Menu.Where(x => x.Nombre.ToLower() == palabra.ToLower());
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
        public bool Validar(Menu m, out List<string> errores)
        {
            errores = new List<string>();
            if (m != null)
            {
                if (string.IsNullOrWhiteSpace(m.Nombre))
                    errores.Add("Necesita escribir el nombre del platillo");
                else if (!Regex.IsMatch(m.Nombre, @"^[a-z A-ZñÑ]+$"))
                    errores.Add("Escriba bien el nombre del platillo, no puede estar formado por carácteres especiales o números.");
                else if (m.Nombre.Length > 40)
                    errores.Add("El nombre del platillo no puede exceder los 40 caracteres");
                if (m.Precio <= 0)
                    errores.Add("El precio debe ser mayor que cero");
                if (string.IsNullOrWhiteSpace(m.Descripcion))
                    errores.Add("Necesita escribir la descripción del platillo");
            }
            return errores.Count() == 0;
        }
    }
}
