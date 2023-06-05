using AudioCorner.Albums;
using AudioCornerDB;
using Npgsql;
using System;
using System.Windows;

namespace AudioCorner.Songs
{
    public partial class DelSongs : Window
    {
        DataBase DB = new DataBase();
        public string query;
        // Удаление
        private void DeleteSong()
        {
            try
            {
                DB.OpenConnection();
                query = $@"delete from public.""Song"" where ""Song_ID"" = '{Convert.ToInt32(DelSongID.Text)}'";
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
        private void LoadSongData()
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
        public DelSongs()
        {
            InitializeComponent();
        }

        private void ExitDelSongBut_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DoneDelSong_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Нужно удалить что-то еще?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            DeleteSong();
            if (answer == MessageBoxResult.Yes)
            {
                LoadSongData();
                return;
            }
            Close();
        }
    }
}
