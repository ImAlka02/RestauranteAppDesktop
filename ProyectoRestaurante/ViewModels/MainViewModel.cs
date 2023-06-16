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
using ProyectoRestaurante.Views.AdminViews;
using System.Net.Mail;

namespace ProyectoRestaurante.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Propiedades e Instancias
        public bool EstaConectado => Usuario.Id != 0;
        UsuarioCatalogo catalogoUs = new UsuarioCatalogo();
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

        public void EnviarCorreo()
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress("201G0253@rcarbonifera.tecnm.mx", "Registro en El Buen Gusto"),
                    Subject = "Bienvenido a nuestra plataforma"
                };
                mail.IsBodyHtml = true;
                mail.Body = $"<body style=\"border: 2px solid orange; Padding:20px; width:400px; border-radius:20px\">\r\n    <h1 style=\"font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size:x-large; color: orange;\">Estimado Usuario: </h1>\r\n    <h3 style=\"font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: orange;\">Bienvenido al sistema de Registro de Usuarios del restaurante \"El Buen Gusto\".</h3>\r\n    <div style=\"width:400px\";>\r\n        <p style=\"font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size:18;\">\r\n            Este correo electrónico es para confirmar que su cuenta ha sido creada correctamente y que ahora puede acceder a todos los servicios de nuestra plataforma.<br />\r\n            Por favor, guarde su nombre de usuario y contraseña en un lugar seguro, ya que los necesitará para acceder a su cuenta en el futuro. <br />\r\n            Si tiene alguna pregunta o necesita ayuda, por favor no dude en contactar a través del correo electrónico de ayuda: <i><b>elbuengusto@gmail.com</b></i><br />\r\n           \r\n            Gracias por confiar en nuestra plataforma.\r\n            <br />\r\n            Atentamente, <b>El Buen Gusto</b>.\r\n        </p>\r\n    </div>\r\n</body>";
                mail.Bcc.Add("elbuengustotec@gmail.com");
                mail.Bcc.Add(Usuario.Correo);
                SmtpClient client = new SmtpClient("smtp.outlook.office365.com");
                //SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                //System.Net.NetworkCredential cred = new("elbuengustotec@gmail.com", "buengusto123");
                System.Net.NetworkCredential cred = new("201G0253@rcarbonifera.tecnm.mx", "hawkshero#2");
                client.Credentials = cred;
                client.Send(mail);
            }
            catch (Exception)
            {
                Error = "Ha ocurrido un error inesperado";

            }

        }

        private void Registrar()
        {
            if (Usuario != null)
            {
                if (catalogoUs.Validar(Usuario, out List<string> errores))
                {
                    catalogoUs.Agregar(Usuario);
                    EnviarCorreo();
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
                    Error = "Contraseña incorrecta";

                }
                Actualizar();
            }
        }


        [Authorize(Roles = "Cliente")]
        private void AccionesUsuarioCliente()
        {
            Vista = new PrincipalClienteView();
        }

        [Authorize(Roles = "Administrador")]
        private void AccionesUsuarioAdministrador()
        {
            Vista = new PrincipalAdminView();
        }

        private void CerrarSesion()
        {
            Usuario = new();
            Vista = LV;
            Actualizar();
        }

        void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        
        
    }
}
