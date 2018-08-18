using LCP_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hub.TabMessage
{
    class TabMessage : Tab  //Class that controls the TabMessage background work (not much right now...)
    {
        public override string Header { get; protected set; } = "TabMessage";
        public override UIElement RootElement { get; protected set; }
        public override TabItem TabItem { get; set; }
        public override Color PrimaryColor { get; protected set; } = Color.FromRgb(0, 153, 77);
        public override string PreviewImage { get; protected set; } = "TabMessage/Icons/TabMessagePreview.png";

        public TabMessage()
        {
            LCPClient client = Ressources.Client;
            client.ClientStateChangedEvent += OnClientStateChanged;
            client.LogInSuccessfullEvent += OnLogInSuccessfull;
            client.ClientSignedOutEvent += OnClientSignedOut;

            if (client.Connected && client.LoggedIn)
                RootElement = new TabMessageUI();
            else
                RootElement = new NoConnectionView();
        }

        private void OnClientSignedOut(object sender, ClientSignedOutEvent e)
        {
            RootElement = new NoConnectionView();
            TabItem.Content = RootElement;
        }

        private void OnLogInSuccessfull(object sender, LogInSuccessfullEvent e)
        {
            RootElement = new TabMessageUI();
            TabItem.Content = RootElement;
        }

        private void OnClientStateChanged(object sender, ClientStateChangedEvent e)
        {
            if (!e.Connected)
            {
                RootElement = RootElement = new NoConnectionView();
                TabItem.Content = RootElement;
            }
        }
    }
}