using IGL.Services;
using Xamarin.Forms;

namespace IGL
{

    public class GlobalVars
    {
        public static int CurrItem;
        public static string ItemSubTitle;
        public static string AudioFile;
        public static string AudioType;
        public static string AudioTitle;
        public static string AudioAuthor;
        public static string AudioReciter;
    }
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
