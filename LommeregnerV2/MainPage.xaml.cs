using CommunityToolkit.Maui.Core;
using LommeregnerV2.ViewModel;
using CommunityToolkit.Maui.Alerts;

namespace LommeregnerV2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            App.Current.MainPage.Loaded += OnMainPageAppearing;
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
        private async void OnMainPageAppearing(object sender, EventArgs e)
        {
            App.Current.MainPage.Appearing -= OnMainPageAppearing; // Unsubscribe after first run

            bool hasBeenRunBefore = Preferences.Get("HasBeenRunBefore", false);

            if (!hasBeenRunBefore)
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                string text = "Welcome";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 20;

                var toast = Toast.Make(text, duration, fontSize);

                await toast.Show(cancellationTokenSource.Token);
            }
        }
    }
}