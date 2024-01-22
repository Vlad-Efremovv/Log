using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для AddUserDialog.xaml
    /// </summary>
    public partial class AddUserDialog : Window
    {
        public AddUserDialog()
        {
            InitializeComponent();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика добавления нового пользователя по значениям из текстовых полей
            string doctorCode = txtDoctorCode.Text;
            string patientCode = txtPatientCode.Text;
            string visitDate = txtVisitDate.Text;
            string costCode = txtCostCode.Text;
            string purpose = txtPurpose.Text;
            string diagnoz = txtDiagnoz.Text;

            bool userAdded = AddNewUser(doctorCode, patientCode, visitDate, costCode, purpose, diagnoz);

            if (userAdded)
            {
                MessageBox.Show("Пользователь успешно добавлен");
                this.Close(); // Закрываем диалоговое окно после успешного добавления
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении пользователя");
            }
        }

        private bool AddNewUser(string doctorCode, string patientCode, string visitDate, string costCode, string purpose, string diagnoz)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";
            try
            {
                // Здесь идет логика выполнения запроса INSERT в базу данных или другие действия по добавлению пользователя
                // Пример: выполнение запроса INSERT в базу данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Соеденение установденно");
                    string insertQuery = "INSERT INTO [dbo].[Прием] ([КодВрача], [КодПациента], [ДатаВизита], [КодСтоимости], [Цель], [КодДиагноза])" +
                     "VALUES (@doctorCode, @patientCode, @visitDate, @costCode, @purpose, @diagnoz)";
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@doctorCode", doctorCode);
                    command.Parameters.AddWithValue("@patientCode", patientCode);
                    command.Parameters.AddWithValue("@visitDate", visitDate);
                    command.Parameters.AddWithValue("@costCode", costCode);
                    command.Parameters.AddWithValue("@purpose", purpose);
                    command.Parameters.AddWithValue("@diagnoz", diagnoz);
                    command.ExecuteNonQuery();

                }

                // Если операция выполнена успешно, возвращаем true
                return true;
            }
            catch (Exception ex)
            {
                // Обработка ошибок (логирование, откат транзакции и т. д.)
                MessageBox.Show("Ошибка:\n" + ex.Message);
                return false;
            }
        }
    }
}
