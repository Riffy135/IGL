using Android.Content.Res;
using Plugin.SimpleAudioPlayer;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static IGL.Views.AudioPage;
using System.Collections.ObjectModel;

[assembly: Dependency(typeof(IGL.Views.AudioPage.ReadFileService))]


namespace IGL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AudioPage : ContentPage
    {
        ISimpleAudioPlayer player;
        public ObservableCollection<Models.ItemDets> AudioDets { get; }


        public AudioPage()
        {
            InitializeComponent();
            InitControls();

            string FName = GlobalVars.AudioFile;
            var stream = DependencyService.Get<IReadFile>().ReadAudioStream(FName);
            Label1.Text = GlobalVars.AudioType;
            Label2.Text = GlobalVars.AudioTitle;
            Label3.Text = GlobalVars.AudioAuthor;

            //var stream = GetStreamFromFile(FName);
            player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load(stream);
            sliderPosition.Maximum = player.Duration;
            sliderPosition.IsEnabled = player.CanSeek;
            Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);
            player.Play();
        }

        void InitControls()
        {
            sliderVolume.Value = 30;

            sliderVolume.ValueChanged += SliderVolumeValueChanged;
            sliderPosition.ValueChanged += SliderPostionValueChanged;

        }

        private void SliderPostionValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sliderPosition.Value != player.Duration)
                player.Seek(sliderPosition.Value);
        }

        private void SliderVolumeValueChanged(object sender, ValueChangedEventArgs e)
        {
            player.Volume = sliderVolume.Value;
        }

        private void StopAudio(object sender, EventArgs e)
        {

            player.Stop();
        }

        private void PauseAudio(object sender, EventArgs e)
        {
            player.Pause();
        }

        private void PlayAudio(object sender, EventArgs e)
        {
            player.Play();

            sliderPosition.Maximum = player.Duration;
            sliderPosition.IsEnabled = player.CanSeek;

            Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);
        }

        bool UpdatePosition()
        {
            lblPosition.Text = $"Position: {(int)player.CurrentPosition} / {(int)player.Duration}";

            sliderPosition.ValueChanged -= SliderPostionValueChanged;
            sliderPosition.Value = player.CurrentPosition;
            sliderPosition.ValueChanged += SliderPostionValueChanged;

            return player.IsPlaying;
        }

        //Stream GetStreamFromFile(string filename)
        //{
        //    var assembly = typeof(App).GetTypeInfo().Assembly;

        //    var stream = assembly.GetManifestResourceStream(filename);

        //    return stream;
        //}

        public interface IReadFile
        {
            Stream ReadAudioStream(string FName);
        }


        internal class ReadFileService : IReadFile
        {

            public Stream ReadAudioStream(string FName)
            {
                AssetManager assets = Android.App.Application.Context.Assets;
                Stream stream = assets.Open(FName);
                return stream;
            }

        }

        private void ExitAudio(object sender, System.EventArgs e)
        {
            player.Stop();
            Shell.Current.Navigation.PopAsync();

            //Shell.Current.CurrentItem.CurrentItem = Shell.Current.CurrentItem.Items[0];
            //Shell.Current.GoToAsync("//ItemsPage");
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.Resources["TabVisible"] = false;
            return false;
        }
    }
}