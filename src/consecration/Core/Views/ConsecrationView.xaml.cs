using Xamarin.Forms;

namespace Consecration.Core
{
    public partial class ConsecrationView : ContentPage
    {
        public ConsecrationView()
        {
            InitializeComponent();

            BindingContext = new MyTripCountdownViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.LoadAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.UnloadAsync();
        }
    }
}