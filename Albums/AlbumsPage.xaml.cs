using Npgsql;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using AudioCornerDB;
using AudioCorner.Albums;

namespace AudioCorner
{

    public partial class AlbumsPage : Window
    {
        DataBase DB = new DataBase();
        public string query;
        private void LoadAlbumData()
        {
            try
            {
                DB.OpenConnection();

                query = @"select ""Album_ID"" as ""ID альбома"", ""Band_ID"" as ""ID группы"", ""Album_Name"" as ""Название_альбома"", ""Num_Of_Songs"" as ""Количество песен"", ""Genre"" as ""Жанр"", ""Release_Date"" as ""Дата выпуска"" from public.""Album"" order by ""Album_ID""";

                NpgsqlCommand cmd = new NpgsqlCommand(query, DB.GetConnection());

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable DT = new DataTable();
                    DT.Load(reader);
                    AlbumsDG.ItemsSource = DT.DefaultView;
                }
                DB.CloseConnection();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        public AlbumsPage()
        {
            InitializeComponent();
            LoadAlbumData();
        }

        private void AddAlbumBut_Click(object sender, RoutedEventArgs e)
        {
            AddAlbum addAlbum = new AddAlbum();
            addAlbum.Show();
        }

        private void DelAlbumBut_Click(object sender, RoutedEventArgs e)
        {
            DelAlbum delAlbum = new DelAlbum();
            delAlbum.Show();
        }

        private void RefreshAlbumsBut_Click(object sender, RoutedEventArgs e)
        {
            LoadAlbumData();
        }

        private void SearchBut_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchBox.Text;
            DataView dv = (DataView)AlbumsDG.ItemsSource;
            dv.RowFilter = string.Format("Название_альбома like '%{0}%'", search);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MenuBut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MenuVar = new MainWindow();
            MenuVar.Show();
            Close();
        }
    }
}
