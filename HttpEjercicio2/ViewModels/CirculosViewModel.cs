using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HttpEjercicio2.ViewModels
{
    public class CirculosViewModel:INotifyPropertyChanged
    {
        Services.ServiceCirculos service = new();     

        public ICommand IniciarServerCommand { get; set; }

        public CirculosViewModel()
        {
            IniciarServerCommand = new RelayCommand(Iniciar);
        }

        private void Iniciar()
        {
            
            service.Iniciar();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
