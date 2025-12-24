using efficiencyCalculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efficiencyCalculator.Helpers
{
    public static class Calculator
    {
        public static Device calculate(string name, double power, double hoursPerDay)
        {
            return new Device
            {
                Name = name,
                Power = convertToKW(power),
                Consumption = convertToKW(power) * hoursPerDay * 30,
                Expense = convertToKW(power) * hoursPerDay * Properties.Settings.Default.Price * 30,
                //implementirati odredjivanje klase uredjaja
                Class = "B",
                Date = DateTime.Today,

                DailyConsumption = convertToKW(power) * hoursPerDay,
                DailyExpense = convertToKW(power) * hoursPerDay * Properties.Settings.Default.Price
            };

        }

        private static double convertToKW(double w)
        {
            return w / 1000;
        }
    }
}
