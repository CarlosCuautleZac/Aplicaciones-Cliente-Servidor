using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Transactions;
using System.Windows.Input;
using VotacionXamarin.Models;
using VotacionXamarin.Services;
using Xamarin.Forms;

namespace VotacionXamarin.ViewModels
{
    public class VotacionViewModel : INotifyPropertyChanged
    {
        VotacionCliente cliente = new VotacionCliente();

        public Pregunta Pregunta { get; set; }
        public string Error { get; set; }
        public Command VotarCommand { get; set; }

        public bool Votado { get; set; }

        public VotacionViewModel()
        {
            VotarCommand = new Command<int>(Votar);
            CargarPregunta();
        }

        private async void CargarPregunta()
        {
            Pregunta = await cliente.GetPregunta();

            if (Pregunta == null)
                Error = "No se pudo conectar al servidor";

            Lanzar();
        }

        //get
        //private async void Votar(int opcionvoto)
        //{
        //    try
        //    {
        //        if (Pregunta != null)
        //        {
        //            await cliente.Votar(opcionvoto);
        //            Votado = true;
        //            Lanzar(nameof(Votado));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Error = ex.Message;
        //        Lanzar(nameof(Error));
        //    }

        //}


        private async void Votar(int opcionvoto)
        {
            try
            {
                if (Pregunta != null)
                {
                    Voto v = new Voto();
                    v.Opcion = opcionvoto;
                    await cliente.VotarPost(v);
                    Votado = false;
                    Lanzar();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Lanzar(nameof(Error));
            }

        }

        //private async void VotarPost(Voto voto)
        //{
        //    try
        //    {
        //        if (voto != null)
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Error = ex.Message;
        //        Lanzar(nameof(Error));
        //    }

        //}

        public void Lanzar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
