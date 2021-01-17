using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Happy_Tree
{
    public partial class MainPage : ContentPage
    {

        Task StartTask;
        bool bStartLoop = true;
        async void Button_Clicked(object sender, System.EventArgs e)
        {
            bStartLoop = false;
            startimg.FadeTo(0, 70);
            await logoimg.FadeTo(0, 300);
            await tutorialimg.FadeTo(1, 300);
            await Task.Delay(3800);
            await tutorialimg.FadeTo(0, 300);
            await Navigation.PushModalAsync(new GamePage(), false);
        }
        async void start_loop()
        {
            while(bStartLoop)
            {
                await startimg.FadeTo(0, 400);
                if (!bStartLoop) break;
                await startimg.FadeTo(1, 400);
                if (!bStartLoop) break;
                await Task.Delay(500);
            }
        }
        public MainPage()
        {
            InitializeComponent();
            
            DependencyService.Get<IStatusBarColor>().changestatuscolor(Color.Black.ToHex());
            var statuscolor = Color.Black;
            try
            {
                statuscolor = (Color)App.Current.Resources["PageBackgroundColor"];
                DependencyService.Get<IStatusBarColor>().changestatuscolor(statuscolor.ToHex());
            }
            catch 
            {
            
            }
            DependencyService.Get<IStatusBarColor>().fullscreen(true);
            ICollection <ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                //mergedDictionaries.Add(new Themes.SkyStyle());
            }
        }
        override protected void OnAppearing()
        {
            bStartLoop = true;
            StartTask = new Task(() => { start_loop(); });
            StartTask.Start();
            logoimg.FadeTo(1.0, 400);
        }
        override protected bool OnBackButtonPressed()
        {
            bStartLoop = false;
            StartTask.Wait();
            return false;
        }
    }
}
