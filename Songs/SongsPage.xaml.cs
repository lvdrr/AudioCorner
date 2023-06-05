using Npgsql;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using AudioCornerDB;
using AudioCorner.Albums;
using AudioCorner.Songs;

namespace AudioCorner
{
    public partial class SongsPage : Window
    {
        DataBase DB = new DataBase();
        public string query;
        private void LoadSongData()
        {
            try
            {
                DB.OpenConnection();

                query = @"select ""Song_ID"" as ""ID песни"", ""Album_ID"" as ""ID альбома"", ""Band_ID"" as ""ID Группы"", ""Song_Name"" as ""Название_песни"", ""Genre"" as ""Жанр"" from public.""Song"" order by ""Band_ID"" ";

                NpgsqlCommand cmd = new NpgsqlCommand(query, DB.GetConnection());

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable DT = new DataTable();
                    DT.Load(reader);
                    SongsDG.ItemsSource = DT.DefaultView;
                }
                DB.CloseConnection();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        public SongsPage()
        {
            InitializeComponent();
            LoadSongData();
        }

        private void AddSongBut_Click(object sender, RoutedEventArgs e)
        {
            AddSongs addSong = new AddSongs();
            addSong.Show();
        }

        private void DelSongBut_Click(object sender, RoutedEventArgs e)
        {
            DelSongs delSong = new DelSongs();
            delSong.Show();
        }

        private void MenuBut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MenuVar = new MainWindow();
            MenuVar.Show();
            Close();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SearchBut_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchBox.Text;
            DataView dv = (DataView)SongsDG.ItemsSource;
            dv.RowFilter = string.Format("Название_песни like '%{0}%'", search);
        }

        private void RefreshSongsBut_Click(object sender, RoutedEventArgs e)
        {
            LoadSongData();
        }
    }
}
