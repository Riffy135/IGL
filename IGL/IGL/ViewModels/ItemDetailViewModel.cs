using IGL.Models;
using IGL.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IGL.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        // Leon - App Crashes on Line 96

        private ItemDetailPage itemDetailPage;

        public Command PageAppearCommand { get; set; }
        public ItemDetailViewModel(ItemDetailPage itemDetailPage)
        {
            this.itemDetailPage = itemDetailPage;
            PageAppearCommand = new Command(OnAppearing);
        }

        //public ItemDetailPage MyItemDetailPage { get; }
        public ObservableCollection<ItemDets> ItemDets { get; }

        public Command LoadItemDetailsCommand { get; }
        public Command<ItemDets> ItemTapped { get; }

        public Command AudioDetPageCommand { get; set; }


        bool AllowAudio = false;
        public ItemDetailViewModel()
        {

            ItemDets = new ObservableCollection<ItemDets>();
            LoadItemDetailsCommand = new Command(() => ExecuteLoadItemDetsCommand());
            AudioDetPageCommand = new Command(AudioDetPageDisp);

        }
        void ExecuteLoadItemDetsCommand()
        {
            try
            {
                IsBusy = true;
                ItemDets.Clear();

                switch (AppShell.Current.CurrentItem.ClassId)
                {
                    case "1":
                        switch (GlobalVars.CurrItem)
                        {

                            case 1:
                                AllowAudio = true;
                                ItemDets.Add(new ItemDets { DetId = Guid.NewGuid().ToString(), DetText = "Detail Flyout Item 1A A", DetDesc = "Want Audio Button Displayed on Toolbar" });
                                ItemDets.Add(new ItemDets { DetId = Guid.NewGuid().ToString(), DetText = "Detail Flyout Item 1A B", DetDesc = "Want Audio Button Displayed on Toolbar" });
                                break;
                            case 2:
                                AllowAudio = false;
                                ItemDets.Add(new ItemDets { DetId = Guid.NewGuid().ToString(), DetText = "Detail Flyout Item 1B A", DetDesc = "Don't Want Audio Button Displayed on Toolbar" });
                                ItemDets.Add(new ItemDets { DetId = Guid.NewGuid().ToString(), DetText = "Detail Flyout Item 1B B", DetDesc = "Don't Want Audio Button Displayed on Toolbar" });
                                break;
                        }
                        break;
                    case "2":
                        switch (GlobalVars.CurrItem)
                        {

                            case 1:
                                AllowAudio = false;
                                ItemDets.Add(new ItemDets { DetId = Guid.NewGuid().ToString(), DetText = "Detail Flyout Item 2A A", DetDesc = "Don't Want Audio Button Displayed on Toolbar" });
                                ItemDets.Add(new ItemDets { DetId = Guid.NewGuid().ToString(), DetText = "Detail Flyout Item 2A B", DetDesc = "Don't Want Audio Button Displayed on Toolbar" });
                                break;
                            case 2:
                                AllowAudio = true;
                                ItemDets.Add(new ItemDets { DetId = Guid.NewGuid().ToString(), DetText = "Detail Flyout Item 2B A", DetDesc = "Want Audio Button Displayed on Toolbar" });
                                ItemDets.Add(new ItemDets { DetId = Guid.NewGuid().ToString(), DetText = "Detail Flyout Item 2B B", DetDesc = "Want Audio Button Displayed on Toolbar" });
                                break;
                        }
                        break;
                }

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                if (AllowAudio == true)
                {
                    var ToolbarText = new ToolbarItem { Text = "Audio" };
                    this.itemDetailPage.ToolbarItems.Add(ToolbarText);
                }
                IsBusy = false;
            }

        }
        
        public void OnAppearing()
        {
            //if (AllowAudio == true)
            //{
            //    var ToolbarText = new ToolbarItem { Text = "Audio" };
            //    MyItemDetailPage.ToolbarItems.Add(ToolbarText);
            //}
            IsBusy = true;
        }

        private async void AudioDetPageDisp(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AudioPage));

        }

             
    }


}
