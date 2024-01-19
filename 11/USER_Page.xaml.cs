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
                // Получаем новые данные от диалогового окна
                //string newName = dialog.UserName;
                //string newEmail = dialog.UserEmail;

                // Выполняем запрос INSERT в базу данных
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    //string insertQuery = "INSERT INTO YourTableName (Name, Email) VALUES (@Name, @Email)";
                    //SqlCommand command = new SqlCommand(insertQuery, connection);
                    //command.Parameters.AddWithValue("@Name", newName);
                    //command.Parameters.AddWithValue("@Email", newEmail);
                    //command.ExecuteNonQuery();
                }

                // Обновляем содержимое DataGrid
                BindDataToGrid();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = (Прием)dataGrid.SelectedItem; // Предположим, что вы используете тип User для обозначения записи в DataGrid

            EditUserDialog dialog = new EditUserDialog(selectedUser); // Предположим, что у вас есть класс для диалогового окна редактирования пользователя
            if (dialog.ShowDialog() == true) // Показываем диалог и ждем результата
            {
                // Получение отредактированных данных от пользователя
                //string editedName = dialog.UserName;
                //string editedEmail = dialog.UserEmail;

                // Выполнение запроса UPDATE в базе данных
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    //string updateQuery = "UPDATE YourTableName SET Name = @Name, Email = @Email WHERE Id = @Id"; // Предположим, что у вашей таблицы есть поле Id для идентификации записи
                    //SqlCommand command = new SqlCommand(updateQuery, connection);
                    //command.Parameters.AddWithValue("@Name", editedName);
                    //command.Parameters.AddWithValue("@Email", editedEmail);
                    //command.Parameters.AddWithValue("@Id", selectedUser.Id);
                    //command.ExecuteNonQuery();
                }

                // Обновление содержимого DataGrid
                BindDataToGrid();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecord = (Прием)dataGrid.SelectedItem; // Получаем выбранную запись из DataGrid

            if (selectedRecord != null)
            {
                // Выполняем параметризованный запрос DELETE к базе данных
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        string deleteQuery = "DELETE FROM Прием WHERE Код = @Код";
                        SqlCommand command = new SqlCommand(deleteQuery, connection);
                        command.Parameters.AddWithValue("@Код", selectedRecord.Код);
                        command.ExecuteNonQuery();
                    }

                    // Обновляем содержимое DataGrid
                    BindDataToGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении записи: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
        } 
    }
}
