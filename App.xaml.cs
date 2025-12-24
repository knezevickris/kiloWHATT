using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using efficiencyCalculator.Properties;

namespace efficiencyCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string theme = Settings.Default.AppTheme;

            if (string.IsNullOrEmpty(theme))
                theme = "Svijetli režim";

            var toRemove = Application.Current.Resources.MergedDictionaries.Where(d => d.Source != null &&
                  d.Source.OriginalString.Contains("LightTheme.xaml") ||
                  d.Source.OriginalString.Contains("DarkTheme.xaml")).ToList();

            foreach (var dic in toRemove)
                Application.Current.Resources.MergedDictionaries.Remove(dic);

            if (theme == "Svijetli režim")
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/Resources/LightTheme.xaml", UriKind.Relative) });
            else if (theme == "Tamni režim")
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/Resources/DarkTheme.xaml", UriKind.Relative) });
        }
        
    }

    
}
