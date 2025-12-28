using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using efficiencyCalculator.Model;
using efficiencyCalculator.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace efficiencyCalculator.Helpers
{
    public static class DevicesOperations
    {
        public static void loadTypes(ComboBox comboBox)
        {
            try
            {
                using var connection = DatabaseHelper.getConnection();
                string query = "SELECT * FROM devicetype ORDER BY Name ASC";
                using var command = new MySqlCommand(query, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var typeOfDevice = new DeviceType
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader["Name"].ToString()
                    };
                    comboBox.Items.Add(typeOfDevice);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri ucitavanju tipova uredjaja" + ex.Message, "Greska", MessageBoxButton.OK);
            }
        }
        public static List<Device> loadDevices()
        {
            var devices = new List<Device>();
            string query = "SELECT * FROM device ORDER BY Date DESC";

            try
            {
                using var connection = DatabaseHelper.getConnection();
                using var command = new MySqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var device = new Device
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Consumption = Convert.ToDouble(reader["Consumption"]),
                        Expense = Convert.ToDouble(reader["Expense"]),
                        Class = reader["Class"].ToString(),
                        IdDeviceType = Convert.ToInt32(reader["TypeId"]),
                        Date = Convert.ToDateTime(reader["Date"])
                    };

                    devices.Add(device);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri ucitavanju uredjaja" + ex.ToString(), "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return devices;
        }

        public static void insertDevice(Device device)
        {
            try
            {
                using var connection = DatabaseHelper.getConnection();
                string query = "INSERT INTO device (Name, Power, Consumption, Expense, Class, TypeId, Date) VALUES (@name, @power, @consumption, @expense, @class, @typeId, @date)";
                using var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", device.Name);
                command.Parameters.AddWithValue("@power", device.Power);
                command.Parameters.AddWithValue("@consumption", device.Consumption);
                command.Parameters.AddWithValue("@expense", device.Expense);
                command.Parameters.AddWithValue("@class", device.Class);
                command.Parameters.AddWithValue("@typeId", device.IdDeviceType);
                command.Parameters.AddWithValue("@date", device.Date);

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Greska pri unosu uređaja u bazu podataka" + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


    }

}
