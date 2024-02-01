using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        class Dev
        {
            public string DoctorCode { get; set; }
            public string PatientCode { get; set; }
            public string VisitDate { get; set; }
            public string CostCode { get; set; }
            public string Purpose { get; set; }
            public string Diagnoz { get; set; }
        }

        private Dev dev;

        public Edit(string inputDoctorCode, string inputPatientCode, string inputVisitDate, string inputCostCode, string inputPurpose, string inputDiagnoz)
        {
            InitializeComponent();

            dev = new Dev
            {
                DoctorCode = inputDoctorCode,
                PatientCode = inputPatientCode,
                VisitDate = inputVisitDate,
                CostCode = inputCostCode,
                Purpose = inputPurpose,
                Diagnoz = inputDiagnoz
            };

            DataContext = dev;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика добавления нового пользователя по значениям из текстовых полей
            string doctorCode = dev.DoctorCode;
            string patientCode = dev.PatientCode;
            string visitDate = dev.VisitDate;
            string costCode = dev.CostCode;
            string purpose = dev.Purpose;
            string diagnoz = dev.Diagnoz;

            bool userAdded = AddNewUser(doctorCode, patientCode, visitDate, costCode, purpose, diagnoz);

            if (userAdded)
            {
                MessageBox.Show("Пользователь успешно отредактирован");
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
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

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:\n" + ex.Message);
                return false;
            }
        }
    }
}

/*
 * using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        class dev
        {
            private string doctorCode {  get; set; }
            private string patientCode;
            private string visitDate;
            private string costCode;
            private string purpose;
            private string diagnoz;          
        }

        public Edit(string inputdoctorCode, string inputpatientCode, string inputvisitDatestring, string inputcostCode, string inputpurpose, string inputdiagnoz)
        {
            var dev = new dev();
            try
            {
                dev.doctorCode = inputdoctorCode;
                dev.patientCode = inputpatientCode;
                dev.visitDate = inputvisitDatestring;
                dev.costCode = inputcostCode;
                dev.purpose = inputpurpose;
                dev.diagnoz = inputdiagnoz;
            }
            catch { MessageBox.Show("Не получилось вставить прошлые данные\nВспоминай хахахха"); }

            InitializeComponent();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика добавления нового пользователя по значениям из текстовых полей
            string doctorCode = DoctorCode.Text;
            string patientCode = PatientCode.Text;
            string visitDate = VisitDate.Text;
            string costCode = CostCode.Text;
            string purpose = Purpose.Text;
            string diagnoz = Diagnoz.Text;

            bool userAdded = AddNewUser(doctorCode, patientCode, visitDate, costCode, purpose, diagnoz);

            if (userAdded)
            {
                MessageBox.Show("Пользователь успешно отредактирован");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DoctorCode.Text = inputdoctorCode;
                PatientCode.Text = inputpatientCode;
                VisitDate.Text = inputvisitDatestring;
                CostCode.Text = inputcostCode;
                Purpose.Text = inputpurpose;
                Diagnoz.Text = inputdiagnoz;
            }
            catch { MessageBox.Show("Не получилось вставить прошлые данные\nВспоминай хахахха"); }

        }
    }
}*/
