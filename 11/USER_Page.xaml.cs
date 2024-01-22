using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
   public class Прием
    {
        public int Код { get; set; }
        public string КодВрача { get; set; }
        public string КодПациента { get; set; }
        public string ДатаВизита { get; set; }
        public string КодСтоимости { get; set; }
        public string Цель { get; set; }
        public string КодДиагноза { get; set; }
        
    }

    public partial class USER_Page : Page
    {
        private const string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";

        public USER_Page()
        {
            InitializeComponent();
            BindDataToGrid();
        }

        private void BindDataToGrid()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT \r\n    Прием.Код AS КодПриема,\r\n    Врачь.ФИО AS ФИОВрача,\r\n    Пациент.ФИО AS ФИОПациента,\r\n    Прием.ДатаВизита,\r\n    Стоимость.Сумма AS СтоимостьВизита,\r\n    Диагноз.Наименование AS НаименованиеДиагноза\r\nFROM \r\n    Прием\r\nJOIN \r\n    Врачь ON Прием.КодВрача = Врачь.Код\r\nJOIN \r\n    Пациент ON Прием.КодПациента = Пациент.НомерКарты\r\nJOIN \r\n    Стоимость ON Прием.КодСтоимости = Стоимость.Код\r\nLEFT JOIN \r\n    Диагноз ON Прием.КодДиагноза = Диагноз.Код;"; // Замените YourTableName на название вашей таблицы

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    dataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddUserDialog dialog = new AddUserDialog(); // Предположим, что у вас есть класс для диалогового окна добавления пользователя
            if (dialog.ShowDialog() == true) // Показываем диалог и ждем результата
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                }
                BindDataToGrid();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            BindDataToGrid();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGrid.SelectedItem;
                int КодПриема = Convert.ToInt32(row["КодПриема"]);

                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Прием WHERE Код = @КодПриема";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@КодПриема", КодПриема);
                        command.ExecuteNonQuery();
                    }
                    BindDataToGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении записи: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGrid.SelectedItem;
                int КодПриема = (int)row["КодПриема"]; // Предположим, что тип "КодПриема" - int

                EditUserDialog dialog = new EditUserDialog(КодПриема);
                if (dialog.ShowDialog() == true)
                {
                    BindDataToGrid(); // Обновляем отображаемые данные после редактирования
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для редактирования.");
            }
        }

    }
}
