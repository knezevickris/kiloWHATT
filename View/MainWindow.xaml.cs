using efficiencyCalculator.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using efficiencyCalculator.Model;
using efficiencyCalculator.Helpers;
using System.Collections.ObjectModel;
using efficiencyCalculator.View.UserComponents;

namespace efficiencyCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Device> devices { get; set;}
        public MainWindow()
        {
            InitializeComponent();

            refreshDevicesTable();
        }

        
        private void btnSettings_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            if(button != null)
            {
                button.ToolTip = "Podešavanja";
            }
        }

        private void btnSettings_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            if(button != null)
            {
                button.ToolTip = null;
            }
        }

        private void txtDeviceName_GotFocus(object sender, RoutedEventArgs e)
        {
           var textBox = sender as TextBox;
           if(textBox.Text == "Naziv uređaja")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void txtDeviceName_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Naziv uređaja";
                textBox.Foreground = new SolidColorBrush(Color.FromRgb(105, 111, 199));
            }
        }

        private void txtDevicePower_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if(textBox.Text == "Snaga uređaja (W)")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void txtDevicePower_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Snaga uređaja (W)";
                textBox.Foreground = new SolidColorBrush(Color.FromRgb(105, 111, 199));
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private void btnAddDevice_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtDeviceName.Text) 
                && txtDeviceName.Text != "Unesite naziv uređaja" 
                && double.TryParse(txtDevicePower.Text, out double power) 
                && sldHours.Value != 0)
            {
                addDevice();
            }

            else
            {
                MessageBox.Show("Popunite oba polja za unos i izaberite broj sati.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDeviceName.Clear();
                txtDevicePower.Clear();
                sldHours.Value = 0;
                return;
            }
        }

        private void refreshDevicesTable()
        {
            if(spDevices.Children.Count>0)
                spDevices.Children.Clear();

            devices = new ObservableCollection<Device>(DevicesOperations.loadDevices());

            foreach (var device in devices)
                spDevices.Children.Add(new DeviceRow(device));
        }

        private void addDevice()
        {
            Device newDevice = Calculator.calculate(txtDeviceName.Text, Double.Parse(txtDevicePower.Text), sldHours.Value);

            lblDailyConsumption.Content = $"{newDevice.DailyConsumption:F2} kWh";
            lblDailyExpense.Content = $"{newDevice.DailyExpense:F2} KM";
            lblMonthlyConsumption.Content = $"{newDevice.Consumption:F2} kWh";
            lblMonthlyExpense.Content = $"{newDevice.Expense:F2} KM";
            lblEnergyClass.Content = newDevice.Class;

            DevicesOperations.insertDevice(newDevice);

            refreshDevicesTable();
        }
    }
}
