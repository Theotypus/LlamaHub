using System.Windows.Controls;
using LCP_API;
using System.Windows;

namespace Hub.TabMessage
{
    /// <summary>
    /// Logique d'interaction pour ConversationMenu.xaml
    /// </summary>
    public partial class ConversationMenu : UserControl
    {
        private LCPClient Client;
        private Conversation Conversation;
        private Panel Root;
        private TabMessageUI Controller;

        public ConversationMenu(LCPClient client, Panel root, Conversation conv, TabMessageUI controller)
        {
            Conversation = conv;
            Client = client;
            Root = root;
            Controller = controller;

            DataContext = Conversation;
            InitializeComponent();
        }


        private void Leave_Click(object sender, RoutedEventArgs e)
        {
            Client.LeaveConversation(Conversation);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Controller.ShowEditConvMenu();
        }
    }
}
