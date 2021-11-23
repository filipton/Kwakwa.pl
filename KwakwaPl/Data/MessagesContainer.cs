using System.Collections.Generic;

namespace KwakwaPl.Data
{
    public class MessagesContainer
    {
        public static List<MessageGroup> Groups = new List<MessageGroup>()
        {
            new MessageGroup() { GroupName = "Global", Messages = new List<Message>() },
            new MessageGroup() { GroupName = "Restricted", Messages = new List<Message>(), Password = "kwakwa5.pl" }
        };
    }
}