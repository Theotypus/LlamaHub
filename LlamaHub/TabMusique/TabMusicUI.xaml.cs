using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Hub.TabMusic
{
    /// <summary>
    /// Logique d'interaction pour TabMusiqueGUI.xaml
    /// </summary>
    public partial class TabMusicUI : System.Windows.Controls.UserControl
    {
        private TabMusic Tab;

        internal TabMusicUI(TabMusic tabMusic)
        {
            InitializeComponent();
            DataContext = this;
            Tab = tabMusic;

            ContentRoot.Children.Add(new SongListView(Tab));

            Player.SetTab(Tab);
        }

        private void Open_Click(object sender, RoutedEventArgs e)  // Open button clicked
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();  // Ask for a folder to scan
            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK)
                return;

            string path = dialog.SelectedPath;
            Tab.ScanDirectory(path, SearchOption.AllDirectories);
        }

        private void Song_Click(object sender, RoutedEventArgs e)
        {
            ContentRoot.Children.Clear();
            ContentRoot.Children.Add(new SongListView(Tab));
        }
        private void Artist_Click(object sender, RoutedEventArgs e)
        {
            ContentRoot.Children.Clear();
            ContentRoot.Children.Add(new ArtistListView(Tab));
        }
    }
}
