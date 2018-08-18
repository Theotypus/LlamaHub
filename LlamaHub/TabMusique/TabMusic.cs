using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hub.TabMusic
{
    class TabMusic : Tab
    {
        public override string Header { get; protected set; } = "TabMusique";
        public override UIElement RootElement { get; protected set; }
        public override TabItem TabItem { get; set; }
        public override Color PrimaryColor { get; protected set; } = Color.FromRgb(255, 102, 0);
        public override string PreviewImage { get; protected set; } = "TabMusic/Icons/TabMusicPreview.png";

        internal ObservableCollection<Song> Songs { get; private set; } = new ObservableCollection<Song>();  // All loaded songs
        internal Player Player { get; private set; } = new Player();

        public TabMusic()
        {
            RootElement = new TabMusicUI(this);
        }

        public BackgroundWorker ScanDirectory(string path, SearchOption option)
        {
            BackgroundWorker thread = new BackgroundWorker();
            thread.DoWork += ScanThread;
            thread.RunWorkerAsync(new Tuple<string, SearchOption>(path, option));
            return thread;
        }

        private void ScanThread(object sender, DoWorkEventArgs e)
        {
            string path = ((Tuple<string, SearchOption>)e.Argument).Item1;
            SearchOption option = ((Tuple<string, SearchOption>)e.Argument).Item2;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            List<string> files;
            try
            {
                files = Directory.EnumerateFiles(path, "*.*", option)  // List all mp3/wav files
.Where(s => s.EndsWith(".mp3") || s.EndsWith(".wav")).ToList();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You don't have access to this folder !");
                return;
            }

            foreach (String pathStr in files)
            {
                Song song = new Song(pathStr);  // Create a song object
                ID3Tag.Read(song);  // Read the metadata
                int i = 0;
                if (Songs.Count >= 1)
                    while ((song.Title.CompareTo(Songs[i].Title) > 0) && (i < Songs.Count - 1))
                        i++;
                Application.Current.Dispatcher.Invoke(new ThreadStart(delegate
                {
                    Songs.Insert(i, song);
                }));
            }
            watch.Stop();
            Console.WriteLine("Time: " + watch.ElapsedMilliseconds);
        }

        public List<SongList> GetSongList(SongListType type)
        {
            switch (type)
            {
                case SongListType.ARTIST:
                    List<SongList> artists = new List<SongList>();
                    foreach (Song song in Songs)
                    {
                        string artistStr = song.Artist;
                        bool found = false;
                        foreach (SongList list in artists)
                        {
                            if (list.Data["name"].Equals(artistStr))
                            {
                                list.Songs.Add(song);
                                found = true;
                                break;
                            }
                        }
                        if (found)
                            continue;
                        else
                        {
                            SongList artist = new SongList(SongListType.ARTIST);
                            artist.Songs.Add(song);
                            artist.Data["name"] = artistStr;
                            artists.Add(artist);
                        }

                    }
                    return artists.OrderBy(o => o.Data["name"]).ToList();

                case SongListType.ALBUM:
                    List<SongList> albums = new List<SongList>();
                    foreach (Song song in Songs)
                    {
                        string albumStr = song.Album;
                        bool found = false;
                        foreach (SongList list in albums)
                        {
                            if (list.Data["name"].Equals(albumStr) && list.Data["artist"].Equals(song.AlbumArtist))
                            {
                                list.Songs.Add(song);
                                found = true;
                                break;
                            }
                        }
                        if (found)
                            continue;
                        else
                        {
                            SongList album = new SongList(SongListType.ALBUM);
                            album.Songs.Add(song);
                            album.Data["name"] = albumStr;
                            album.Data["artist"] = song.AlbumArtist;
                            albums.Add(album);
                        }

                    }
                    return albums;

                default:
                    return null;
            }
        }

        public List<SongList> GetAlbumsFromArtist(SongList artist)
        {
            List<SongList> albums = new List<SongList>();
            foreach (Song song in artist.Songs)
            {
                string albumStr = song.Album;
                bool found = false;
                foreach (SongList list in albums)
                {
                    if (list.Data["name"].Equals(albumStr) && list.Data["artist"].Equals(song.AlbumArtist))
                    {
                        list.Songs.Add(song);
                        found = true;
                        break;
                    }
                }
                if (found)
                    continue;
                else
                {
                    SongList album = new SongList(SongListType.ALBUM);
                    album.Songs.Add(song);
                    album.Data["name"] = albumStr;
                    album.Data["artist"] = song.AlbumArtist;
                    albums.Add(album);
                }

            }
            return albums;
        }
    }
}
