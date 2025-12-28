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
using System.Reflection;

namespace efficiencyCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Device> devices { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            loadDeviceTypes();
            refreshDevicesTable();
        }

        private void txtDeviceName_GotFocus(object sender, RoutedEventArgs e)
        {
            hidePlaceholder(sender, "Naziv uređaja");
        }

        private void txtDeviceName_LostFocus(object sender, RoutedEventArgs e)
        {
            showPlaceholder(sender, "Naziv uređaja");
        }

        private void txtDevicePower_GotFocus(object sender, RoutedEventArgs e)
        {
            hidePlaceholder(sender, "Snaga uređaja (W)");
        }

        private void txtDevicePower_LostFocus(object sender, RoutedEventArgs e)
        {
            showPlaceholder(sender, "Snaga uređaja (W)");
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private void btnAddDevice_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDeviceName.Text)
                && txtDeviceName.Text != "Naziv uređaja"
                && double.TryParse(txtDevicePower.Text, out double power)
                && sldHours.Value != 0
                && cmbType.SelectedValue != null)
            {
                addDevice();
            }

            else
            {
                MessageBox.Show("Popunite oba polja za unos i izaberite tip uređaja i broj sati.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDeviceName.Clear();
                txtDevicePower.Clear();
                sldHours.Value = 0;
                cmbType.SelectedIndex = -1;
                return;
            }
        }

        private void showPlaceholder(object sender, string placeholder)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = placeholder;
                textBox.Foreground = new SolidColorBrush(Color.FromRgb(105, 111, 199));
            }
        }
        private void hidePlaceholder(object sender, string placeholder)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == placeholder)
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Gray;
            }
        }
        private void loadDeviceTypes()
        {
            DevicesOperations.loadTypes(cmbType);
        }

        private void refreshDevicesTable()
        {
            if (spDevices.Children.Count > 0)
                spDevices.Children.Clear();

            devices = new ObservableCollection<Device>(DevicesOperations.loadDevices());

            foreach (var device in devices)
                spDevices.Children.Add(new DeviceRow(device));
        }

        private void addDevice()
        {
            Device newDevice = Calculator.calculate(txtDeviceName.Text, Double.Parse(txtDevicePower.Text), sldHours.Value);

            var selectedType = cmbType.SelectedItem as DeviceType;
            if (selectedType != null)
            {
                newDevice.DeviceType = selectedType;
                newDevice.IdDeviceType = selectedType.Id;
            }

            Calculator.calculateClass(newDevice);
            EnergyClassHelper.setBackground(brdEnergyClass, newDevice.Class);

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
