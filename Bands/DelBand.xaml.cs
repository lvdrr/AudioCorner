using AudioCornerDB;
using Npgsql;
using System;
using System.Windows;

namespace AudioCorner
{
    public partial class DelBand : Window
    {
        DataBase DB = new DataBase();
        public string query;
        // Удаление
        private void DeleteBand() 
        {
            try
            {
                DB.OpenConnection();
                query = $@"delete from public.""Band"" where ""Band_ID"" = '{Convert.ToInt32(DelBandID.Text)}'";
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
        }
        // Выгрузка данных
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
                MessageBox.Show(exc.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public DelBand()
        {
            InitializeComponent();
        }

        private void DoneDelBand_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Нужно удалить что-то еще?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            DeleteBand();
            if (answer == MessageBoxResult.Yes)
            {
                LoadDataFromDB();
                return;
            }
            Close();
        }

        private void ExitDelBandBut_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
