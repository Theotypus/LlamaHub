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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hub.TabMusic
{
    /// <summary>
    /// Logique d'interaction pour ArtistListView.xaml
    /// </summary>
    public partial class ArtistListView : UserControl
    {

        private TabMusic Tab;

        public ArtistListView()
        {
        }

        internal ArtistListView(TabMusic tab)
        {
            SetTab(tab);
        }

        internal void SetTab(TabMusic tab)
        {
            Tab = tab;
            InitializeComponent();
            ArtistsBox.ItemsSource = Tab.GetSongList(SongListType.ARTIST);
        }

        private void ArtistItemTemplate_ItemClick(object sender, MouseButtonEventArgs e)
        {
            SongList selected = (sender as StackPanel).DataContext as SongList;

            ArtistView.Children.Clear();
            SongList artist = selected as SongList;
            foreach (SongList album in Tab.GetAlbumsFromArtist(artist))
            {
                TextBlock title = new TextBlock()
                {
                    Text = album.Data["name"]
                };
                ListBox songs = new ListBox()
                {
                    ItemsSource = album.Songs,
                    ItemTemplate = Root.FindResource("SongTemplate") as DataTemplate
                };
                /*VirtualizingPanel.SetIsVirtualizing(songs, true);
                VirtualizingPanel.SetVirtualizationMode(songs, VirtualizationMode.Recycling);*/
                ArtistView.Children.Add(title);
                ArtistView.Children.Add(songs);
            }
        }

        private void SongItemTemplate_ItemClick(object sender, MouseButtonEventArgs e)  // Triggers when an item of the listbox is clicked
        {
            if (e.ClickCount != 2)  // If double click
                return;

            Song selected = (sender as StackPanel).DataContext as Song;
            Tab.Player.Play(selected);  // Play the selected song
        }
    }
}
