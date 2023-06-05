using Npgsql;
using System;
using System.Data;
using System.Windows;
using AudioCornerDB;

namespace AudioCorner
{
    public partial class AddBand : Window
    {
        DataBase DB = new DataBase();
        public string query;

        private void AddBands()
        {
            try
            {
                DB.OpenConnection();
                query = $@"insert into public.""Band"" values ('{Convert.ToInt32(AddBandID.Text)}', '{AddBandName.Text}', '{Convert.ToInt32(AddBandFormedIn.Text)}')";
                NpgsqlCommand cmd = new NpgsqlCommand(query, DB.GetConnection());
                cmd.ExecuteNonQuery();
                DB.CloseConnection();
                MessageBox.Show("Успешно!");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            LoadDataFromDB();
        }
        private void LoadDataFromDB()
        {
            try
            {
                DB.OpenConnection();

                query = @"select * from public.""Band""";

                NpgsqlCommand cmd = new NpgsqlCommand(query, DB.GetConnection());
                DB.CloseConnection();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        public AddBand()
        {
            InitializeComponent();
        }

        public void DoneAddBut_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Нужно добавить что-то еще?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            AddBands();
            if (answer == MessageBoxResult.Yes)
            {
                LoadDataFromDB();
                return;
            }
            Close();
        }
        private void ExitAddBandBut_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
