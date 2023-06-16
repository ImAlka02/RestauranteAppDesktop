using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoRestaurante.Views.AdminViews
{
    /// <summary>
    /// Lógica de interacción para AgregarMenu.xaml
    /// </summary>
    public partial class AgregarMenu : UserControl
    {
        public AgregarMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new();
            file.Filter = "Imagenes|*.jpg;*.png;*.bmp";
            if(file.ShowDialog() == true )
            {
                txtImagen.Text = file.FileName;
            }
        }
    }
}
