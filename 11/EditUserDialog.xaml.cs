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

namespace _11
{
    /// <summary>
    /// Логика взаимодействия для EditUserDialog.xaml
    /// </summary>
    public partial class EditUserDialog : Window
    {

        private const string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";
        public EditUserDialog(int кодПриема) // Конструктор класса EditUserDialog
        {
            InitializeComponent();
        }
        public DataTable GetDataFromDatabase(int кодПриема)
        {

            string query = "SELECT * FROM Прием WHERE Код = @КодПриема";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@КодПриема", кодПриема);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
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
