using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using ProyectoRestaurante.Catalogos;
using ProyectoRestaurante.Models;
using ProyectoRestaurante.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoRestaurante.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged 
    {

        private Accion operacion;
        public Accion Operacion
        {
            get { return operacion; }
            set { operacion = value; }
        }
        MenuCatalogo catalogoMen = new MenuCatalogo();
        public ObservableCollection<Menu> ListaMenu { get; set; } = new ObservableCollection<Menu>();
        public ObservableCollection<Usuario> UsuarioLista { get; set; }= new ObservableCollection<Usuario>();
        public Menu? Menu { get; set; }
        public Usuario Usuario { get; set; }
        public string Error { get; set; }
        public ICommand VerRegistrarMenuCommand { get; set; }
        public ICommand VerEliminarMenuCommand { get; set; }
        public ICommand VerEditarMenuCommand { get; set; }
        public ICommand RegistrarMenuCommand { get; set; }
        public ICommand EliminarMenuCommand { get; set; }
        public ICommand EditarMenuCommand { get; set; }
        
        
        public MenuViewModel()
        {
            VerRegistrarMenuCommand = new RelayCommand(VerRegistrarMenu);
            VerEliminarMenuCommand = new RelayCommand(VerEliminarMenu);
            VerEditarMenuCommand = new RelayCommand<Menu>(VerEditarMenu);
            RegistrarMenuCommand = new RelayCommand(RegistrarMenu);
            EliminarMenuCommand = new RelayCommand(EliminarMenu);
            EditarMenuCommand = new RelayCommand<Menu>(EditarMenu);
          
            ActualizarBD();

        }

        private void EditarMenu(Menu m)
        {
            throw new NotImplementedException();
        }

        private void EliminarMenu()
        {
            catalogoMen.Delete(Menu);
            Actualizar();
        }

        private void RegistrarMenu()
        {
            if (Menu != null)
            {
                if (catalogoMen.Validar(Menu, out List<string> errores))
                {
                    catalogoMen.Create(Menu);
                    //EnviarCorreo(Usuario);
                   
                    Menu = new();
                    Actualizar();
                }

                else
                {
                    foreach (var item in errores)
                    {
                        Error = $"{Error}{item}{Environment.NewLine}";
                    }
                    Actualizar();
                }
            }
            Error = "";
        
        }

        private void VerEditarMenu(Menu m)
        {
            if (Menu != null)
            {

            }
        }

        private void VerEliminarMenu()
        {
            if (Menu != null)
            {
                //operacion = Accion.EliminarMenu;
                Actualizar();
            }
        }

        private void VerRegistrarMenu()
        {
            Menu = new();
            //operacion = Accion.RegistrarMenu;
            Actualizar();
        }

        private void ActualizarBD()
        {
            ListaMenu.Clear();
            if(ListaMenu != null)
            {
                foreach (var item in catalogoMen.GetMenus())
                {
                    ListaMenu.Add(item);
                }
            }
            
        }
        void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
        


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
