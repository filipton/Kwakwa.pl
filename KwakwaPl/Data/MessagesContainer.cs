using System.Collections.Generic;

namespace KwakwaPl.Data
{
    public static class MessagesContainer
    {
        public static List<MessageGroup> Groups = new List<MessageGroup>()
        {
            new MessageGroup() { GroupName = "Global", Messages = new List<Message>() },
            new MessageGroup() { GroupName = "Restricted", Messages = new List<Message>(), Password = "kwakwa5.pl", PasswordSecured = true},
            new MessageGroup() { GroupName = "Test", Messages = new List<Message>(), Password = "Test123+", PasswordSecured = true, OneTimePassword = true}
        };
    }
}