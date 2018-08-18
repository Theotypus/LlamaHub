using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace LCP_API
{
    public class Conversation : INotifyPropertyChanged
    {
        /* Model class that represents a conversation
         * Each conversation is identified with a unique Id generated on the server side (uuid that looks like: "5c3b99d2a16311e8afdc7824afcab585")
         * The 'Items' list contains both the messages and the events (ex: when the name changes or a user is added) from the conversation */

        public string Id { get; private set; }

        public ObservableCollection<User> Participants { get; private set; } = new ObservableCollection<User>();

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                FirePropertyChanged("Name");
            }
        }

        [BsonIgnore]  // /!\ Use the AddItem() method to keep the list sorted
        public ObservableCollection<IConversationItem> Items { get; private set; } = new ObservableCollection<IConversationItem>();

        private byte[] _image;

  /*      [BsonIgnore]
        public BitmapImage Image
        {
            get
            {
                return GetImage();
            }
        }*/


        public Message LastMessage
        {
            get  // Returns the last item of type 'message' (if it exists)
            {
                return (Items.Where(x => x.IsMessage).Count() > 0) ? Items.Where(x => x.IsMessage).Last() as Message : null;
            }
        }

        public Conversation() { }

        public Conversation(string id, string name, List<User> participants, byte[] image = null)
        {
            Id = id;
            _name = name;
            participants.ForEach(Participants.Add);
            _image = image;
        }

        // Insert the item in the right place and update the last message property
        internal void AddItem(IConversationItem item)
        {
            Items.Add(item);
            Sort();    // TODO: No need to sort the whole list, just insert the item in the right place

            if (item.IsMessage && LastMessage == item)
                FirePropertyChanged("LastMessage");
        }


        // Sort an observablecollection without creating a new one (to keep bindings working)
        private void Sort()
        {
            List<IConversationItem> sortableList = new List<IConversationItem>(Items);
            sortableList = sortableList.OrderBy(x => x.UnixTime).ToList();

            for (int i = 0; i < sortableList.Count; i++)
            {
                Items.Move(Items.IndexOf(sortableList[i]), i);
            }
        }

        /*public BitmapImage GetImage()
        {
            if (_image == null || _image.Length == 0)  // Load default image
                return new BitmapImage(new Uri("pack://application:,,,/LCP API/Icons/GroupIcon.png"));
            else
            {
                var image = new BitmapImage();
                using (var mem = new MemoryStream(_image))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                return image;
            }
        }*/

        // To update the GUI when a property changes

        public event PropertyChangedEventHandler PropertyChanged;

        protected void FirePropertyChanged(String name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
