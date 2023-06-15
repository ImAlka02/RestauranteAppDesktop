using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using ProyectoRestaurante.Catalogos;
using ProyectoRestaurante.Models;
using ProyectoRestaurante.Views;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ProyectoRestaurante.Views.ClienteView;

namespace ProyectoRestaurante.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Propiedades e Instancias
        public bool EstaConectado => Usuario.Id != 0;
        UsuarioCatalogo catalogoUs = new UsuarioCatalogo();
        MenuViewModel menuViewModel = new();
        private object vmActual;
        public object ViewModelActual
        {
            get { return vmActual; }
            set 
            {
                vmActual = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }

        LoginView LV; //
        RegistrarView RV; //
        public Usuario? Usuario { get; set; }
        public string Error { get; set; }
        public UserControl Vista { get; set; }
        #endregion

        #region Commandos
        public ICommand CerrarSesionCommand { get; set; }
        public ICommand IniciarSesionCommand { get; set; }
        public ICommand VerRegistrarCommand { get; set; }
        public ICommand VolverCommand { get; set; }
        public ICommand RegistarCommand { get; set; }
        #endregion

        public MainViewModel()
        {
            CerrarSesionCommand = new RelayCommand(CerrarSesion);
            IniciarSesionCommand = new RelayCommand(IniciarSesion);
            RegistarCommand = new RelayCommand(Registrar);
            VerRegistrarCommand = new RelayCommand(VerRegistar);
            VolverCommand = new RelayCommand(Volver);

            ViewModelActual = this;
            //Creacion de un nuevo objeto "Usuario"
            Usuario = new();
            //Vista predeterminada "LoginView"
            LV = new LoginView()
            {
                DataContext = this
            };
            Vista = LV;
            Actualizar();
        }

        private void Registrar()
        {
            if (Usuario != null)
            {
                if (catalogoUs.Validar(Usuario, out List<string> errores))
                {
                    catalogoUs.Agregar(Usuario);
                    //EnviarCorreo(Usuario);
                    Volver();
                    Usuario = new();
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
        private void Volver()
        {
            LV = new LoginView();
            Vista = LV;
            Error = "";
            Actualizar();
        }

        private void VerRegistar()
        {
            Usuario = new();
            RV = new RegistrarView();
            Vista = RV;
            Error = "";
            Actualizar();
        }
        private void IniciarSesion()
        {
            if (Usuario != null)
            {
                var inicio = catalogoUs.spIniciarSesion(Usuario.Correo, Usuario.Contrasena);

                if (inicio == 1)
                {
                    // Existe el usuario, ahora lo buscamos en la bd para establecer que el es el conectado
                    var usconectado = catalogoUs.GetUsuario(Usuario.Correo);
                    Usuario = usconectado;
                    if (Thread.CurrentPrincipal != null)
                    {
                        if (Thread.CurrentPrincipal.IsInRole("Administrador"))
                            AccionesUsuarioAdministrador();
                        if (Thread.CurrentPrincipal.IsInRole("Cliente"))
                            AccionesUsuarioCliente();
                    }


                }
                else if (inicio == 2)
                {
                    Error = "Usuario no encontado";

                }
                else
                {
                    Error = "contrasena incorrecta";

                }
                ViewModelActual = menuViewModel;
                Actualizar();
            }
        }


        [Authorize(Roles = "Cliente")]
        private void AccionesUsuarioCliente()
        {
            Vista = new MenuView();
        }

        [Authorize(Roles = "Administrador")]
        private void AccionesUsuarioAdministrador()
        {
            //Vista = new UsuariosView();
        }

        private void CerrarSesion()
        {
            Usuario = new();
            //Vista = view;
            Actualizar();
        }

        void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        
        
    }
}
