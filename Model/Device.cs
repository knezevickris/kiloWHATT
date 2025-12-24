using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efficiencyCalculator.Model
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Power { get; set; }
        public double Consumption { get; set; }
        public double Expense { get; set; }
        public string Class { get; set; }
        public DateTime Date { get; set; }

        public double DailyConsumption { get; set; }
        public double DailyExpense { get; set; }
    }
}
