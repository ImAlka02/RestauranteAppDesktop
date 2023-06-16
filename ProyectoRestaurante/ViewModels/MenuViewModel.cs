using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoRestaurante.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged 
    {

        int index = 0;
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
        public ICommand GetMenusXNombreCommand { get; set; }
        public ICommand VerEditarUsuarioCommand { get; set; }
        public ICommand EditarUsuarioCommand { get; set; }
        public ICommand VerEliminarUsuarioCommand { get; set; }
        public ICommand EliminarUsuarioCommand { get; set; }

        public MenuViewModel()
        {
            VerRegistrarMenuCommand = new RelayCommand(VerRegistrarMenu);
            VerEliminarMenuCommand = new RelayCommand<int>(VerEliminarMenu);
            VerEditarMenuCommand = new RelayCommand<int>(VerEditarMenu);
            RegistrarMenuCommand = new RelayCommand(RegistrarMenu);
            EliminarMenuCommand = new RelayCommand(EliminarMenu);
            EditarMenuCommand = new RelayCommand<Menu>(EditarMenu);
            NavegarUsuariosCommand = new RelayCommand(VerUsuarios);
            NavegarMenuCommand = new RelayCommand(VerMenu);
            GetMenusXNombreCommand = new RelayCommand<string>(GetMenusXNombre);
            VerEditarUsuarioCommand = new RelayCommand<int>(VerEditarUsuario);
            EditarUsuarioCommand = new RelayCommand(EditarUser);
            VerEliminarUsuarioCommand = new RelayCommand<int>(VerEliminarUsuario);
            EliminarUsuarioCommand = new RelayCommand(EliminarUser);
            ActualizarBD();
            operacion = Accion.VerMenu;
        }

        private void EliminarUser()
        {
            if (Usuario != null)
            {
                catalogoUser.Eliminar(Usuario);
                operacion = Accion.VerUsuarios;
                ActualizarBD();
                Actualizar();
            }
        }

        private void VerEliminarUsuario(int id)
        {
            Usuario = catalogoUser.GetUsuarioByID(id);
            if (Usuario != null)
            {
                operacion = Accion.EliminarUsuario;
                Actualizar();
            }
        }

        private void EditarUser()
        {
            if (Usuario != null)
            {
                if (catalogoUser.Validar(Usuario, out List<string> errores))
                {
                    var temporal = catalogoUser.GetUsuarioByID(Usuario.Id);
                    if (temporal != null)
                    {
                        temporal.Id = Usuario.Id;
                        temporal.Nombre = Usuario.Nombre;
                        temporal.Correo = Usuario.Correo;
                        temporal.Contrasena = Usuario.Contrasena;
                        temporal.IdRol = Usuario.IdRol;

                        catalogoUser.Editar(temporal);
                        ActualizarBD();
                        operacion = Accion.VerUsuarios;
                    }
                }
                else
                {
                    foreach (var item in errores)
                    {
                        Error = $"{Error} {item} {Environment.NewLine}";
                        Actualizar();
                    }
                }
            }
            Error = "";
            ActualizarBD();
            Actualizar();
        }

        private void VerEditarUsuario(int id)
        {
            Operacion = Accion.EditarUsuario;
            Usuario = catalogoUser.GetUsuarioByID(id);
            if (Usuario != null)
            {
                Usuario u = new()
                {
                    Id = Usuario.Id,
                    Nombre = Usuario.Nombre,
                    Correo = Usuario.Correo,
                    Contrasena = Usuario.Contrasena,
                    IdRol = Usuario.IdRol
                };
                index = UsuarioLista.IndexOf(Usuario);
                Usuario = u;
                Actualizar();
            }
        }

        private void GetMenusXNombre(string obj)
        {
            if (obj != "")
            {
                ListaMenu.Clear();
                foreach (var item in catalogoMen.GetMenuXNombre(obj))
                {
                    ListaMenu.Add(item);
                }
                Actualizar();
            }
            else
            {
                Actualizar();
                ActualizarBD();
            }
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
            
            if (Menu != null)
            {
                if (catalogoMen.Validar(Menu, out List<string> errores))
                {
                    var temporal = catalogoMen.ObtenerMenus(Menu.Id);
                    if (temporal != null)
                    {
                        temporal.Id = Menu.Id;
                        temporal.Nombre = Menu.Nombre;
                        temporal.Descripcion = Menu.Descripcion;
                        temporal.Precio = Menu.Precio;

                        catalogoMen.Update(temporal);
                        ActualizarBD();
                        operacion = Accion.VerMenu;
                    }
                }
                else
                {
                    foreach (var item in errores)
                    {
                        Error = $"{Error} {item} {Environment.NewLine}";
                        Actualizar();
                    }
                }
            }
            Error = "";
            ActualizarBD();
            Actualizar();
            
            
        }

        private void EliminarMenu()
        {
            if (Menu != null)
            {
                catalogoMen.Delete(Menu);
                operacion = Accion.VerMenu;
                ActualizarBD();
                Actualizar();
            }
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

        private void VerEditarMenu(int Id)
        {
            Operacion = Accion.EditarPlatillo;
            Menu = catalogoMen.ObtenerMenus(Id);
            if (Menu != null)
            {
                Menu e = new()
                {
                    Id = Menu.Id,
                    Nombre = Menu.Nombre,
                    Precio = Menu.Precio,
                    Descripcion = Menu.Descripcion,

                };
                index = ListaMenu.IndexOf(Menu);
                Menu = e;
                Actualizar();
            }
        }

        private void VerEliminarMenu(int Id)
        {
            Menu = catalogoMen.ObtenerMenus(Id);
            if (Menu != null)
            {
                operacion = Accion.EliminarPlatillo;
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
