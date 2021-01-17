using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Happy_Tree
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Data.read();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            Data.save();
        }

        protected override void OnResume()
        {
        }
    }
}
