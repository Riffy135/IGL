using IGL.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IGL.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; set; }
        //ToolbarItems.Remove(ToolbarDisplay);

        public Command LoadItemsCommand { get; }
        public Command<Item> ItemTapped { get; }

        private readonly ObservableCollection<Item> items = new ObservableCollection<Item>();

        public ObservableCollection<Item> Item { get { return items; } }

        private int ItemNumber;

        public class IVM : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string name = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public ItemsViewModel()
        {
            Items = new ObservableCollection<Item>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);
        }


        Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                switch (Shell.Current.CurrentItem.ClassId)
                {
                    //build data for ItemsPage
                    case "1":
                        Title = "Flyout Item 1";

                        items.Clear();
                        ItemNumber = 0;
                        items.Add(new Item { Id = Guid.NewGuid().ToString(), ItemNo = ++ItemNumber, Text = "Flyout Item 1A" });
                        items.Add(new Item { Id = Guid.NewGuid().ToString(), ItemNo = ++ItemNumber, Text = "Flyout Item 1B" });

                        break;
                    case "2":

                        Title = "Flyout Item 2";

                        items.Clear();
                        ItemNumber = 0;
                        items.Add(new Item { Id = Guid.NewGuid().ToString(), ItemNo = ++ItemNumber, Text = "Flyout Item 2A" });
                        items.Add(new Item { Id = Guid.NewGuid().ToString(), ItemNo = ++ItemNumber, Text = "Flyout Item 2B" });
                        break;

                    default:
                        break;
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            return Task.CompletedTask;
        }

        public void OnAppearing()
        {

            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }


        private async void OnItemSelected(Item item)
        {
            if (item == null)
                return;
            if (item.ItemNo > 0)
            {
                GlobalVars.ItemSubTitle = item.Text;
                GlobalVars.CurrItem = item.ItemNo;

                await Shell.Current.GoToAsync("/ItemDetailPage");
            }


        }
    }
}