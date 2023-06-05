using AudioCornerDB;
using Npgsql;
using System;
using System.Collections.Generic;
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

namespace AudioCorner.Songs
{
    public partial class AddSongs : Window
    {
        DataBase DB = new DataBase();
        public string query;

        private void AddSong()
        {
            try
            {
                DB.OpenConnection();
                query = $@"insert into public.""Song"" values ('{Convert.ToInt32(AddSongID.Text)}', '{Convert.ToInt32(AddAlbumID.Text)}', '{Convert.ToInt32(AddBandID.Text)}', '{AddSongName.Text}', '{AddGenre.Text}')";
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
            LoadSongData();
        }
        private void LoadSongData()
        {
            try
            {
                DB.OpenConnection();

                query = @"select * from public.""Song"" order by ""Album_ID""";

                NpgsqlCommand cmd = new NpgsqlCommand(query, DB.GetConnection());
                DB.CloseConnection();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        public AddSongs()
        {
            InitializeComponent();
        }

        private void ExitAddSong_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DoneAddSong_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Нужно добавить что-то еще?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            AddSong();
            if (answer == MessageBoxResult.Yes)
            {
                LoadSongData();
                return;
            }
            Close();
        }
    }
}
