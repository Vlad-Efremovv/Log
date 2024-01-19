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
    /// Логика взаимодействия для EditUserDialog.xaml
    /// </summary>
    public partial class EditUserDialog : Window
    {
        private Прием прием;

        public EditUserDialog(Прием user)
        {
            InitializeComponent();
            this.прием = прием;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение значений из текстовых полей
            string code = txtCode.Text;
            string doctorCode = txtDoctorCode.Text;
            string patientCode = txtPatientCode.Text;
            string visitDate = txtVisitDate.Text;
            string costCode = txtCostCode.Text;
            string purpose = txtPurpose.Text;

            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";

            SqlConnection connection = new SqlConnection(connectionString);
            {
                string query = "INSERT INTO Прием ([Код], [КодВрача], [КодПациента], [ДатаВизита], [КодСтоимости], [Цель]) " +
                               "VALUES (@Код, @КодВрача, @КодПациента, @ДатаВизита, @КодСтоимости, @Цель)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Код", code);
                    command.Parameters.AddWithValue("@КодВрача", doctorCode);
                    command.Parameters.AddWithValue("@КодПациента", patientCode);
                    command.Parameters.AddWithValue("@ДатаВизита", visitDate);
                    command.Parameters.AddWithValue("@КодСтоимости", costCode);
                    command.Parameters.AddWithValue("@Цель", purpose);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно сохранены в базе данных!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
                    }
                }
            }

        }

    }
}
