using LiteDB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace LCP_API
{
    static class CommandReceiver
    {
        public static void Process(LCPClient client, XDocument xml)
        {
            XElement command = xml.Root;
            string type = command.Name.ToString();

            switch (type)
            {
                case "Response":
                    if ((command.Attribute("Command").Value.Equals("LogIn") || command.Attribute("Command").Value.Equals("SignUp"))
                                && Boolean.Parse(command.Attribute("Success").Value))
                        client.ProcessLogIn(int.Parse(command.Attribute("Id").Value));
                    break;

                case "Error":
                    if (command.Attribute("Command").Value.Equals("LogIn"))
                    {
                        var reader1 = command.CreateReader();
                        reader1.MoveToContent();
                        string error = reader1.ReadInnerXml();
                        client.ProcessLogInFailed(error);
                    }
                    else
                    {
                        var reader3 = command.CreateReader();
                        reader3.MoveToContent();
                        string error = reader3.ReadInnerXml();
                        Console.WriteLine("Error ! " + error);
                    }
                    break;

                case "Message":
                    if (int.Parse(command.Attribute("Sender").Value).Equals(client.Local.Id))
                        break;

                    var reader2 = command.CreateReader();
                    reader2.MoveToContent();
                    string message = reader2.ReadInnerXml();

                    using (var db = new LiteDatabase(client.DbPath))
                    {
                        var users = db.GetCollection<User>("users");
                        User sender = users.FindOne(Query.EQ("_id", int.Parse(command.Attribute("Sender").Value)));
                        TextMessage msg = new TextMessage(message, sender, double.Parse(command.Attribute("Time").Value, CultureInfo.InvariantCulture), command.Attribute("Conversation").Value, sender.Id == client.Local.Id);
                        client.ProcessConversationItem(msg);
                    }
                    break;

                case "NewConversation":
                    using (var db = new LiteDatabase(client.DbPath))
                    {
                        var conversations = db.GetCollection<Conversation>("conversations");
                        if (conversations.Exists(Query.EQ("_id", command.Attribute("Id").Value)))
                            break;

                        var users = db.GetCollection<User>("users");

                        List<User> participants = new List<User>();
                        byte[] image = null;
                        foreach (XElement child in command.Elements())
                        {
                            if (child.Name.LocalName.Equals("Image"))
                            {
                                var reader3 = child.CreateReader();
                                reader3.MoveToContent();
                                image = Convert.FromBase64String(reader3.ReadInnerXml());
                            }
                            else if (child.Name.LocalName.Equals("Participant"))
                            {
                                User user;
                                int id = int.Parse(child.Attribute("Id").Value);
                                if (users.Exists(Query.EQ("_id", id)))
                                    user = users.FindOne(Query.EQ("_id", id));
                                else
                                {
                                    user = new User(id, child.Attribute("Username").Value);
                                    users.Insert(user);
                                }
                                participants.Add(user);
                            }
                        }

                        User creator;
                        int creator_id = int.Parse(command.Attribute("Creator").Value);
                        if (users.Exists(Query.EQ("_id", creator_id)))
                            creator = users.FindOne(Query.EQ("_id", creator_id));
                        else
                        {
                            creator = new User(creator_id, command.Attribute("C_Username").Value);
                            users.Insert(creator);
                        }

                        Conversation conv = new Conversation(command.Attribute("Id").Value,
                            command.Attribute("Name").Value, participants, image);
                        client.ProcessConversation(conv, creator, double.Parse(command.Attribute("Time").Value, CultureInfo.InvariantCulture));
                    }
                    break;

                case "Search":
                    List<User> results = new List<User>();
                    foreach (XElement child in command.Elements())
                        results.Add(new User(int.Parse(child.Attribute("Id").Value), child.Attribute("Username").Value));
                    client.ProcessSearchResults(results);
                    break;

                case "UserLeft":

                    using (var db = new LiteDatabase(client.DbPath))
                    {
                        var users = db.GetCollection<User>("users");
                        User user = users.FindOne(Query.EQ("_id", int.Parse(command.Attribute("User").Value)));

                        ConversationEvent evt = new ConversationEvent(user, double.Parse(command.Attribute("Time").Value, CultureInfo.InvariantCulture), command.Attribute("Conversation").Value, EventType.UserLeft);
                        client.ProcessConversationItem(evt);

                    }
                    break;


                case "ChangeName":
                    using (var db = new LiteDatabase(client.DbPath))
                    {
                        var users = db.GetCollection<User>("users");
                        User user = users.FindOne(Query.EQ("_id", int.Parse(command.Attribute("User").Value)));

                        var reader3 = command.CreateReader();
                        reader3.MoveToContent();
                        string name = reader3.ReadInnerXml();

                        ConversationEvent evt = new ConversationEvent(user, double.Parse(command.Attribute("Time").Value, CultureInfo.InvariantCulture), command.Attribute("Conversation").Value, EventType.NameChanged);
                        evt.Args.Add("Name", name);
                        client.ProcessConversationItem(evt);
                    }
                    break;

                case "Add":
                    using (var db = new LiteDatabase(client.DbPath))
                    {
                        var users = db.GetCollection<User>("users");

                        User user;
                        int id = int.Parse(command.Attribute("Id").Value);
                        if (users.Exists(Query.EQ("_id", id)))
                            user = users.FindOne(Query.EQ("_id", id));
                        else
                        {
                            user = new User(id, command.Attribute("Username").Value);
                            users.Insert(user);
                        }

                        User addedby = users.FindOne(Query.EQ("_id", int.Parse(command.Attribute("AddedBy").Value)));

                        ConversationEvent evt = new ConversationEvent(addedby, double.Parse(command.Attribute("Time").Value, CultureInfo.InvariantCulture), command.Attribute("Conversation").Value, EventType.UserAdded);
                        evt.Args["User"] = user;
                        client.ProcessConversationItem(evt);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
