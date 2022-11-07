using FotosAlbumEnTiempoReal.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Threading;
using System.ComponentModel;

namespace FotosAlbumEnTiempoReal.ViewModels
{
    public class AlbumViewModel:INotifyPropertyChanged
    {
        AlbumService service = new();

        public ICommand IniciarCommand { get; set; }

        public ObservableCollection<BitmapImage> Imagenes { set; get; } = new();

        DispatcherTimer timer = new DispatcherTimer();
        public BitmapImage? ImagenSeleccionada { get; set; } = null;
        int indice = 0;

        Dispatcher principal = Dispatcher.CurrentDispatcher;


        public event PropertyChangedEventHandler? PropertyChanged;

        public AlbumViewModel()
        {
            var ruta = Environment.CurrentDirectory;
            var files = Directory.GetFiles("Assets/", "*.jpg");
            foreach (var item in files)
            {

                BitmapImage image = new BitmapImage(new Uri(ruta + "/" + item));
                Imagenes.Add(image);
            }

            ImagenSeleccionada = Imagenes.FirstOrDefault();

            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
            service.ImagenRecibida += Service_ImagenRecibida;

            IniciarCommand = new RelayCommand(() => service.Start());

        }

        private void Service_ImagenRecibida(string? obj)
        {
            principal.Invoke(() =>
            {
                var ruta = Environment.CurrentDirectory;
                BitmapImage image = new BitmapImage(new Uri(ruta + "/" + obj));
                Imagenes.Add(image);
                ImagenSeleccionada = image;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));

                timer.Stop();
                timer.Start();
            });
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            indice++;
            if (indice == Imagenes.Count)
                indice = 0;
            ImagenSeleccionada = Imagenes.ElementAtOrDefault(indice);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));

        }
    }
}
