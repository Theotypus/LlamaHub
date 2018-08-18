using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using LCP_API;
using System.Linq;
using System.Collections.ObjectModel;
using System.Speech;
using System.Speech.Synthesis;
using System.ComponentModel;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;

namespace Hub.TabMessage
{
    /// <summary>
    /// Logique d'interaction pour TabMessageGUI.xaml
    /// </summary>
    public partial class TabMessageUI : UserControl  //Class that controls the TabMessage GUI
    {
        private LCPClient Client= Ressources.Client;

        private Conversation Selected;

        private ConversationMenu ConvMenu = null;
        private EditConversation EditMenu = null;

        public TabMessageUI()
        {
            DataContext = this;

            InitializeComponent();

            Username.Content = Client.Local.Username;
            ConvoBox.ItemsSource = Client.Conversations;

            FunnyCommand command = new FunnyCommand(Client, DrawPanel);
            command.Start();
        }

        //Button "Send" clicked
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Text == null || Selected == null || Selected.Participants.Where(x => x.Id == Client.Local.Id).Count() == 0)
                return;

            /*DateTime t = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long unixTime = (long)(DateTime.Now - t).TotalSeconds;*/

            TextMessage msg = new TextMessage(MessageBox.Text, Client.Local, Tools.GetUnixTime(), Selected.Id, true);
            Client.SendMessage(msg);
            MessageBox.Clear();
        }

        private void NewConversation_Click(object sender, RoutedEventArgs e)
        {
            NewConversation menu = new NewConversation(Client, Root);
            Grid.SetRowSpan(menu, 4);
            Panel.SetZIndex(menu, 1);
            Root.Children.Add(menu);
        }

        // Called when a conversation is selected
        private void ConvoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Selected = ConvoBox.SelectedItem as Conversation;  // Retrieve the selected conversation
            if (Selected == null)                                           // and display its messages
            {
                MessagesList.ItemsSource = null;
                Participants.Content = "";
                ConversationHeader.Content = "";
                ConversationImage.Fill = new SolidColorBrush((Color)Root.FindResource("PrimaryColor"));
            }
            else
            {
                Binding nameBinding = new Binding()
                {
                    Source = Selected,
                    Path = new PropertyPath("Name"),
                    Mode = BindingMode.OneWay
                };
                BindingOperations.SetBinding(ConversationHeader, Label.ContentProperty, nameBinding);

                Binding participantsBinding = new Binding()
                {
                    Source = Selected,
                    Path = new PropertyPath("Participants"),
                    Mode = BindingMode.OneWay,
                    Converter = new ParticipantToTextConverter()
                };
                BindingOperations.SetBinding(Participants, Label.ContentProperty, participantsBinding);

                MessagesList.ItemsSource = Selected.Items;

                //ConversationImage.Fill = new ImageBrush(Selected.Image);
            }
            if (ConvMenu != null)
                CloseConvMenu();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if (ConvMenu != null)
                CloseConvMenu();
            else if (Selected != null)
            {
                ConvMenu = new ConversationMenu(Client, Root, Selected, this);
                Grid.SetRowSpan(ConvMenu, 4);
                Grid.SetColumn(ConvMenu, 2);
                Panel.SetZIndex(ConvMenu, 1);
                Root.Children.Add(ConvMenu);
            }
        }

        private void CloseConvMenu()
        {
            Root.Children.Remove(ConvMenu);
            ConvMenu = null;

            if (EditMenu != null)
            {
                Root.Children.Remove(EditMenu);
                EditMenu = null;
            }
        }

        public void ShowEditConvMenu()
        {
            EditMenu = new EditConversation(Client, Root, Selected);
            Grid.SetRowSpan(EditMenu, 4);
            Grid.SetColumn(EditMenu, 2);
            Panel.SetZIndex(EditMenu, 2);
            Root.Children.Add(EditMenu);
        }
    }




    public class ParticipantToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Join(", ", (value as ObservableCollection<User>).Select(i =>
                i.Username));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class UnixTimeToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return String.Format("{0:t}", dtDateTime.AddSeconds((double)value).ToLocalTime());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
