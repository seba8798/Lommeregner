using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using LommeregnerV2.ViewModel;

namespace LommeregnerV2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
            MainThread.InvokeOnMainThreadAsync(async () =>
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                string text = "Welcome Back";
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 20;

                var toast = Toast.Make(text, duration, fontSize);

                await toast.Show(cancellationTokenSource.Token);
            });
        }

    }
}
