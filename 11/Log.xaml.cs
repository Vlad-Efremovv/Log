using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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

namespace _11
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
    }

    public partial class Log : Page
    {
        public Log()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Password;

                string hashedPassword = PasswordHashing.CalculateMD5Hash(password);
                MessageBox.Show("Хэшированный пароль: " + hashedPassword);

                string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";
                string queryString = "SELECT * FROM Авторизация";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    bool flag = false;

                    while (reader.Read())
                    {
                        string logSQL = reader["Логин"].ToString();
                        string pasSQL = reader["ХэшПароль"].ToString();
                        string accessSQL = reader["ПраваДоступа"].ToString();

                        if (logSQL == username && hashedPassword == pasSQL)
                        {
                            flag = true;

                            MessageBox.Show("Этот пароль просто ахуенен");

                            if (accessSQL == "1")
                            {
                                //NavigationService?.RemoveBackEntry();
                               // LogFrame.Navigate(new ADMPage());
                                LogFrame.Content = new ADMPage();
                            }
                            else
                            {
                                LogFrame.NavigationService.RemoveBackEntry();
                                LogFrame.Navigate(new USER_Page());
                            }

                            break;
                        }

                    }

                    if (!flag)
                    {
                        MessageBox.Show("Чушпан!\nТы что ввел то?\nСам видел то?");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);

            }
        }
    }

    public class PasswordHashing
    {
        public static string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
