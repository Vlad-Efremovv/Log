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
            string code = txtCode.Text;
            string doctorCode = txtDoctorCode.Text;
            string patientCode = txtPatientCode.Text;
            string visitDate = txtVisitDate.Text;
            string costCode = txtCostCode.Text;
            string purpose = txtPurpose.Text;
            string diagnoz = txtDiagnoz.Text;
            // Добавление нового пользователя в систему или другая логика обработки
            // Пример: вызов метода для добавления пользователя
            bool userAdded = AddNewUser(code, doctorCode, patientCode, visitDate, costCode, purpose, diagnoz);

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

        private bool AddNewUser(string code, string doctorCode, string patientCode, string visitDate, string costCode, string purpose, string diagnoz)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";
            try
            {
                // Здесь идет логика выполнения запроса INSERT в базу данных или другие действия по добавлению пользователя
                // Пример: выполнение запроса INSERT в базу данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO [dbo].[Прием] ([Код], [КодВрача], [КодПациента], [ДатаВизита], [КодСтоимости], [Цель], [КодДиагноза])" +
                     "VALUES (@Code, @DoctorCode, @PatientCode, @VisitDate, @CostCode, @Purpose, @Diagnoz)";

                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@Code", Convert.ToInt32(code));
                    command.Parameters.AddWithValue("@DoctorCode", Convert.ToInt32(doctorCode));
                    command.Parameters.AddWithValue("@PatientCode", Convert.ToInt32(patientCode));
                    command.Parameters.AddWithValue("@VisitDate", visitDate);
                    command.Parameters.AddWithValue("@CostCode", Convert.ToInt32(costCode));
                    command.Parameters.AddWithValue("@Purpose", purpose);
                    command.Parameters.AddWithValue("@Diagnoz", Convert.ToInt32(diagnoz));
                    command.ExecuteNonQuery();
                }

                // Если операция выполнена успешно, возвращаем true
                return true;
            }
            catch (Exception ex)
            {
                // Обработка ошибок (логирование, откат транзакции и т. д.)
                MessageBox.Show("Ошибка:\n"+ex.Message);
                return false;
            }
        }
    }
}
