using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using MySql.Data.MySqlClient;

namespace efficiencyCalculator.Helpers
{
    public class DatabaseHelper
    {
        private static string connectionString = "Server=localhost; Database=electricdevices; User=root; Password=";
 
        public static MySqlConnection getConnection()
        {
            try
            {
                var connection = new MySqlConnection(connectionString);
                connection.Open();

                return connection;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Greška u uspostavljanju konekcije sa bazom podataka." + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

    }
}

