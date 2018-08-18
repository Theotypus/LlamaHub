using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using LCP_API;

namespace Hub.TabMessage
{
    class FunnyCommand
    {
        private LCPClient Client;
        private StackPanel DrawPanel;

        public FunnyCommand(LCPClient client, StackPanel drawPanel)
        {
            Client = client;
            DrawPanel = drawPanel;
        }

        public void Start()
        {
            Client.ConversationItemEvent += OnMessageReceived;
        }

        public void Stop()
        {
            Client.ConversationItemEvent -= OnMessageReceived;
        }


        private async void OnMessageReceived(object sender, ConversationItemArgs e)
        {
            if (e.Item.IsMessage && (e.Item as TextMessage).Text.StartsWith("/"))
            {
                TextMessage msg = e.Item as TextMessage;  // TODO: Why the fuck isn't the image centered vertically ??
                if (msg.Text.StartsWith("/totoapproved", true, CultureInfo.CurrentUICulture))
                {
                    Image img = new Image()
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/TabMessage/Icons/TotoApproved.png"))
                    };
                    DrawPanel.Children.Add(img);

                    DoubleAnimation da = new DoubleAnimation(2000, 400, TimeSpan.FromSeconds(1))
                    {
                        EasingFunction = new CircleEase()
                        {
                            EasingMode = EasingMode.EaseOut,
                        }
                    };
                    img.VerticalAlignment = VerticalAlignment.Center;
                    img.HorizontalAlignment = HorizontalAlignment.Center;

                    img.BeginAnimation(FrameworkElement.WidthProperty, da);
                    img.BeginAnimation(FrameworkElement.HeightProperty, da);
                    await Task.Delay(2000);
                    DrawPanel.Children.Remove(img);
                }
                else if (msg.Text.StartsWith("/apoual", true, CultureInfo.CurrentUICulture))
                {
                    SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                    string speech = msg.Sender.Username + " vous demande de vous mettre à poil tout de suite";
                    synthesizer.SpeakAsync(speech);
                }
            }
        }

        public void Send(Message message)
        {

        }
    }
}
