using Npgsql;
using System;
using System.Data;
using System.Windows;
using AudioCornerDB;

namespace AudioCorner.Albums
{
    
    public partial class AddAlbum : Window
    {
        DataBase DB = new DataBase();
        public string query;

        private void AddAlbums()
        {
            try
            {
                DB.OpenConnection();
                query = $@"insert into public.""Album"" values ('{Convert.ToInt32(AddAlbumID.Text)}', '{Convert.ToInt32(AddBandID.Text)}', '{AddAlbumName.Text}', '{Convert.ToInt32(AddNumOfAlbumSongs.Text)}', '{AddGenre.Text}', '{Convert.ToInt32(AddReleaseDate.Text)}')";
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
            LoadAlbumData();
        }
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
                MessageBox.Show(exc.Message);
            }
        }
        public AddAlbum()
        {
            InitializeComponent();
        }

        private void DoneAddAlbum_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Нужно добавить что-то еще?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            AddAlbums();
            if (answer == MessageBoxResult.Yes)
            {
                LoadAlbumData();
                return;
            }
            Close();
        }

        private void ExitAddAlbum_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
