using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hub.TabMusic
{
    class PlayerStatusChangedEvent : EventArgs  // Event that triggers when the media player status changes (play/pause, song changed...)
    {
        public bool IsPlaying { get; set; }  // Is the player currently playing
        public Song CurrentSong { get; set; }  // The current loaded song
        public bool SongHasChanged { get; set; }  // Has the loaded song changed with this event?
    }
}
