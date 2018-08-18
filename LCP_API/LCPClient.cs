using Hub;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Xml.Linq;

namespace LCP_API
{
    public class LCPClient
    {
        /* This class is the interface between the GUI and the background work (database/server connection)
         * 
         * The database (using litedb) is updated in real time
         * 
         * The client communicates (for now) with the server through xml commands that are built using 'CommandBuilder'
         * and decoded using 'CommandReceiver'. For exemples, see 'CommandModel.xml'. 
         * The 'ServerConnection' represents the low level connection to the server. */

        public event EventHandler<LogInSuccessfullEvent> LogInSuccessfullEvent;
        public event EventHandler<LogInFailedEvent> LogInFailedEvent;
        public event EventHandler<ClientSignedOutEvent> ClientSignedOutEvent;
        public event EventHandler<ClientStateChangedEvent> ClientStateChangedEvent;
        public event EventHandler<SearchResultEvent> SearchResultEvent;
        public event EventHandler<ConversationItemArgs> ConversationItemEvent;

        // Bind to this collection to update the GUI automatically when a conversation is created
        public ObservableCollection<Conversation> Conversations { get; private set; } = new ObservableCollection<Conversation>();

        private readonly IPAddress Ip;
        private readonly int Port;

        public User Local = null;  // The currently connected user
        public bool LoggedIn = false;  // Same idea as below
        public bool Connected
        {
            get   // To be connected or not to be connected
            {
                return Connection.Connected;
            }
        }

        public string DbPath = "";  // Each user has its own database (path: /Users/<username>)

        private ServerConnection Connection;

        public LCPClient(IPAddress ip, int port)
        {
            Ip = ip;
            Port = port;

            Connection = new ServerConnection(this);
        }




        /* -------- Interface with the server/database ----------- */

        public void LogIn(string username, string password)
        {
            // Connect the socket
            if (!Connection.Connected)
                Connection.Connect(Ip, Port);

            // Create/read the database
            Directory.CreateDirectory("Users");
            DbPath = "Users/" + username + ".db";
            InitializeDb();

            using (var db = new LiteDatabase(DbPath))
            {
                var users = db.GetCollection<User>("users");
                if (users.Exists(Query.EQ("Username", username)))  // Retrieve the local user if it exists
                    Local = users.FindOne(Query.EQ("Username", username));
                else
                    Local = new User(-1, username);  // Otherwise, set a random id that will be updated and added to the db when the server replies
            }

            /* Find the time of the last message received (accross all conversations)
             * so that the server can send only the new messages */
            double last_message = 0;
            foreach (Conversation conv in Conversations)
            {
                if (conv.LastMessage != null && conv.LastMessage.UnixTime > last_message)
                    last_message = conv.LastMessage.UnixTime;
            }

            Connection.Send(CommandBuilder.LogIn(username, password, last_message));
        }

        public void SignUp(string username, string name, string password)
        {
            //See above
            if (!Connection.Connected)
                Connection.Connect(Ip, Port);

            Directory.CreateDirectory("Users");
            DbPath = "Users/" + username + ".db";
            InitializeDb();

            Local = new User(-1, username);
            Connection.Send(CommandBuilder.SignUp(username, name, password));
        }

        public void SendMessage(Message message)
        {
            Connection.Send(CommandBuilder.Message(message.ConversationId, (message as TextMessage).Text, message.UnixTime));
            ProcessConversationItem(message);  // No need to wait for the server to answer to process the message
        }

        public void CreateConversation(string name, List<User> participants, byte[] image = null)
        {
            // We can't add the conversation locally as the id is delivered by the server
            // TODO: Create the id locally?
            Connection.Send(CommandBuilder.CreateConversation(name, participants, image));
        }

        public void Search(string request)
        {
            // The server will return all users whose username contains <request>
            Connection.Send(CommandBuilder.Search(request));
        }

        public void Connect()
        {
            // Manually connect the client (if needed)
            if (!Connected)
                Connection.Connect(Ip, Port);
        }

        public void LeaveConversation(Conversation conv)
        {
            Connection.Send(CommandBuilder.Leave(conv));

            ConversationEvent evt = new ConversationEvent(Local, Tools.GetUnixTime(), conv.Id, EventType.UserLeft);
            ProcessConversationItem(evt);
        }

        public void ChangeConversationName(Conversation conv, string name)
        {
            Connection.Send(CommandBuilder.ChangeName(conv, name));

            ConversationEvent evt = new ConversationEvent(Local, Tools.GetUnixTime(), conv.Id, EventType.NameChanged);
            evt.Args["Name"] = name;
            ProcessConversationItem(evt);
        }

        public void AddParticipant(Conversation conv, User user)
        {
            Connection.Send(CommandBuilder.Add(conv, user));

            using (var db = new LiteDatabase(DbPath))
            {
                // If the user is not already in the db, add it
                var users = db.GetCollection<User>("users");
                if (!users.Exists(Query.EQ("_id", user.Id)))
                    users.Insert(user);
            }

            ConversationEvent evt = new ConversationEvent(Local, Tools.GetUnixTime(), conv.Id, EventType.UserAdded);
            evt.Args["User"] = user;
            ProcessConversationItem(evt);
        }

        public void SignOut()
        {
            ClientSignedOutEvent?.Invoke(null, new ClientSignedOutEvent());
            throw new NotImplementedException();
        }








        /* ---------- Internal method to process new events/messages/conversations/... ----------- */

        private void InitializeDb()
        {
            Conversations.Clear();
            using (var db = new LiteDatabase(DbPath))
            {
                var users = db.GetCollection<User>("users");
                var conversations = db.GetCollection<Conversation>("conversations");
                var messages = db.GetCollection<Message>("messages");
                var events = db.GetCollection<ConversationEvent>("events");

                users.EnsureIndex(x => x.Id, true);
                conversations.EnsureIndex(x => x.Id, true);

                // First retrieve all the conversations
                conversations.FindAll().ToList().ForEach(Conversations.Add);

                // Then populate them with messages and events
                foreach (Message msg in messages.FindAll())
                    Conversations.Where(x => x.Id.Equals(msg.ConversationId)).First().AddItem(msg);
                foreach (ConversationEvent evt in events.FindAll())
                    Conversations.Where(x => x.Id.Equals(evt.ConversationId)).First().AddItem(evt);
            }
        }

        internal void ProcessConversationItem(IConversationItem item)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                Conversation conv = Conversations.Where(x => x.Id == item.ConversationId).First();

                // Update the db
                if (item.IsMessage)
                {
                    Message msg = item as Message;
                    var messages = db.GetCollection<Message>("messages");
                    if (messages.Exists(Query.EQ("_id", msg.Id)))
                        return;
                    messages.Insert(item as Message);
                }
                else
                {
                    ConversationEvent evt = item as ConversationEvent;

                    var events = db.GetCollection<ConversationEvent>("events");

                    if (events.Exists(Query.EQ("_id", evt.Id)))
                        return;
                    events.Insert(evt);

                    var conversations = db.GetCollection<Conversation>("conversations");
                    conversations.EnsureIndex(x => x.Id);
                    Conversation dbconv = conversations.FindOne(Query.EQ("_id", evt.ConversationId));

                    /* In case of an event, some property of the conversation must be updated
                     * (both in-memory and in the db) */
                    switch (evt.Type)
                    {
                        case EventType.NameChanged:
                            RunOnUI(new ThreadStart(() => { conv.Name = evt.Args["Name"] as string; }));

                            dbconv.Name = evt.Args["Name"] as string;
                            break;

                        case EventType.UserAdded:
                            var users = db.GetCollection<User>("users");
                            User user = evt.Args["User"] as User;

                            if (conv.Participants.Where(x => x.Id == user.Id).Count() > 0)
                                return;

                            RunOnUI(new ThreadStart(() => { conv.Participants.Add(user); }));

                            dbconv.Participants.Add(user);
                            break;

                        case EventType.UserLeft:
                            if (conv.Participants.Where(x => x.Id == evt.Sender.Id).Count() > 0)
                                RunOnUI(new ThreadStart(() => { conv.Participants.Remove(conv.Participants.Where(x => x.Id == evt.Sender.Id).First()); }));

                            if (dbconv.Participants.Where(x => x.Id == evt.Sender.Id).Count() > 0)
                                dbconv.Participants.Remove(dbconv.Participants.Where(x => x.Id == evt.Sender.Id).First());
                            break;
                    }

                    conversations.Update(dbconv);  // Save the changes
                }

                // Update the in-memory model
                RunOnUI(new ThreadStart(() => {
                    conv.AddItem(item);
                    ConversationItemEvent?.Invoke(this, new ConversationItemArgs(item));
                }));
            }
        }

        internal void ProcessConversation(Conversation conv, User creator, double time)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                // Update the in-memory model and the db
                var conversations = db.GetCollection<Conversation>("conversations");
                conversations.Insert(conv);

                RunOnUI(new ThreadStart(() => { Conversations.Add(conv); }));

                conversations.EnsureIndex(x => x.Id);

                // Create the 'creation' event
                ProcessConversationItem(new ConversationEvent(creator, time, conv.Id, EventType.ConversationCreated));
            }
        }

        // Mostly invoking events as it can be done from outside the class
        internal void ProcessLogIn(int localId)
        {
            LoggedIn = true;
            Local.Id = localId;
            using (var db = new LiteDatabase(DbPath))
            {
                var col = db.GetCollection<User>("users");
                col.EnsureIndex(x => x.Id);
                if (!col.Exists(Query.EQ("_id", localId)))
                    col.Insert(Local);
            }
            RunOnUI(new ThreadStart(() => { LogInSuccessfullEvent?.Invoke(this, new LogInSuccessfullEvent()); }));
        }


        internal void ProcessLogInFailed(string error)
        {
            RunOnUI(new ThreadStart(() => { LogInFailedEvent?.Invoke(this, new LogInFailedEvent(error)); }));
        }


        internal void ProcessSearchResults(List<User> results)
        {
            RunOnUI(new ThreadStart(() => { SearchResultEvent?.Invoke(this, new SearchResultEvent(results)); }));
        }

        internal void RaiseStateChangeEvent(ClientStateChangedEvent evt)
        {
            RunOnUI(new ThreadStart(() => { ClientStateChangedEvent?.Invoke(this, evt); }));
        }

        private void RunOnUI(ThreadStart action)
        {
            // The code that may update the UI needs to be run on the UI thread
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
