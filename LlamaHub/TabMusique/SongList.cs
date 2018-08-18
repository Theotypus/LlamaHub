using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hub.TabMusic
{
    class SongList
    {
        public List<Song> Songs { get; set; } = new List<Song>();
        public SongListType Type { get; private set; }

        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();

        public BitmapImage Cover
        {
            get
            {
                foreach (Song song in Songs)
                {
                    var cover = song.Cover;
                    if (cover != null)
                        return cover;
                }
                return null;
            }
        }

        public SongList(SongListType type)
        {
            Type = type;
        }
    }
}
