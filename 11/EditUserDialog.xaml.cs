using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
using System.Windows.Shapes;
using static _11.MainWindow;

namespace _11
{
    /// <summary>
    /// Логика взаимодействия для EditUserDialog.xaml
    /// </summary>
    public partial class EditUserDialog : Window
    {
        public const string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";

        public EditUserDialog(int id) // Конструктор класса EditUserDialog
        {
            GetDataFromDatabase(id);
            InitializeComponent();
        }
        public void GetDataFromDatabase(int id)
        {

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
                    string logSQL = reader["Код"].ToString();
                    //string pasSQL = reader["ХэшПароль"].ToString();

                    if (logSQL == id.ToString())
                    {
                        flag = true;

                        txtDoctorCode.Text = reader["КодВрача"].ToString();

                        MessageBox.Show("Этот пароль просто шик");

                        MenuFrameClass.MainFrame.Navigate(new USER_Page());

                        // LogFrame.NavigationService.Navigate(new USER_Page());

                        break;
                    }

                }

                if (!flag)
                {
                    MessageBox.Show("Чушпан!\nТы что ввел то?\nСам видел то?");
                }

            }

        }
            private void SaveButton_Click(object sender, RoutedEventArgs e) {

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "UPDATE Прием SET КодВрача = @КодВрача, КодПациента = @КодПациента, ДатаВизита = @ДатаВизита, КодСтоимости = @КодСтоимости, Цель = @Цель, КодДиагноза = @КодДиагноза WHERE Код = @КодПриема";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@КодВрача", txtDoctorCode.Text);
                        command.Parameters.AddWithValue("@КодПациента", txtPatientCode.Text);
                        command.Parameters.AddWithValue("@ДатаВизита", txtVisitDate.Text);
                        command.Parameters.AddWithValue("@КодСтоимости", txtCostCode.Text);
                        command.Parameters.AddWithValue("@Цель", txtPurpose.Text);
                        command.Parameters.AddWithValue("@КодДиагноза", txtDiagnoz.Text);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Данные успешно обновлены в базе данных!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
                        }
                    }
                }
                this.Close(); // Закрыть диалоговое окно
            }
        }

    }

