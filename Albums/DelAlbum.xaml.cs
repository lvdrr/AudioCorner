using AudioCornerDB;
using Npgsql;
using System;
using System.Windows;

namespace AudioCorner.Albums
{
    public partial class DelAlbum : Window
    {
        DataBase DB = new DataBase();
        public string query;
        // Удаление
        private void DeleteAlbum()
        {
            try
            {
                DB.OpenConnection();
                query = $@"delete from public.""Album"" where ""Album_ID"" = '{Convert.ToInt32(DelAlbumID.Text)}'";
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
        private void LoadAlbumData()
        {
            try
            {
                DB.OpenConnection();

                query = @"select * from public.""Album""";

                NpgsqlCommand cmd = new NpgsqlCommand(query, DB.GetConnection());
                DB.CloseConnection();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public DelAlbum()
        {
            InitializeComponent();
        }

        private void DoneDelAlbum_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Нужно удалить что-то еще?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            DeleteAlbum();
            if (answer == MessageBoxResult.Yes)
            {
                LoadAlbumData();
                return;
            }
            Close();
        }

        private void ExitDelAlbumBut_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
