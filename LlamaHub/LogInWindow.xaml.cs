using LCP_API;
using System.Windows;

namespace Hub
{
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
            Ressources.Client.LogInSuccessfullEvent += OnLogInSuccessfull;
            Ressources.Client.LogInFailedEvent += OnLogInFailed;
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (!(Username.Text.Equals("") || Password.Password.Equals("")))
                Ressources.Client.LogIn(Username.Text, Password.Password);
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (!(Username.Text.Equals("") || Password.Password.Equals("")))
                Ressources.Client.SignUp(Username.Text, Username.Text, Password.Password);
        }

        private void OnLogInFailed(object sender, LogInFailedEvent e)
        {
            MessageBoxResult result = MessageBox.Show("Error : " + e.Error, "Log In Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnLogInSuccessfull(object sender, LogInSuccessfullEvent e)
        {
            Close();
        }
    }
}
