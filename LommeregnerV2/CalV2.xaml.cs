using LommeregnerV2.ViewModel;
using System.Web;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LommeregnerV2;

public partial class CalV2 : ContentPage
{
    public string DisplayText { get; set; }
    private CalculatorViewModel _viewModel;
    public DateTime CreationTime { get; }
    public string Name { get; set; }

    public CalV2(CalculatorViewModel calculatorViewModel, string displayTextV1)
    {
        InitializeComponent();
        _viewModel = calculatorViewModel; 
        BindingContext = _viewModel;
        _viewModel.DisplayTextV2 = displayTextV1;
    }
}