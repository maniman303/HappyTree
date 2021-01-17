using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Graphics.Drawables;
using Xamarin.Essentials;
using Android.Util;

namespace Happy_Tree
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        bool debug = false;
        double timeScalar = 1.0;
        int record = 0;
        public static int score = 0;
        bool enableButtons = false;
        bool doRandom = true;
        int iApple1 = 2;
        int iApple2 = 3;
        int iApple3 = 4;
        int iApple4 = 1;
        Task tApple1;
        Task tApple2;
        Task tApple3;
        Task tApple4;
        CancellationTokenSource source1 = new CancellationTokenSource();
        CancellationTokenSource source2 = new CancellationTokenSource();
        CancellationTokenSource source3 = new CancellationTokenSource();
        CancellationTokenSource source4 = new CancellationTokenSource();
        private static readonly object _locker = new object();
        Random rnd = new Random();
        List<string> appleSkins = new List<string>();
        void addScore(int n)
        {
            lock (_locker)
            {
                score += n;
                if (score > 999999) score = 999999;
                scoreLabel.Text = score.ToString();
                if (score > Data.getRecord()) recordLabel.Text = score.ToString();
            } 
        }
        int getAppleType(ImageButton o)
        {
            if (o == bApple1) return iApple1;
            else if (o == bApple2) return iApple2;
            else if (o == bApple3) return iApple3;
            else if (o == bApple4) return iApple4;
            return 0;
        }
        void changeApple(int a, int t, bool force = false)
        {
            switch(a)
            {
                case 1:
                    if (iApple1 == t && !force)
                        t = 4;
                    iApple1 = t;
                    bApple1.Source = appleSkins[t - 1];
                    break;
                case 2:
                    if (iApple2 == t && !force)
                        t = 4;
                    iApple2 = t;
                    bApple2.Source = appleSkins[t - 1];
                    break;
                case 3:
                    if (iApple3 == t && !force)
                        t = 4;
                    iApple3 = t;
                    bApple3.Source = appleSkins[t - 1];
                    break;
                case 4:
                    if (iApple4 == t && !force)
                        t = 4;
                    iApple4 = t;
                    bApple4.Source = appleSkins[t - 1];
                    break;
            }
        }
        async void randomApple(int e)
        {
            int type = 1;
            int time = 1;
            Thread.Sleep(600);
            while (doRandom)
            {
                type = rnd.Next(3) + 1;
                time = rnd.Next(5) + 9;
                switch(e)
                {
                    case 1:
                        try
                        {
                            await Task.Delay((int)Math.Max(time * 150 * timeScalar, 0.5), source1.Token);
                        }
                        catch (TaskCanceledException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            source1.Dispose();
                            source1 = new CancellationTokenSource();
                        }
                        break;
                    case 2:
                        try
                        {
                            await Task.Delay((int)Math.Max(time * 150 * timeScalar, 0.5), source2.Token);
                        }
                        catch (TaskCanceledException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            source2.Dispose();
                            source2 = new CancellationTokenSource();
                        }
                        break;
                    case 3:
                        try
                        {
                            await Task.Delay((int)Math.Max(time * 150 * timeScalar, 0.5), source3.Token);
                        }
                        catch (TaskCanceledException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            source3.Dispose();
                            source3 = new CancellationTokenSource();
                        }
                        break;
                    case 4:
                        try
                        {
                            await Task.Delay((int)Math.Max(time * 150 * timeScalar, 0.5), source4.Token);
                        }
                        catch (TaskCanceledException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            source4.Dispose();
                            source4 = new CancellationTokenSource();
                        }
                        break;
                }
                if(doRandom) MainThread.BeginInvokeOnMainThread(() => {
                    changeApple(e, type);
                });
            }
        }
        void decreaseTime()
        {
            timeScalar -= 0.02;
            if (timeScalar < 0.3)
                timeScalar = 0.3;
        }
        async void Apple_Clicked(object sender, System.EventArgs e)
        {
            if (enableButtons)
            {
                decreaseTime();
                int i = getAppleType((ImageButton)sender);
                switch (i)
                {
                    case 1:
                        addScore(100);
                        break;
                    case 2:
                        addScore(50);
                        break;
                    case 3:
                        addScore(10);
                        break;
                    case 4:
                        if (!debug)
                        {
                            Data.save();
                            doRandom = false;
                            source1.Cancel();
                            source2.Cancel();
                            source3.Cancel();
                            source4.Cancel();
                            await Task.Delay(700);
                            AppleFade(0.0, 500);
                            await Task.Delay(500);
                            await Navigation.PushModalAsync(new OverPage(), false);
                        }
                        break;
                    default:
                        addScore(1000);
                        break;
                }
                if (sender == bApple1)
                {
                    source1.Cancel();
                }
                else if (sender == bApple2)
                {
                    source2.Cancel();
                }
                else if (sender == bApple3)
                {
                    source3.Cancel();
                }
                else if (sender == bApple4)
                {
                    source4.Cancel();
                }
            }
        }
        void AppleFade(double o, uint t)
        {
            bApple1.FadeTo(o, t);
            bApple2.FadeTo(o, t);
            bApple3.FadeTo(o, t);
            bApple4.FadeTo(o, t);
        }
        double wtf()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            double width = mainDisplayInfo.Width;
            double height = mainDisplayInfo.Height;
            return width / height;
        }
        public GamePage()
        {
            InitializeComponent();
            var eee = wtf();
            AbsoluteLayout.SetLayoutBounds(bApple1, new Rectangle(0.026, 1.0 - 1.278 * eee, 0.202, 0.202));
            AbsoluteLayout.SetLayoutBounds(bApple2, new Rectangle(0.286, 1.0 - 1.078 * eee, 0.202, 0.202));
            AbsoluteLayout.SetLayoutBounds(bApple3, new Rectangle(0.7, 1.0 - 1.05 * eee, 0.202, 0.202));
            AbsoluteLayout.SetLayoutBounds(bApple4, new Rectangle(0.96, 1.0 - 1.342 * eee, 0.202, 0.202));
            appleSkins.Add("drawable/AppleYellow.png");
            appleSkins.Add("drawable/AppleRed.png");
            appleSkins.Add("drawable/AppleGreen.png");
            appleSkins.Add("drawable/AppleBrown.png");
        }
        override protected bool OnBackButtonPressed()
        {
            Data.save();
            doRandom = false;
            Navigation.PopModalAsync(false);
            return true;
        }
        override protected void OnAppearing()
        {
            timeScalar = 1.0;
            score = 0;
            recordLabel.Text = Data.getRecordString();
            record = Data.getRecord();
            scoreLabel.Text = "0";
            changeApple(1, 1, true);
            changeApple(2, 2, true);
            changeApple(3, 3, true);
            changeApple(4, 4, true);
            recordLabel.FadeTo(1.0, 700);
            scoreLabel.FadeTo(1.0, 700);
            bApple1.FadeTo(1.0, 500);
            bApple2.FadeTo(1.0, 500);
            bApple3.FadeTo(1.0, 500);
            bApple4.FadeTo(1.0, 500);
            doRandom = true;
            tApple1 = new Task(() => { randomApple(1); });
            tApple2 = new Task(() => { randomApple(2); });
            tApple3 = new Task(() => { randomApple(3); });
            tApple4 = new Task(() => { randomApple(4); });
            tApple1.Start();
            tApple2.Start();
            tApple3.Start();
            tApple4.Start();
            enableButtons = true;
        }
}
}