using System;
using System.Collections.Generic;
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
        //private int КодПриема; // Предположим, что это поле для хранения идентификатора записи Приема
        private const string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";
        public EditUserDialog(int кодПриема) // Конструктор класса EditUserDialog
        {
            InitializeComponent(); // Вызов метода для инициализации пользовательского интерфейса
           // КодПриема = кодПриема; // Инициализация поля кода приёма

            var data = GetDataFromDatabase(кодПриема); // Получение данных из базы данных

                    }

        private Прием GetDataFromDatabase(int кодПриема)
        {
            Прием data = new Прием();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Прием WHERE Код = @Код";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Код", кодПриема);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    data.Code = reader["Код"].ToString();
                    data.DoctorCode = reader["КодВрача"].ToString();
                    data.PatientCode = reader["КодПациента"].ToString();
                    data.VisitDate = reader["ДатаВизита"].ToString();
                    data.CostCode = reader["КодСтоимости"].ToString();
                    data.Purpose = reader["Цель"].ToString();
                    data.DiagnosisCode = reader["КодДиагноза"].ToString();
                }

                reader.Close();
            }
            return data;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Прием SET КодВрача = @КодВрача, КодПациента = @КодПациента, ДатаВизита = @ДатаВизита, КодСтоимости = @КодСтоимости, Цель = @Цель, КодДиагноза = @КодДиагноза WHERE Код = @Код";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Код", КодПриема);
                    // Используем КодПриема, который был инициализирован в конструкторе класса
                    command.Parameters.AddWithValue("@КодВрача", txtCode.Text); // Замените на реальное значение поля
                    command.Parameters.AddWithValue("@КодПациента", txtDoctorCode.Text); // Замените на реальное значение поля
                    command.Parameters.AddWithValue("@ДатаВизита", txtPatientCode.Text); // Замените на реальное значение поля
                    command.Parameters.AddWithValue("@КодСтоимости", txtVisitDate.Text); // Замените на реальное значение поля
                    command.Parameters.AddWithValue("@Цель", txtCostCode.Text); // Замените на реальное значение поля
                    command.Parameters.AddWithValue("@КодДиагноза", txtPurpose.Text); // Замените на реальное значение поля

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
