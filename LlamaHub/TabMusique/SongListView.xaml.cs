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
    /// Logique d'interaction pour SongListView.xaml
    /// </summary>
    public partial class SongListView : UserControl
    {
        private TabMusic Tab;

        public SongListView()
        {
        }
        internal SongListView(TabMusic tab)
        {
            SetTab(tab);
        }

        internal void SetTab(TabMusic tab)
        {
            Tab = tab;
            InitializeComponent();
            SongBox.ItemsSource = Tab.Songs;
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
