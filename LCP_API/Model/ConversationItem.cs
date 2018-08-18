using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LCP_API
{
    public interface IConversationItem
    {
        // Unifies messages and events from a conversation with basic commun properties

        double UnixTime { get; }
        bool IsMessage { get; }
        string ConversationId { get; }
    }
}
