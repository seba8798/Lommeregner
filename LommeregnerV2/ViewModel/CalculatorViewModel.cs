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
        private string? currentOperation;
        private bool isResultDisplayed;
        private string displayText = string.Empty;

        public string DisplayText
        {
            get { return displayText; }
            set { SetProperty(ref displayText, value); }
        }

        public ICommand OperationCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand NumberCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand BackspaceCommand { get; }

        public CalculatorViewModel()
        {
            OperationCommand = new RelayCommand<string>(OnOperationClicked);
            EqualsCommand = new RelayCommand(OnEqualsClicked);
            NumberCommand = new RelayCommand<string>(OnNumberClicked);
            ClearCommand = new RelayCommand(OnClearClicked);
            BackspaceCommand = new RelayCommand(OnBackspaceClicked);
        }
        private void OnV2()
        {
            App.Current.MainPage.Navigation.PushAsync(new CalV2(this));
        }
        private void OnBackButton()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        private void OnOperationClicked(string operation)
        {
            if (string.IsNullOrEmpty(DisplayText) || !double.TryParse(DisplayText, out currentNumber))
            {
                return;
            }
            else
            {
                currentOperation = operation;
                DisplayText = string.Empty;
                isResultDisplayed = false;
            }
        }


        private async void OnEqualsClicked()
        {
            if (double.TryParse(DisplayText, out double nextNumber))
            {
                try
                {
                    switch (currentOperation)
                    {
                        case "+":
                            resultNumber = currentNumber + nextNumber;
                            break;
                        case "-":
                            resultNumber = currentNumber - nextNumber;
                            break;
                        case "*":
                            resultNumber = currentNumber * nextNumber;
                            break;
                        case "/":
                            if (nextNumber == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            resultNumber = currentNumber / nextNumber;
                            break;
                        default:
                            throw new InvalidOperationException("Unsupported operation");
                    }

                    if (resultNumber == Math.Truncate(resultNumber))
                    {
                        DisplayText = resultNumber.ToString("F0");
                    }
                    else if (resultNumber == Math.Round(resultNumber, 2))
                    {
                        DisplayText = resultNumber.ToString("F2");
                    }
                    else
                    {
                        DisplayText = resultNumber.ToString("F10");
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
                    DisplayText = ex.Message;
                }
            }
            else
            {
                DisplayText = "Invalid input";
            }
        }

        private void OnNumberClicked(string number)
        {
            if (isResultDisplayed)
            {
                DisplayText = string.Empty;
                isResultDisplayed = false;
            }

            DisplayText += number;
        }


        private void OnClearClicked()
        {
            DisplayText = string.Empty;
        }

        private void OnBackspaceClicked()
        {
            if (!string.IsNullOrEmpty(DisplayText))
            {
                DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}