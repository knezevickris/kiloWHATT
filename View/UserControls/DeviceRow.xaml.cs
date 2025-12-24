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

namespace efficiencyCalculator.View.UserComponents
{
    /// <summary>
    /// Interaction logic for DeviceRow.xaml
    /// </summary>
    public partial class DeviceRow : UserControl
    {
     
        public DeviceRow(Device device)
        {
            InitializeComponent();
            DataContext = device;
        }
        

        public DeviceRow()
        {
            InitializeComponent();
        }
    }
}
