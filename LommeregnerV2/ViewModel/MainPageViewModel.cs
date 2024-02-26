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

        public ObservableCollection<CalV2> CalculatorsV2 { get; set; }
        public string SavedDisplayTextV2 { get; set; }
        public ICommand NewCalculator { get; private set; }
        public ICommand NewCalculatorV2 { get; private set; }
        public ICommand OpenCalculator { get; }
        public ICommand OpenCalculatorV2 { get; }
        public CalculatorViewModel SavedCalculatorViewModel { get; set; }

        public MainPageViewModel()
        {
            // Initialiser Calculators
            Calculators = new ObservableCollection<CalV1>(); 
            CalculatorsV2 = new ObservableCollection<CalV2>(); 

            SavedDisplayTextV2 = ""; 

            NewCalculator = new Command(() =>
            {
                // Opret ny CalV1 instans
                var calculator = new CalV1(this);
                // Opret ny ViewModel
                var calculatorViewModel = new CalculatorViewModel(this);
                // Sæt BindingContext
                calculator.BindingContext = calculatorViewModel; 

                calculator.Name = DateTime.Now.ToString();
                Calculators.Add(calculator);

                // Naviger til calculator
                Shell.Current.Navigation.PushAsync(calculator); 
            });
            NewCalculatorV2 = new Command(() =>
            {
                // Opret ny ViewModel
                var calculatorViewModel = new CalculatorViewModel(this) { MainPageViewModel = this };

                // Opret ny CalV2 instans
                var calculatorV2 = new CalV2(calculatorViewModel, SavedDisplayTextV2);

                // Sæt BindingContext
                calculatorV2.BindingContext = calculatorViewModel; 

                calculatorV2.Name = DateTime.Now.ToString(); 
                CalculatorsV2.Add(calculatorV2); 

                Shell.Current.Navigation.PushAsync(calculatorV2); 
            });
            OpenCalculator = new Command<CalV1>(calculator =>
            {
                Shell.Current.Navigation.PushAsync(calculator); 
            });
            OpenCalculatorV2 = new Command<CalV2>(calculatorV2 =>
            {
                Shell.Current.Navigation.PushAsync(calculatorV2); 
            });
        }
    }
}