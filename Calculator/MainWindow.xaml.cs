using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Diagnostics;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double? lastNumber;
        double? result;
        SelectedOperator? selectedOperator;
        public MainWindow()
        {
            InitializeComponent();

            AC.Click += AC_Click;
            equals.Click += Equals_Click;
            point.Click += Point_Click;
            sign.Click += Sign_Click;
            percent.Click += Percent_Click;
        }

        
        private void NumberClick(object sender, RoutedEventArgs e)
        {
            Button numberObject = (Button)  sender;
            string numberPressed = numberObject.Content.ToString().Trim();
            if (resultlbl.Content.ToString() == "0")
            {
                resultlbl.Content = numberPressed;
            }
            else
            {
                resultlbl.Content += numberPressed;
            }
        }

        private void OperatorClick(object sender, RoutedEventArgs e)
        {
            
            Button operatorObject = (Button) sender;
            String operatorPressed = operatorObject.Name.ToString();
            lastNumber = Double.Parse(resultlbl.Content.ToString());
           
            if (operatorPressed == "plus")
            {
                selectedOperator = SelectedOperator.Plus;
            }
            else if (operatorPressed == "minus")
            {
                selectedOperator = SelectedOperator.Minus;
            }
            else if (operatorPressed == "multiply")
            {
                selectedOperator = SelectedOperator.Multiply;
            }
            else if (operatorPressed == "divide")
            {
                selectedOperator = SelectedOperator.Divide;
            }
            resultlbl.Content = "0";
        }

        private void AC_Click(object sender, RoutedEventArgs e)
        {
            resultlbl.Content = "0";
            lastNumber = null;
            selectedOperator = null;
        }

        private void Point_Click(object sender, RoutedEventArgs e)
        {
            String text = resultlbl.Content.ToString();
            if (!text.Contains("."))
                resultlbl.Content += ".";
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            String text = resultlbl.Content.ToString();
            if (text == "0") return;
            if (text.StartsWith("-"))
                resultlbl.Content = text.Substring(1);
            else
                resultlbl.Content = "-" + text;
        }

        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultlbl.Content.ToString(), out double value))
            {
                resultlbl.Content = (value / 100).ToString();
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            double secondNumber = Double.Parse(resultlbl.Content.ToString().Trim());
            if (lastNumber == null)
            {
                MessageBox.Show("You have not entered a first number before your operator", "...", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else if (selectedOperator == null)
            {
                MessageBox.Show("You have not entered an operator", "...", MessageBoxButton.OK,
                   MessageBoxImage.Information);
            }
            else if (selectedOperator == SelectedOperator.Divide && secondNumber == 0)
            {
                MessageBox.Show("You cannot divide by zero", "...", MessageBoxButton.OK,
                   MessageBoxImage.Information);
            }
            else
            {
                result = MathService(lastNumber, secondNumber, selectedOperator);
                resultlbl.Content = result;
                lastNumber = null;
                selectedOperator = null;
            }
            
        }

        private static double? MathService(double? first, double second, SelectedOperator? selectedOperator)
        {


            if (selectedOperator == SelectedOperator.Plus)
            {
                return (first + second);
            }
            else if (selectedOperator == SelectedOperator.Minus)
            {
                return (first - second);
            }
            else if (selectedOperator == SelectedOperator.Multiply)
            {
                return (first * second);
            }
            else if (selectedOperator == SelectedOperator.Divide)
            {
                return (first / second);
            }
            else
            {
                return 0;
            }

        }


        public enum SelectedOperator
        {
            Plus,
            Minus,
            Multiply,
            Divide
        }
    }
}
