using Npgsql;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using AudioCornerDB;

namespace AudioCorner
{
    public partial class BandsPage : Window
    {
        DataBase DB = new DataBase();
        public string query;
        private void LoadDataFromDB()
        {
            try
            {
                DB.OpenConnection();

                query = @"select ""Band_ID"" as ""ID группы"", ""Band_Name"" as ""Название_группы"", ""Formed_In"" as ""Дата формирования"" from public.""Band"" order by ""Band_ID""";

                NpgsqlCommand cmd = new NpgsqlCommand(query, DB.GetConnection());

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable DT = new DataTable();
                    DT.Load(reader);
                    BandsDG.ItemsSource = DT.DefaultView;
                }
                DB.CloseConnection();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        public BandsPage()
        {
            InitializeComponent();
            LoadDataFromDB();
        }

        private void MenuBut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MenuVar = new MainWindow();
            MenuVar.Show();
            Close();
        }

        public void AddBut_Click(object sender, RoutedEventArgs e)
        {
            AddBand addBand = new AddBand();
            addBand.Show();
        }

        private void DelBut_Click(object sender, RoutedEventArgs e)
        {
            DelBand delBand = new DelBand();
            delBand.Show();
        }

        private void SearchBut_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchBox.Text;
            DataView dv = (DataView)BandsDG.ItemsSource;
            dv.RowFilter = string.Format("Название_группы like '%{0}%'", search);
        }

        private void RefreshBandsBut_Click(object sender, RoutedEventArgs e)
        {
            LoadDataFromDB();
        }
    }
}