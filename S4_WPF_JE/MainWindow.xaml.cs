using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;

namespace S4_WPF_JE
{
    public partial class MainWindow : Window
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-IPA0A59\\SQLEXPRESS; " +
                                       "Initial Catalog=db_lab04; Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            List<Person> people = new List<Person>();

            SqlCommand command = new SqlCommand("BuscarPersonaNombre", conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.SqlDbType = SqlDbType.VarChar;
            parameter1.Size = 50;
            //parameter1.Value = txtNombre.Text.Trim();
            parameter1.Value = "";
            parameter1.ParameterName = "@FirstName";

            command.Parameters.Add(parameter1);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                people.Add(new Person 
                {
                    PersonID = dataReader[0].ToString(),
                    FirstName = dataReader[1].ToString(),
                    LastName = dataReader[2].ToString(),
                    HireDate = dataReader[3].ToString(),
                    EnrollmentDate = dataReader[4].ToString(),
                });

            }

            conn.Close();
            dgvPeople.ItemsSource = people;
        }
    }
}
