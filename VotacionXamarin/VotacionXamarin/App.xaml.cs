using System;
using VotacionXamarin.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace VotacionXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Page1();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
