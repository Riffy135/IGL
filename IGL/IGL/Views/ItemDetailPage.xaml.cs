using IGL.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace IGL.Views
{
    public partial class ItemDetailPage : ContentPage
    {

        ItemDetailViewModel _viewModel;

        public ItemDetailPage()
        {

            InitializeComponent();
            ToolbarItems.RemoveAt(0);

            BindingContext = _viewModel = new ItemDetailViewModel();
            //BindingContext = _viewModel = new ItemDetailViewModel(this);

        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            _viewModel.OnAppearing();
        }

    }
}