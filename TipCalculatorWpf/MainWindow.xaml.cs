using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace TipCalculatorWpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void PreviewAmountInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]*(?:\.[0-9]{0,2})?$");
        }

        public void PreviewTipInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]*$");
        }
    }

}