using FotosAlbumEnTiempoReal.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FotosAlbumEnTiempoReal.ViewModels
{
    public class AlbumViewModel
    {
        AlbumService service = new();

        public ICommand IniciarCommand { get; set; }

        public AlbumViewModel()
        {
            IniciarCommand = new RelayCommand(()=>service.Start());
        }

       
    }
}
