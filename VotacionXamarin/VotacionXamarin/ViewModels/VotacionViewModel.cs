using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Transactions;
using System.Windows.Input;
using VotacionXamarin.Services;

namespace VotacionXamarin.ViewModels
{
    public class VotacionViewModel : INotifyPropertyChanged
    {
        VotacionCliente cliente = new VotacionCliente();

        public string Pregunta { get; set; }
        public string Error { get; set; }
        public ICommand VotarCommand { get; set; }


        public VotacionViewModel()
        {
            VotarCommand = new RelayCommand(Votar);
        }

        private void Votar()
        {
            throw new NotImplementedException();
        }

        public void Lanzar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
