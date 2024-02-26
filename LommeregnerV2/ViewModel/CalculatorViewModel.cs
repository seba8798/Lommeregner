using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LommeregnerV2.Interface;

namespace LommeregnerV2.ViewModel
{
    public class CalculatorViewModel : ObservableRecipient, ICalculatorViewModel
    {
        private double currentNumber, resultNumber;
        private bool isResultDisplayed;

        private string? currentOperation;
        private string displayTextV1 = string.Empty;
        private string displayTextV2 = string.Empty;

        private CalculatorViewModel tempCalculatorViewModel;
        public CalV2 CalV2 { get; set; }
        public MainPageViewModel MainPageViewModel { get; set; }


        public string DisplayTextV1
        {
            get { return displayTextV1; }
            set { SetProperty(ref displayTextV1, value); } // Opdaterer displayTextV1
        }

        public string DisplayTextV2
        {
            get { return displayTextV2; }
            set { SetProperty(ref displayTextV2, value); } // Opdaterer displayTextV2
        }

        public ICommand OperationCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand NumberCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand BackButton { get; }
        public ICommand V2BackButton { get; }
        public ICommand V2 { get; }
        public ICommand SaveButton { get; }

        public CalculatorViewModel(MainPageViewModel mainPageViewModel)
        {

            MainPageViewModel = mainPageViewModel;
            OperationCommand = new RelayCommand<string>(OnOperationClicked); // Kommando til at håndtere operationer
            EqualsCommand = new RelayCommand(OnEqualsClicked); // Kommando til at håndtere lighedstegn
            NumberCommand = new RelayCommand<string>(OnNumberClicked); // Kommando til at håndtere tal
            ClearCommand = new RelayCommand(OnClearClicked); // Kommando til at rydde displayet
            BackspaceCommand = new RelayCommand(OnBackspaceClicked); // Kommando til at slette det sidste tegn
            BackButton = new RelayCommand(OnBackButton); // Kommando til at gå tilbage til hovedsiden
            V2 = new Command<string>(async (displayTextV1) => await OnV2(displayTextV1)); // Kommando til at skifte til V2
            CalV2 = new CalV2(this, string.Empty); // Opretter en ny instans af CalV2
            SaveButton = new Command(OnSaveButton); // Kommando til at gemme resultatet
        }
        private void OnBackButton()
        {
            // Gå tilbage til hovedsiden
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        // Gemmer resultatet fra CalV2
        private void OnSaveButton()
        {
            try
            {
                // Create a new CalculatorViewModel instance
                var newCalculatorViewModel = new CalculatorViewModel(MainPageViewModel);

                // Initialize CalV2 with the new CalculatorViewModel instance
                CalV2 = new CalV2(newCalculatorViewModel, DisplayTextV2)
                {
                    Name = DateTime.Now.ToString()
                };

                // Add CalV2 to CalculatorsV2
                MainPageViewModel.CalculatorsV2.Add(CalV2);
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
                // Log the error or show a message to the user
                Console.WriteLine(ex.Message);
            }
        }

        private async Task OnV2(string displayTextV1)
        {
            try
            {
                // Create a new CalculatorViewModel instance
                var newCalculatorViewModel = new CalculatorViewModel(MainPageViewModel);

                // Create a new CalV2 instance with the new CalculatorViewModel instance
                var calV2Page = new CalV2(newCalculatorViewModel, displayTextV1);

                // Push the new page onto the navigation stack
                await Shell.Current.Navigation.PushAsync(calV2Page);
            }
            catch (Exception ex)
            {
                // Log the error or show a message to the user
                Console.WriteLine(ex.Message);
            }
        }
        private void OnNumberClicked(string number)
        {
            if (isResultDisplayed)
            {
                DisplayTextV1 = string.Empty;
                DisplayTextV2 = string.Empty;
                isResultDisplayed = false;
            }

            DisplayTextV1 += number;
            DisplayTextV2 += number;

            // Opret en ny CalculatorViewModel instans
            tempCalculatorViewModel = new CalculatorViewModel(MainPageViewModel);
        }


        private void OnOperationClicked(string operation)
        {
            //DisplayTextV1
            if (string.IsNullOrEmpty(DisplayTextV1) || !double.TryParse(DisplayTextV1, out currentNumber))
            {
                return;
            }
            else
            {
                currentOperation
                    = operation;
                DisplayTextV1 = string.Empty;
                isResultDisplayed = false;
            }
            //DisplayTextV2
            if (string.IsNullOrEmpty(DisplayTextV2) || !double.TryParse(DisplayTextV2, out currentNumber))
            {
                return;
            }
            else
            {
                currentOperation
                    = operation;
                DisplayTextV2 = string.Empty;
                isResultDisplayed = false;
            }
        }


        private async void OnEqualsClicked()
        {
            //DisplayTextV1
            if (double.TryParse(DisplayTextV1, out double nextNumberV1))
            {
                try
                {
                    switch (currentOperation)
                    {
                        case "+":
                            resultNumber = currentNumber + nextNumberV1;
                            break;
                        case "-":
                            resultNumber = currentNumber - nextNumberV1;
                            break;
                        case "*":
                            resultNumber = currentNumber * nextNumberV1;
                            break;
                        case "/":
                            if (nextNumberV1 == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            resultNumber = currentNumber / nextNumberV1;
                            break;
                        default:
                            throw new InvalidOperationException("Unsupported operation");
                    }

                    if (resultNumber == Math.Truncate(resultNumber))
                    {
                        DisplayTextV1 = resultNumber.ToString("F0");
                    }
                    else if (resultNumber == Math.Round(resultNumber, 2))
                    {
                        DisplayTextV1 = resultNumber.ToString("F2");
                    }
                    else
                    {
                        DisplayTextV1 = resultNumber.ToString("F10");
                    }

                    if (resultNumber == 69 || resultNumber == 80085)
                    {
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await Application.Current.MainPage.DisplayAlert("Toast", "Nice!", "OK");
                        });
                    }

                    isResultDisplayed = true;
                }
                catch (Exception ex)
                {
                    DisplayTextV1 = ex.Message;
                }
            }
            else
            {
                DisplayTextV1 = "Invalid input";
            }

            //DisplayTextV2
            if (double.TryParse(DisplayTextV2, out double nextNumberV2))
            {
                try
                {
                    switch (currentOperation)
                    {
                        case "+":
                            resultNumber = currentNumber + nextNumberV2;
                            break;
                        case "-":
                            resultNumber = currentNumber - nextNumberV2;
                            break;
                        case "*":
                            resultNumber = currentNumber * nextNumberV2;
                            break;
                        case "/":
                            if (nextNumberV2 == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            resultNumber = currentNumber / nextNumberV2;
                            break;
                        default:
                            throw new InvalidOperationException("Unsupported operation");
                    }

                    if (resultNumber == Math.Truncate(resultNumber))
                    {
                        DisplayTextV2 = resultNumber.ToString("F0");
                    }
                    else if (resultNumber == Math.Round(resultNumber, 2))
                    {
                        DisplayTextV2 = resultNumber.ToString("F2");
                    }
                    else
                    {
                        DisplayTextV2 = resultNumber.ToString("F10");
                    }

                    if (resultNumber == 69 || resultNumber == 80085)
                    {
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await Application.Current.MainPage.DisplayAlert("Toast", "Nice!", "OK");
                        });
                    }

                    isResultDisplayed = true;
                }
                catch (Exception ex)
                {
                    DisplayTextV2 = ex.Message;
                }
            }
            else
            {
                DisplayTextV2 = "Invalid input";
            }
        }

        private void OnClearClicked()
        {
            DisplayTextV1 = string.Empty;
            DisplayTextV2 = string.Empty;
        }

        private void OnBackspaceClicked()
        {
            if (!string.IsNullOrEmpty(DisplayTextV1))
            {
                DisplayTextV1 = DisplayTextV1.Substring(0, DisplayTextV1.Length - 1);
            }
            if (!string.IsNullOrEmpty(DisplayTextV2))
            {
                DisplayTextV2 = DisplayTextV2.Substring(0, DisplayTextV2.Length - 1);
            }
        }
    }
}
