using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Hub.TabMusic
{
    /// <summary>
    /// Logique d'interaction pour PlayerGUI.xaml
    /// </summary>
    public partial class PlayerGUI : UserControl
    {

        private bool IsTimeSliderValueChanging = false;  // Needed to make the time bar work properly
        private TabMusic Tab;

        public PlayerGUI()
        {
        }

        internal void SetTab(TabMusic tab)
        {
            InitializeComponent();

            Tab = tab;

            Tab.Player.StatusChangeEvent += Player_StatusChanged;  // Listen for the player events

            DispatcherTimer timer = new DispatcherTimer();  // Update the time bar every 100 milliseconds
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)  // Method that updates the time bar
        {
            if (Tab.Player.IsPlaying && !IsTimeSliderValueChanging) // Don't update if the user is dragging it
            {
                TimeSlider.Value = Tab.Player.Position;

                TimeSpan ts = TimeSpan.FromMilliseconds(Tab.Player.Position);
                string str = ts.ToString(@"hh\:mm\:ss");
                if (str[0] == '0' && str[1] == '0')
                    str = str.Substring(3, str.Length - 3);
                Position_Label.Content = str;
            }
        }

        private void Player_StatusChanged(object sender, PlayerStatusChangedEvent e)  // Implementation of the player status changed event
        {
            if (e.SongHasChanged)   // If the song has changed, update the GUI
            {
                TimeSlider.Maximum = Tab.Player.TotalDuration;

                TimeSpan ts = TimeSpan.FromMilliseconds(Tab.Player.TotalDuration);
                string str = ts.ToString(@"hh\:mm\:ss");
                if (str[0] == '0' && str[1] == '0')
                    str = str.Substring(3, str.Length - 3);
                TotalDuration_Label.Content = str;

                Title.Content = e.CurrentSong.Title;
                Artist.Content = e.CurrentSong.Artist;
                Cover.Source = ID3Tag.GetCover(e.CurrentSong);
            }
            if (e.IsPlaying)  // If it's only a play/pause event, update the play/pause button
                Play.Content = "||";
            else
                Play.Content = "|>";
        }

        private void Pause_Click(object sender, RoutedEventArgs e)  // Play/Pause button clicked
        {
            if (Tab.Player.IsPlaying)
                Tab.Player.Pause();
            else
                Tab.Player.Resume();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)  // Previous button clicked
        {
            throw new NotImplementedException("Sorry, you have to wait a little longer to use this awesome function...");
        }

        private void Next_Click(object sender, RoutedEventArgs e)  // Next button clicked
        {
            int index = new Random().Next(0, Tab.Songs.Count);  // Pick a random song and play it
            Tab.Player.Play(Tab.Songs.ElementAt(index));
        }


        private void TimeSlider_DragStarted(object sender, DragStartedEventArgs e)  // Update variables when the user start dragging the time bar
        {
            IsTimeSliderValueChanging = true;
        }

        private void TimeSlider_DragCompleted(object sender, DragCompletedEventArgs e)  // Update variables and player when it is finished
        {
            IsTimeSliderValueChanging = false;
            Tab.Player.Position = TimeSlider.Value;
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)  // Triggers when the user drags the volume slider
        {
            if (Tab != null)
                Tab.Player.Volume = e.NewValue;
        }
    }
}
