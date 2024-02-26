using CommunityToolkit.Maui.Core;
using LommeregnerV2.ViewModel;
using CommunityToolkit.Maui.Alerts;

namespace LommeregnerV2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            // Tilmeld OnMainPageAppearing til Loaded event
            App.Current.MainPage.Loaded += OnMainPageAppearing;

            InitializeComponent();

            // Sæt BindingContext
            BindingContext = new MainPageViewModel();
        }
        private async void OnMainPageAppearing(object sender, EventArgs e)
        {
            // Unsubscribe tilmelding efter første kørsel
            App.Current.MainPage.Appearing -= OnMainPageAppearing;

            // Tjek om appen er kørt før
            bool hasBeenRunBefore = Preferences.Get("HasBeenRunBefore", false);

            if (!hasBeenRunBefore)
            {
                // Opret CancellationTokenSource
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