using LommeregnerV2.ViewModel;

namespace LommeregnerV2;

public partial class CalV2 : ContentPage
{
    public string DisplayText { get; set; }
    private CalculatorViewModel _viewModel;
    public DateTime CreationTime { get; }
    public string Name { get; set; }

    public CalV2(CalculatorViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;  // Use the passed in view model
        BindingContext = _viewModel;
        Name = string.Empty;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.DisplayTextV2 = DisplayText;
    }
}