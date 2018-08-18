using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hub.TabMusic
{
    static class ID3Tag  // Utility class to read song's metadatas
    {
        public static void Read(Song song)  // Set the song's title, artist and album
        {
            try
            {
                TagLib.File file = TagLib.File.Create(song.Path);
                song.Title = file.Tag.Title;
                song.Artist = file.Tag.FirstPerformer;
                song.Album = file.Tag.Album;
                song.AlbumArtist = file.Tag.FirstAlbumArtist;
            }
            catch (TagLib.CorruptFileException)
            {
                Console.WriteLine("Error while parsing " + song.Path);
            }
            if (song.Title == null)  // Use the file name as title if there's no metadata
            {
                string[] tab = song.Path.Split('\\');
                song.Title = tab[tab.Length - 1];
            }
            if (song.Artist == null)
                song.Artist = "<Unknown>";
            if (song.Album == null)
                song.Album = "<Unknown>";
            if (song.AlbumArtist == null)
                song.AlbumArtist = song.Artist;
        }

        public static BitmapImage GetCover(Song song)  // Retrieve and returns the album cover
        {
            try
            {
                TagLib.File file = TagLib.File.Create(song.Path);

                if (file.Tag.Pictures.Length >= 1)
                {
                    byte[] bin = file.Tag.Pictures[0].Data.Data;
                    using (var ms = new MemoryStream(bin))
                    {
                        ms.Position = 0;
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.StreamSource = ms;
                        bi.EndInit();
                        return bi;
                    }
                }
            }
            catch (TagLib.CorruptFileException) { }

            return null;  // Should return a default cover
        }
    }
}
