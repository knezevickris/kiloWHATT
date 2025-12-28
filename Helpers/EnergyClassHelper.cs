using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using efficiencyCalculator.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace efficiencyCalculator.Helpers
{
    public class EnergyClassHelper
    {
        /* deklarisemo mapu mapa, spoljnja mapa kljuc: tip uredjaja, vrijednost: unutrasnja mapa kojoj su kljucevi A, B ... G,
                a vrijednosti su granice tih klasa
            znaci imamo poznat tip uredjaja koji je prvi kljuc i njegovu mjesecnu potrosnju koju uporedjujemo sa 
            granicnim vrijednostima, nepoznata nam je oznaka klase
        */
        public Dictionary<string, Dictionary<string, double>> Thresholds { get; set; }

        public static EnergyClassHelper loadFromFile(string filePath)
        {
            var json = File.ReadAllText(filePath); //iscita JSON fajl i smjesti ga u varijablu
            var thresholds = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, double>>>(json); 
            //postupno izdvaja podatke iz ogromnog stringa u mapu mapa koju smo napravili
            //prvi string ce biti npr. frizider i sa njim u vezi mapa ciji su kljucevi "A", "B" itd. a vrijednosti 100.00, 150.00 itd.
            return new EnergyClassHelper { Thresholds = thresholds };
        }

        public string getDeviceClass(Device device)
        {
            if (!Thresholds.ContainsKey(device.DeviceType.ToString()))
            {
                throw new ArgumentException("Nepoznat tip uredjaja.");
            }
            
            var classes = Thresholds[device.DeviceType.ToString()].OrderBy(c=>c.Value);
            foreach ( var c in classes)
            {
                if (device.Consumption <= c.Value)
                    return c.Key;
            }

            return "G";
        }

        public static void setBackground(Border border, string energyClass)
        {
            var json = File.ReadAllText("Config/colorsOfClasses.json");
            var colors = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            if (!colors.ContainsKey(energyClass))
            {
                throw new ArgumentException("Nepoznata klasa uredjaja");
            }

            border.Background = (SolidColorBrush)(new BrushConverter().ConvertFromString(colors[energyClass]));
            border.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));   
        }

    }
}
