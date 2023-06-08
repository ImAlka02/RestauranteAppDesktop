﻿using GalaSoft.MvvmLight.Command;
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

namespace ProyectoRestaurante.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Propiedades e Instancias
        public bool EstaConectado => Usuario.Id != 0;
        UsuarioCatalogo catalogoUs = new UsuarioCatalogo();
        LoginView LV;
        RegistrarView RV;
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
            //IniciarSesionCommand = new RelayCommand(IniciarSesion);
            RegistarCommand = new RelayCommand(Registrar);
            VerRegistrarCommand = new RelayCommand(VerRegistar);
            VolverCommand = new RelayCommand(Volver);

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
                catalogoUs.Agregar(Usuario);
                //EnviarCorreo(Usuario);
                Volver();
                Usuario = new();
                Actualizar();

            }

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

        //private void IniciarSesion()
        //{

        //    //Validar que las cajas de texto tengas datos
        //    //Correo sea valido con expresiones regulares
        //    if (Usuario != null)
        //    {
        //        var inicio = catalagoUs.spIniciarSesion(Usuario.Correo, Usuario.Contrasena);

        //        if (inicio == 1)
        //        {
        //            // Existe el usuario, ahora lo buscamos en la bd para establecer que el es el conectado
        //            var usconectado = catalagoUs.GetUsuario(Usuario.Correo);
        //            Usuario = usconectado;
        //            if (Thread.CurrentPrincipal != null)
        //            {
        //                if (Thread.CurrentPrincipal.IsInRole("Administrador"))
        //                    AccionesUsuarioAdministrador();
        //                if (Thread.CurrentPrincipal.IsInRole("Capturista"))
        //                    AccionesUsuarioCapturista();
        //            }


        //        }
        //        else if (inicio == 2)
        //        {
        //            Error = "Usuario no encontado";

        //        }
        //        else
        //        {
        //            Error = "contrasena incorrecta";

        //        }
        //        Actualizar();
        //    }
        //}


        [Authorize(Roles = "Cliente")]
        private void AccionesUsuarioCapturista()
        {
            //Vista = new BienvenidoView();
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