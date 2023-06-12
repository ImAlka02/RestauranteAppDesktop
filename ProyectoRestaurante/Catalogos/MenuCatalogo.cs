using Microsoft.EntityFrameworkCore;
using ProyectoRestaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
