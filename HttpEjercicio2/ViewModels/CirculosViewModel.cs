using GalaSoft.MvvmLight.Command;
using HttpEjercicio2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HttpEjercicio2.ViewModels
{
    public class CirculosViewModel:INotifyPropertyChanged
    {
        public double Alto { get; set; }
        public double Ancho { get; set; }
        ObservableCollection<Ellipse> Circulos { get; set; } = new();
        ServiceCirculos services = new();
        public ICommand IniciarServidorCommand { get; set; }
        Dispatcher dispatcher;


        public CirculosViewModel()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            IniciarServidorCommand = new RelayCommand(IniciarServidor);
            services.CirculoRecibido += Services_CirculoRecibido;
        }

        private void Services_CirculoRecibido(double radio, int posX, int posY, string color)
        {
            dispatcher.Invoke(() =>
            {
                Ellipse e = new();
                {
                    e.Width = radio * 2;
                    e.Height = radio * 2;
                    e.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                };
                Canvas.SetLeft(e, posX - radio);
                Canvas.SetTop(e, posY - radio);
                Circulos.Add(e);
            });
        }

        private void IniciarServidor()
        {
            services.Iniciar();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
