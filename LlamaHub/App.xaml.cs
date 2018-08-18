using LCP_API;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Hub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly IPAddress Ip = IPAddress.Parse("127.0.0.1");
        private static readonly int Port = 4242;

        private void Application_Startup(object sender, StartupEventArgs e) // When Application Start
        {
            //Start the client
            Ressources.Client = new LCPClient(Ip, Port);
            Ressources.Client.Connect();

            if (Ressources.Client.Connected)   // Show log in window
            {
                LogInWindow logInWindow = new LogInWindow();
                logInWindow.Closing += OpenMainWindow;
                logInWindow.Show();
            }
            else   // Start the hub offline
            {
                MessageBox.Show("The connection to the server has failed. The hub will be launched offline.", "Sever not connected",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                MainWindow Window = new MainWindow();
                Window.Show();
            }
        }

        private void OpenMainWindow(object sender, EventArgs e)
        {
            MainWindow Window = new MainWindow();
            Window.Show();
        }

        private void App_Exit(object sender, ExitEventArgs e) // Close connections on exit
        {
            // Client.CloseConnection();
        }
    }
}
