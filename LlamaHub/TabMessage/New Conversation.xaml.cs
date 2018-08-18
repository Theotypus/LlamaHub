using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LCP_API;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Hub.TabMessage
{
    /// <summary>
    /// Logique d'interaction pour NewConversation.xaml
    /// </summary>
    public partial class NewConversation : System.Windows.Controls.UserControl
    {

        private ObservableCollection<User> Participants = new ObservableCollection<User>();
        private ObservableCollection<User> SearchResults = new ObservableCollection<User>();
        private byte[] ImageData = null;

        private LCPClient Client;
        private System.Windows.Controls.Panel Root;

        public NewConversation(LCPClient client, System.Windows.Controls.Panel root)
        {
            Client = client;
            Root = root;

            DataContext = this;
            InitializeComponent();

            ResultBox.ItemsSource = SearchResults;
            ParticipantsBox.ItemsSource = Participants;

            Client.SearchResultEvent += UpdateSearchResults;
        }

        public void UpdateSearchResults(object sender, SearchResultEvent e)
        {
            SearchResults.Clear();

            if (e.Results.Count == 0)
                System.Windows.Forms.MessageBox.Show("The search you asked for returned no results. That's a shame...", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                foreach (User user in e.Results)
                    if (!Participants.Contains(user))
                        SearchResults.Add(user);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (!NameBox.Text.Equals("") & Participants.Count > 0)
            {
                Client.CreateConversation(NameBox.Text, Participants.ToList(), ImageData);
                Root.Children.Remove(this);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Root.Children.Remove(this);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Image Files (*.png)|*.png";
            //openFileDialog1.FilterIndex = 2;
            //openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            MemoryStream ms = new MemoryStream();
                            myStream.CopyTo(ms);
                            ImageData = ms.ToArray();

                            BitmapImage biImg = new BitmapImage();
                            MemoryStream ms2 = new MemoryStream(ImageData);
                            biImg.BeginInit();
                            biImg.StreamSource = ms2;
                            biImg.EndInit();

                            Image.Fill = new ImageBrush
                            {
                                ImageSource = biImg
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Client.Search(SearchBox.Text);
        }

        private void Results_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selected = ResultBox.SelectedItem as User;
            if (selected != null)
            {
                Participants.Add(selected);
                SearchResults.Remove(selected);
            }
        }
    }
}
