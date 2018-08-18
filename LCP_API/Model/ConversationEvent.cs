using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCP_API
{
    public class ConversationEvent : IConversationItem
    {
        /* Represents an event in a conversation (Ex: Creation, name changed, user added/left)
        See 'EventType' enum for a complete list */

        public string Id  // Should be unique this way
        {
            get { return Sender.Id.ToString() + UnixTime.ToString() + ConversationId; }
        }

        public EventType Type { get; private set; }

        public double UnixTime { get; private set; }

        public User Sender { get; private set; }  // The person who started the event

        public string ConversationId { get; private set; }

        /* Optional arguments:
         *  - NameChanged : string "Name" = new name
         *  - UserAdded : User "User" = user added */

        public Dictionary<string, object> Args { get; private set; } = new Dictionary<string, object>();

        [BsonIgnore]
        public bool IsMessage => false;

        [BsonIgnore]
        public string Text
        {
            get
            {
                switch (Type)
                {
                    case EventType.ConversationCreated:
                        return Sender.Username + " created the conversation";

                    case EventType.NameChanged:
                        return Sender.Username + " changed the name to \"" + Args["Name"] + "\"";

                    case EventType.UserAdded:
                        return Sender.Username + " added " + (Args["User"] as User).Username + " to the conversation";

                    case EventType.UserLeft:
                        return Sender.Username + " left the conversation";

                    default:
                        return "";
                }
            }
        }

        public ConversationEvent() { }

        public ConversationEvent(User sender, double unixtime, string convid, EventType type)
        {
            Sender = sender;
            UnixTime = unixtime;
            ConversationId = convid;
            Type = type;
        }
    }

    public enum EventType
    {
        ConversationCreated,
        UserAdded,
        UserLeft,
        NameChanged
    }
}
