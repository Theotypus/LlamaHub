using LCP_API;
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

namespace Hub.TabMessage
{
    /// <summary>
    /// Logique d'interaction pour NoConnectionView.xaml
    /// </summary>
    public partial class NoConnectionView : UserControl
    {
        public NoConnectionView()
        {
            InitializeComponent();
        }

        private void OnConnectClick(object sender, RoutedEventArgs e)
        {
            Ressources.Client.Connect();
            if (Ressources.Client.Connected)
            {
                new LogInWindow().Show();
            }
            else
            {
                MessageBox.Show("The connection to the server has failed. The hub will be launched offline.", "Sever not connected",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
