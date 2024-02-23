using System.Windows.Input;

namespace LommeregnerV2.Interface
{
    public interface ICalculatorViewModel
    {
        string DisplayTextV1 { get; set; }
        string DisplayTextV2 { get; set; }
        ICommand OperationCommand { get; }
        ICommand EqualsCommand { get; }
        ICommand NumberCommand { get; }
        ICommand ClearCommand { get; }
        ICommand BackspaceCommand { get; }
    }
}
