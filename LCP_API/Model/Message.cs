using LCP_API;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCP_API
{
    public abstract class Message : IConversationItem
    {
        /* Represents a message
         * There are 3 types of messages : Text, Image and File */

        public string Id  // Should be enough to be unique
        {
            get { return Sender.Id.ToString() + UnixTime.ToString() + ConversationId; }
        }

        public bool Local { get; protected set; }

        public double UnixTime { get; protected set; }

        public User Sender { get; protected set; }

        public string ConversationId { get; protected set; }

        [BsonIgnore]
        public bool IsMessage => true;

        public Message() { }

        public Message(User sender, double unixtime, string convid, bool local)
        {
            Sender = sender;
            UnixTime = unixtime;
            ConversationId = convid;
            Local = local;
        }
    }


    public class TextMessage : Message
    {
        public string Text { get; private set; }

        public TextMessage() { }

        public TextMessage(string text, User sender, double unixtime, string convid, bool local) :
                base(sender, unixtime, convid, local)
        {
            Text = text;
        }
    }

    public class ImageMessage : Message  // TODO: Not finished
    {
        public string Path { get; private set; }

        public ImageMessage() { }

        public ImageMessage(string path, User sender, double unixtime, string convid, bool local) :
                base(sender, unixtime, convid, local)
        {
            Path = path;
        }
    }

    public class FileMessage : Message  // TODO: this
    {
        public FileMessage() { }
    }
}
