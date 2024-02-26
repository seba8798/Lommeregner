using LommeregnerV2.ViewModel;

namespace LommeregnerV2
{
    public partial class CalV1 : ContentPage
    {
        private CalculatorViewModel _viewModel;

        public DateTime CreationTime { get; }
        public string Name { get; set; }

        public CalV1(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            CreationTime = DateTime.Now;
            _viewModel = new CalculatorViewModel(mainPageViewModel);
            BindingContext = _viewModel;
        }
    }
}
