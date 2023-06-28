using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string currentInput;
        private string previousInput;
        private string currentOperator;
        private bool isOperatorClicked;

        private string previousOperation;
        public string PreviousOperation
        {
            get { return previousOperation; }
            set
            {
                previousOperation = value;
                OnPropertyChanged("PreviousOperation");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ClearCalculator();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (isOperatorClicked)
            {
                currentInput = "";
                isOperatorClicked = false;
            }

            Button button = (Button)sender;
            currentInput += button.Content.ToString();
            resultTextBox.Text = currentInput;
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            currentOperator = button.Content.ToString();
            previousInput = currentInput;
            isOperatorClicked = true;
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            double operand1 = Convert.ToDouble(previousInput);
            double operand2 = Convert.ToDouble(currentInput);
            double result = 0;

            switch (currentOperator)
            {
                case "+":
                    result = operand1 + operand2;
                    break;
                case "-":
                    result = operand1 - operand2;
                    break;
                case "*":
                    result = operand1 * operand2;
                    break;
                case "/":
                    if (operand2 != 0)
                        result = operand1 / operand2;
                    else
                        MessageBox.Show("Cannot divide by zero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "%":
                    result = operand1 % operand2;
                    break;
                case "^":
                    result = Math.Pow(operand1, operand2);
                    break;
            }

            currentInput = result.ToString();
            resultTextBox.Text = currentInput;
            isOperatorClicked = true;
            PreviousOperation = $"{previousInput} {currentOperator} {currentInput} = {result}";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearCalculator();
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!currentInput.Contains("."))
                currentInput += ".";
            resultTextBox.Text = currentInput;
        }

        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {
            double number = Convert.ToDouble(currentInput);
            double result = Math.Sqrt(number);
            currentInput = result.ToString();
            resultTextBox.Text = currentInput;
        }

        private void ReciprocalButton_Click(object sender, RoutedEventArgs e)
        {
            double number = Convert.ToDouble(currentInput);
            if (number != 0)
            {
                double result = 1 / number;
                currentInput = result.ToString();
                resultTextBox.Text = currentInput;
            }
            else
            {
                MessageBox.Show("Cannot divide by zero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FactorialButton_Click(object sender, RoutedEventArgs e)
        {
            int number = Convert.ToInt32(currentInput);
            if (number >= 0)
            {
                int result = 1;
                for (int i = 2; i <= number; i++)
                    result *= i;
                currentInput = result.ToString();
                resultTextBox.Text = currentInput;
            }
            else
            {
                MessageBox.Show("Factorial is defined only for non-negative integers.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogarithmButton_Click(object sender, RoutedEventArgs e)
        {
            double number = Convert.ToDouble(currentInput);
            double result = Math.Log10(number);
            currentInput = result.ToString();
            resultTextBox.Text = currentInput;
        }

        private void FloorButton_Click(object sender, RoutedEventArgs e)
        {
            double number = Convert.ToDouble(currentInput);
            double result = Math.Floor(number);
            currentInput = result.ToString();
            resultTextBox.Text = currentInput;
        }

        private void CeilingButton_Click(object sender, RoutedEventArgs e)
        {
            double number = Convert.ToDouble(currentInput);
            double result = Math.Ceiling(number);
            currentInput = result.ToString();
            resultTextBox.Text = currentInput;
        }

        private void ClearCalculator()
        {
            currentInput = "";
            previousInput = "";
            currentOperator = "";
            isOperatorClicked = false;
            resultTextBox.Text = "0";
        }
    }
}