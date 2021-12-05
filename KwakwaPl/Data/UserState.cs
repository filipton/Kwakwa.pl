using System;
using System.Collections.Generic;

namespace KwakwaPl.Data
{
    public class UserState
    {
        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }
        public Dictionary<int, string> GroupPasswords = new Dictionary<int, string>();

        public bool IsAuthorizedToGroup(int gid)
        {
            return string.IsNullOrWhiteSpace(MessagesContainer.Groups[gid].Password) || (GroupPasswords.ContainsKey(gid) && MessagesContainer.Groups[gid].Password == GroupPasswords[gid]);
        }
    }
}