using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Votacion.Models;
using Votacion.Services;

namespace Votacion.ViewModels
{
    public enum Vistas
    {
        Pregunta,
        Resultados
    }
    public class EncuestaViewModel : INotifyPropertyChanged
    {

        public ICommand IniciarCommand { get; set; }

        public Pregunta? Pregunta { get; set; }

        public Vistas Vista { get; set; }

        public int Voto1 { get; set; }
        public int Voto2 { get; set; }
        public int Voto3 { get; set; }

        public string Error { get; set; } = "";

        public int Total => Voto1 + Voto2 + Voto3;

        VotacionService votacionService = new();

        public EncuestaViewModel()
        {
            Vista = Vistas.Pregunta;
            IniciarCommand = new RelayCommand(Iniciar);

            //Nos sucribimos al evento
            votacionService.VotoRecibido += VotacionService_VotoRecibido;

            //Cargamos la pregunta
            CargarPregunta();
        }

        private void CargarPregunta()
        {
            if (File.Exists("pregunta.json"))
            {
                var json = File.ReadAllText("pregunta.json");
                Pregunta = JsonConvert.DeserializeObject<Pregunta>(json);
            }
            else
            {
                Pregunta = new();
            }

            Lanzar(nameof(Pregunta));
        }


        private void VotacionService_VotoRecibido(int obj)
        {
            switch (obj)
            {
                case 1: Voto1++; break;
                case 2: Voto2++; break;
                case 3: Voto3++; break;
            }
            Lanzar();
        }

        private void Iniciar()
        {
            if (Pregunta != null)
            {
                Error = "";

                //Validar la pregunta
                if (string.IsNullOrWhiteSpace(Pregunta.Descripcion))
                {
                    Error = "Escribe la pregunta" + Environment.NewLine;
                }

                if (string.IsNullOrEmpty(Pregunta.Respuesta1) || string.IsNullOrEmpty(Pregunta.Respuesta2) || string.IsNullOrEmpty(Pregunta.Respuesta3))
                {
                    Error = "Escribe por lo menos 1 respuesta" + Environment.NewLine;
                }

                if (Error == "")//No hubo errores
                {
                    votacionService.EstablecerPregunta(Pregunta);
                    votacionService.Iniciar();
                    var json = JsonConvert.SerializeObject(Pregunta);
                    File.WriteAllText("pregunta.json", json);
                    Vista = Vistas.Resultados;
                }
            }

            Lanzar();
        }

        void Lanzar(string? nombre = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
