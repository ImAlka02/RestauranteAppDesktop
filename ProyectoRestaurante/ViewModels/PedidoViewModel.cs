using GalaSoft.MvvmLight.Command;
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
    public class PedidoViewModel : INotifyPropertyChanged
    {
        PedidoCatalogo catalogoPed = new PedidoCatalogo();
        public ObservableCollection<Pedido> listpedidos { get; set; } = new ObservableCollection<Pedido>();
        public Pedido? Pedido { get; set; }
        public string Error { get; set; }
        public ICommand VerRegistrarPedidoCommand { get; set; }
        public ICommand VerEliminarPedidoCommand { get; set; }
        public ICommand VerEditarPedidoCommand { get; set; }
        public ICommand RegistrarPedidoCommand { get; set; }
        public ICommand EliminarPedidoCommand { get; set; }
        public ICommand EditarPedidoCommand { get; set; }
        public PedidoViewModel()
        {
            VerRegistrarPedidoCommand = new RelayCommand(VerRegistrarMenu);
            VerEliminarPedidoCommand = new RelayCommand(VerEliminarMenu);
            VerEditarPedidoCommand = new RelayCommand(VerEditarMenu);
            RegistrarPedidoCommand = new RelayCommand(RegistrarMenu);
            EliminarPedidoCommand = new RelayCommand(EliminarMenu);
            EditarPedidoCommand = new RelayCommand(EditarMenu);


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
