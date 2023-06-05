using System.Windows;

namespace AudioCorner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BandsBut_Click(object sender, RoutedEventArgs e)
        {
            BandsPage BandsVar = new BandsPage();
            BandsVar.Show();
            Close();
        }

        private void AlbumsBut_Click(object sender, RoutedEventArgs e)
        {
            AlbumsPage AlbumsVar = new AlbumsPage();
            AlbumsVar.Show();
            Close();
        }

        private void SongsBut_Click(object sender, RoutedEventArgs e)
        {
            SongsPage SongsVar = new SongsPage();
            SongsVar.Show();
            Close();
        }
    }
}
