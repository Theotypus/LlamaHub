using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hub.TabMusic
{
    class Song  // Model class that represents a song (change to a structure ?)
    {
        // Metadata
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string AlbumArtist { get; set; }

        public string Path { get; set; }  // Path of the file

        public BitmapImage Cover
        {
            get
            {
                return ID3Tag.GetCover(this);
            }
        }

        public Song(string path)  // /!\ Initialize all metadata using ID3Tag.Read(song)
        {
            Path = path;
        }
    }
}
