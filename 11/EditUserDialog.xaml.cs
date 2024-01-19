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
        private Прием прием;

        public EditUserDialog(Прием user)
        {
            InitializeComponent();
            this.прием = прием;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           
            // Получение значений из текстовых полей
            string code = txtCode.Text = прием.Код.ToString();
            string doctorCode = txtDoctorCode.Text = прием.КодВрача.ToString();
            string patientCode = txtPatientCode.Text = прием.КодПациента.ToString();
            string visitDate = txtVisitDate.Text = прием.ДатаВизита.ToString();
            string costCode = txtCostCode.Text = прием.Цель.ToString();
            string purpose = txtPurpose.Text = прием.КодСтоимости.ToString();

            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";

            SqlConnection connection = new SqlConnection(connectionString);
            {
                string query = "INSERT INTO Прием ([Код], [КодВрача], [КодПациента], [ДатаВизита], [КодСтоимости], [Цель], [@КодДиагноза]) " +
                               "VALUES (@Код, @КодВрача, @КодПациента, @ДатаВизита, @КодСтоимости, @Цель, @КодДиагноза)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Код", code);
                    command.Parameters.AddWithValue("@КодВрача", doctorCode);
                    command.Parameters.AddWithValue("@КодПациента", patientCode);
                    DateTime visitDateTime;
                    if (DateTime.TryParseExact(visitDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out visitDateTime))
                    {
                        command.Parameters.AddWithValue("@VisitDate", visitDateTime);
                    }
                    command.Parameters.AddWithValue("@КодСтоимости", costCode);
                    command.Parameters.AddWithValue("@Цель", purpose);
                    //command.Parameters.AddWithValue("@КодДиагноза", diagnog);


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
