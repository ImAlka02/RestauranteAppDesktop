using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using ProyectoRestaurante.Catalogos;
using ProyectoRestaurante.Models;
using ProyectoRestaurante.Views;
using ProyectoRestaurante.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
        UsuarioCatalogo catalogoUser = new UsuarioCatalogo();
        public ObservableCollection<Menu> ListaMenu { get; set; } = new ObservableCollection<Menu>();
        public ObservableCollection<Usuario> UsuarioLista { get; set; }= new ObservableCollection<Usuario>();
        private string rutaImagen, copiaImagen;
        public string Imagen
        {
            get { return rutaImagen; }
            set {
                rutaImagen = value;
                Actualizar("Imagen");
            }
        }
        public Menu? Menu { get; set; }
        public Usuario Usuario { get; set; }
        public string Error { get; set; }
        public ICommand VerRegistrarMenuCommand { get; set; }
        public ICommand VerEliminarMenuCommand { get; set; }
        public ICommand VerEditarMenuCommand { get; set; }
        public ICommand RegistrarMenuCommand { get; set; }
        public ICommand EliminarMenuCommand { get; set; }
        public ICommand EditarMenuCommand { get; set; }
        public ICommand NavegarUsuariosCommand { get; set; }
        public ICommand NavegarMenuCommand { get; set; }



        public MenuViewModel()
        {
            VerRegistrarMenuCommand = new RelayCommand(VerRegistrarMenu);
            VerEliminarMenuCommand = new RelayCommand(VerEliminarMenu);
            VerEditarMenuCommand = new RelayCommand<Menu>(VerEditarMenu);
            RegistrarMenuCommand = new RelayCommand(RegistrarMenu);
            EliminarMenuCommand = new RelayCommand(EliminarMenu);
            EditarMenuCommand = new RelayCommand<Menu>(EditarMenu);
            NavegarUsuariosCommand = new RelayCommand(VerUsuarios);
            NavegarMenuCommand = new RelayCommand(VerMenu);
            ActualizarBD();
            operacion = Accion.VerMenu;
        }

        private void VerMenu()
        {
            operacion = Accion.VerMenu;
            Actualizar();
        }

        private void VerUsuarios()
        {
            operacion = Accion.VerUsuarios;
            Actualizar();
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
                    if (!string.IsNullOrWhiteSpace(Imagen))
                    {
                        var t = $"{AppDomain.CurrentDomain.BaseDirectory}imagenes";
                        if (!Directory.Exists(t))
                        {
                            Directory.CreateDirectory(t);
                        }
                        var t1 = $"{t}\\{Menu.Id}.jpg";
                        File.Copy(Imagen, t1, true);
                    }
                    operacion = Accion.VerMenu;
                    
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
            ActualizarBD();
            Actualizar();
            
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
            operacion = Accion.AgregarPlatillo;
            Actualizar();
        }

        private void ActualizarBD()
        {
            ListaMenu.Clear();
            UsuarioLista.Clear();
            if (ListaMenu != null)
            {
                foreach (var item in catalogoMen.GetMenus())
                {
                    ListaMenu.Add(item);
                }
            }
            if(UsuarioLista != null)
            {
                foreach (var item in catalogoUser.GetUsuarios())
                {
                    UsuarioLista.Add(item);
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
