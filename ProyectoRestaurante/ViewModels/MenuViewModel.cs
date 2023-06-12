using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using ProyectoRestaurante.Catalogos;
using ProyectoRestaurante.Models;
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
        MenuCatalogo catalogoMen = new MenuCatalogo();
        public ObservableCollection<Menu> listamenus { get; set; } = new ObservableCollection<Menu>();
        public Menu? Menu { get; set; }
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
            VerEditarMenuCommand = new RelayCommand(VerEditarMenu);
            RegistrarMenuCommand = new RelayCommand(RegistrarMenu);
            EliminarMenuCommand = new RelayCommand(EliminarMenu);
            EditarMenuCommand = new RelayCommand(EditarMenu);
        

        }

        private void EditarMenu()
        {
            throw new NotImplementedException();
        }

        private void EliminarMenu()
        {
            throw new NotImplementedException();
        }

        private void RegistrarMenu()
        {
            throw new NotImplementedException();
        }

        private void VerEditarMenu()
        {
            throw new NotImplementedException();
        }

        private void VerEliminarMenu()
        {
            throw new NotImplementedException();
        }

        private void VerRegistrarMenu()
        {
            throw new NotImplementedException();
        }

        void Actualizar(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
