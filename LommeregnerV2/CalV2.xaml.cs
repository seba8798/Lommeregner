using LommeregnerV2.ViewModel;

namespace LommeregnerV2;

public partial class CalV2 : ContentPage
{
    public string DisplayText { get; set; }

    public CalV2(CalculatorViewModel viewModel)
    {
        InitializeComponent();
        DisplayText = viewModel.DisplayText;
        BindingContext = viewModel;
    }
}