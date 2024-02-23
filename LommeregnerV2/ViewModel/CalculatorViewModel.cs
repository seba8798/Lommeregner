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

        private string displayTextV1 = string.Empty;
        private string displayTextV2 = string.Empty;

        public string DisplayTextV1
        {
            get { return displayTextV1; }
            set { SetProperty(ref displayTextV1, value); }
        }

        public string DisplayTextV2
        {
            get { return displayTextV2; }
            set { SetProperty(ref displayTextV2, value); }
        }

        public ICommand OperationCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand NumberCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand V1BackButton { get; }
        public ICommand V2 { get; }
        public ICommand V2BackButton { get; }

        public CalculatorViewModel()
        {
            OperationCommand = new RelayCommand<string>(OnOperationClicked);
            EqualsCommand = new RelayCommand(OnEqualsClicked);
            NumberCommand = new RelayCommand<string>(OnNumberClicked);
            ClearCommand = new RelayCommand(OnClearClicked);
            BackspaceCommand = new RelayCommand(OnBackspaceClicked);
            V1BackButton = new RelayCommand(OnV1BackButton);
            V2 = new RelayCommand(OnV2);
            V2BackButton = new RelayCommand(OnV2BackButton);
        }

        private void OnV2BackButton()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        //private async void OnV2(string displayTextV1)
        //{
        //    await App.Current.MainPage.Navigation.PushAsync(new CalV2(this, displayTextV1));
        //}
        private async void OnV2()
        {
            string DisplayText = DisplayTextV1; // Get the value of DisplayTextV1
            string uriString = $"///CalV2?displayTextV1={Uri.EscapeDataString(DisplayText)}";
            await Shell.Current.GoToAsync(uriString);
        }

        private void OnV1BackButton()
        {
            App.Current.MainPage.Navigation.PopAsync();
            App.Current.MainPage.Navigation.PopAsync();

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}