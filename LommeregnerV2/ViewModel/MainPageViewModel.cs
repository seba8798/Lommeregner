using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LommeregnerV2.ViewModel
{
    public class MainPageViewModel
    {
        public ObservableCollection<CalV1> Calculators { get; set; }
        public ICommand NewCalculator { get; private set; }
        public ICommand OpenCalculator { get; }

        public MainPageViewModel()
        {
            Calculators = new ObservableCollection<CalV1>();
            NewCalculator = new Command(() =>
            {
                var calculator = new CalV1(); // Opret en ny instans af CalV1 for hver lommeregner
                var calculatorViewModel = new CalculatorViewModel();
                calculator.BindingContext = calculatorViewModel;
                calculator.Name = DateTime.Now.ToString();
                Calculators.Add(calculator);
                Shell.Current.Navigation.PushAsync(calculator);
            });
            OpenCalculator = new Command<CalV1>(calculator =>
            {
                Shell.Current.Navigation.PushAsync(calculator);
            });
        }
    }
}