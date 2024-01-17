using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для Log.xaml
    /// </summary>
    public partial class Log : Page
    {
        public Log()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            string hashedPassword = PasswordHashing.CalculateMD5Hash(password);
            //MessageBox.Show("Хэшированный пароль: " + hashedPassword);

            /*if (YourAuthenticationService.ValidateUser(username, password))
             {
                 // Открываем новое окно или выполняем другие действия при успешной авторизации
                 var mainAppWindow = new MainApplicationWindow();
                 mainAppWindow.Show();
                 this.Close();
             }
             else
             {
                 MessageBox.Show("Неверный логин или пароль");
             }
            */
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
