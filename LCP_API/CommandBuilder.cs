using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LCP_API
{
    static class CommandBuilder
    {
        /* This class provides method to build the commands sent to the server
         * /!\ Do not use directly, use LCPClient instead 
         * See the file CommandModel.xml for exemples */

        public static XDocument SignUp(string username, string name, string password)
        {
            return Build(new XElement("SignUp",
                    new XAttribute("Username", username),
                    new XAttribute("Name", username),
                    new XAttribute("Password", password)));
        }

        public static XDocument LogIn(string username, string password, double last_connection)
        {
            return Build(new XElement("LogIn",
                    new XAttribute("Username", username),
                    new XAttribute("Password", password),
                    new XAttribute("Time", last_connection)));
        }

        public static XDocument Message(string convId, string message, double time)
        {
            return Build(new XElement("Message",
                    new XAttribute("Conversation", convId),
                    new XAttribute("Time", time),
                    new XText(message)));
        }

        public static XDocument CreateConversation(string name, List<User> participants, byte[] image)
        {
            XElement command = new XElement("NewConversation",
                    new XAttribute("Name", name));
            foreach (User participant in participants)
                command.Add(new XElement("Participant",
                    new XAttribute("Id", participant.Id)));
            if (image != null)
            {
                XElement img = new XElement("Image",
                    new XText(Convert.ToBase64String(image)));
                command.Add(img);
            }
            return Build(command);
        }

        public static XDocument Search(string request)
        {
            return Build(new XElement("Search",
                    new XText(request)));
        }

        public static XDocument Leave(Conversation conv)
        {
            return Build(new XElement("Leave",
                    new XAttribute("Conversation", conv.Id)));
        }

        public static XDocument ChangeName(Conversation conv, string name)
        {
            return Build(new XElement("ChangeName",
                    new XAttribute("Conversation", conv.Id),
                    new XText(name)));
        }

        public static XDocument Add(Conversation conv, User user)
        {
            return Build(new XElement("Add",
                    new XAttribute("Conversation", conv.Id),
                    new XAttribute("User", user.Id)));
        }


        private static XDocument Build(XElement command)
        {
            XDocument doc = new XDocument(
                                        new XDeclaration("1.0", "UTF-8", null),
                                        command);
            return doc;
        }
    }
}
