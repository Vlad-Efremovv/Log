using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _11
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Log());

            //MenuFrameClass.MainFrame = MainFrame;                                           // присвоение фрейму параментров get set    

            try
            {
                string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Больница;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "База данных");
            }
        }
    }
}
