using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCP_API
{
    public class ClientStateChangedEvent : EventArgs
    {
        public bool Connected { get; private set; }

        public ClientStateChangedEvent(bool connected)
        {
            Connected = connected;
        }
    }

    public class LogInSuccessfullEvent : EventArgs
    {
        public LogInSuccessfullEvent() { }
    }

    public class LogInFailedEvent : EventArgs    // TODO: Create enum for errors causes
    {
        public string Error { get; private set; }

        public LogInFailedEvent(string error)
        {
            Error = error;
        }
    }

    public class ClientSignedOutEvent : EventArgs
    {
        public ClientSignedOutEvent() { }
    }

    public class SearchResultEvent : EventArgs
    {
        public List<User> Results;

        public SearchResultEvent(List<User> results)
        {
            Results = results;
        }
    }

    public class ConversationItemArgs : EventArgs
    {
        public IConversationItem Item { get; private set; }

        public ConversationItemArgs(IConversationItem item)
        {
            Item = item;
        }
    }
}
