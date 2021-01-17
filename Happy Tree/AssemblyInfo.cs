using Android.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("ComicSansMSBold.ttf", Alias = "Comic")]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
#if DEBUG
[assembly: Application(Debuggable=true)]
#else
[assembly: Application(Debuggable = false)]
#endif