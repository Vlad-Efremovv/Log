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
        class dev
        {
            private string doctorCode;
            private string patientCode;
            private string visitDate;
            private string costCode;
            private string purpose;
            private string diagnoz;

            public string DoctorCode
            {
                get { return doctorCode; }
                set { doctorCode = value; OnPropertyChanged(); }
            }

            public string PatientCode
            {
                get { return patientCode; }
                set { patientCode = value; OnPropertyChanged(); }
            }

            public string VisitDate
            {
                get { return visitDate; }
                set { visitDate = value; OnPropertyChanged(); }
            }

            public string CostCode
            {
                get { return costCode; }
                set { costCode = value; OnPropertyChanged(); }
            }

            public string Purpose
            {
                get { return purpose; }
                set { purpose = value; OnPropertyChanged(); }
            }

            public string Diagnoz
            {
                get { return diagnoz; }
                set { diagnoz = value; OnPropertyChanged(); }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Edit(string inputdoctorCode, string inputpatientCode, string inputvisitDatestring, string inputcostCode, string inputpurpose, string inputdiagnoz)
        {
            var dev = new dev();
            try {
                dev.DoctorCode = inputdoctorCode;
                dev.PatientCode = inputpatientCode;
                dev.VisitDate = inputvisitDatestring;
                dev.CostCode = inputcostCode;
                dev.Purpose = inputpurpose;
                dev.Diagnoz = inputdiagnoz;
            } catch { MessageBox.Show("Не получилось вставить прошлые данные\nВспоминай хахахха"); }
            DataContext = new dev();

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
    }
}
