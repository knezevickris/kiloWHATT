using Google.Protobuf;
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
using System.Windows.Shapes;

namespace efficiencyCalculator.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            txtPrice.Text = Convert.ToString(Properties.Settings.Default.Price);
            lblLastUpdated.Content = Properties.Settings.Default.PriceUpdated;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(AdjustPrice() == true || AdjustTheme() == true)
            {
                Properties.Settings.Default.Save();
            }

            this.Close();
        }

        private void ApplyTheme(string theme)
        {
            var toRemove = Application.Current.Resources.MergedDictionaries.Where(d => d.Source != null &&
                   d.Source.OriginalString.Contains("LightTheme.xaml") ||
                   d.Source.OriginalString.Contains("DarkTheme.xaml")).ToList();

            foreach (var dic in toRemove)
                Application.Current.Resources.MergedDictionaries.Remove(dic);

            string path = theme == "Tamni režim"
                                    ? "/Resources/DarkTheme.xaml"
                                    : "/Resources/LightTheme.xaml";

            var dict = new ResourceDictionary
            {
                Source = new Uri(path, UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Add(dict);

        }

        private bool AdjustPrice()
        {
            if (txtPrice.Text != Properties.Settings.Default.Price.ToString())
            {
                Properties.Settings.Default.Price = Convert.ToDouble(txtPrice.Text);
                Properties.Settings.Default.PriceUpdated = DateTime.Today.ToString("dd.MM.yyyy");
                return true;
            }
            else
                return false;
        }

        private bool AdjustTheme()
        {
            string selectedTheme = (cmbTheme.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (!string.IsNullOrWhiteSpace(selectedTheme) && selectedTheme != Properties.Settings.Default.AppTheme)
            {
                Properties.Settings.Default.AppTheme = selectedTheme;
                ApplyTheme(selectedTheme);
                return true;
            }

            else
                return false;
        }

    }
}
