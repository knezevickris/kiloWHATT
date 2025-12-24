using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using efficiencyCalculator.Model;
using efficiencyCalculator.Helpers;
using System.Windows;

namespace efficiencyCalculator.Helpers
{
    public static class DevicesOperations
    {
        public static List<Device> loadDevices()
        {
            var devices = new List<Device>();
            string query = "SELECT * FROM device ORDER BY Date DESC";

            try
            {
                using (var connection = DatabaseHelper.getConnection())
                {
                    if (connection == null)
                        return devices;

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var device = new Device
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Consumption = Convert.ToDouble(reader["Consumption"]),
                                    Expense = Convert.ToDouble(reader["Expense"]),
                                    Class = reader["Class"].ToString(),
                                    Date = Convert.ToDateTime(reader["Date"])
                                };

                                devices.Add(device);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Greška pri ucitavanju uredjaja"+ex.ToString(), "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return devices;

        }

        public static void insertDevice(Device device)
        {
            try
            {
                using (var connection = DatabaseHelper.getConnection())
                {
                    string query = "INSERT INTO device (Name, Power, Consumption, Expense, Class, Date) VALUES (@name, @power, @consumption, @expense, @class, @date)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", device.Name);
                        command.Parameters.AddWithValue("@power", device.Power);
                        command.Parameters.AddWithValue("@consumption", device.Consumption);
                        command.Parameters.AddWithValue("@expense", device.Expense);
                        command.Parameters.AddWithValue("@class", device.Class);
                        command.Parameters.AddWithValue("@date", device.Date);

                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Greska pri unosu uređaja u bazu podataka"+ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }

}
