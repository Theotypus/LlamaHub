using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Hub.TabMusic
{
    class Player  // Wrapper around the wpf's media player
    {
        public event EventHandler<PlayerStatusChangedEvent> StatusChangeEvent;  // Event that triggers when the player's status changes (play/pause, song changed...)

        private MediaPlayer MediaPlayer = new MediaPlayer();  // Instance of the wpf's player
        public bool IsPlaying { get; private set; } = false;  // Current status of the player (playing or paused)

        public Song CurrentSong { get; private set; }  // The song currently loaded into the media player

        public double Position  // Current position of the player IN MILLISECONDES
        {
            get
            {
                return MediaPlayer.Position.TotalMilliseconds;
            }
            set
            {
                MediaPlayer.Position = TimeSpan.FromMilliseconds(value);
            }
        }

        public double Volume  // Volume property between 0.0 and 1.0
        {
            get
            {
                return MediaPlayer.Volume;
            }
            set
            {
                MediaPlayer.Volume = value;
            }
        }

        public double TotalDuration  // Total length of the current song IN MILLISECONDS
        {
            get
            {
                if (MediaPlayer.NaturalDuration.HasTimeSpan)
                    return MediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                return 0;
            }
        }

        public Player()
        {
            MediaPlayer.MediaEnded += MediaEnded;  // Update variables when the song is over
            MediaPlayer.MediaOpened += MediaOpened;  // Trigger events when a new song is loaded
        }

        public void Play(Song song)  // Loads and play the song
        {
            if (IsPlaying)  // Stop the media player
            {
                MediaPlayer.Stop();
                IsPlaying = false;
            }
            MediaPlayer.Open(new Uri(song.Path));  // Open the new song
            CurrentSong = song;
            Resume();  // Play it
        }

        public void Resume()  // Unpause the player
        {
            if (!IsPlaying && CurrentSong != null)
            {
                MediaPlayer.Play();
                UpdatePlayStatus(true);
            }
        }
        public void Pause()  // Pause the player
        {
            if (IsPlaying)
            {
                MediaPlayer.Pause();
                UpdatePlayStatus(false);
            }
        }

        private void MediaOpened(object sender, EventArgs e)  // Event that triggers once a song is loaded
        {
            StatusChangeEvent?.Invoke(this, new PlayerStatusChangedEvent // Trigger the status changed event
            {
                IsPlaying = IsPlaying,
                CurrentSong = this.CurrentSong,
                SongHasChanged = true
            });
        }
        private void MediaEnded(object sender, EventArgs e)  // Event that triggers once a song is over
        {
            UpdatePlayStatus(false);  // Update variables
        }

        private void UpdatePlayStatus(bool playing)  // Internal function that updates varaibles and triggers the status changed event
        {
            IsPlaying = playing;
            StatusChangeEvent?.Invoke(this, new PlayerStatusChangedEvent
            {
                IsPlaying = IsPlaying,
                CurrentSong = CurrentSong,
                SongHasChanged = false
            });
        }
    }
}
