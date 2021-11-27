using System;
using System.Collections.Generic;

namespace KwakwaPl.Data
{
	//TODO: Add bool for one time passwords (without cookies saving)
	
	public struct MessageGroup
	{
		public string GroupName;
		public string Password;
		public bool PasswordSecured;
		public bool OneTimePassword;
		public List<Message> Messages;

		public void AddMessage(string username, string msg)
		{
			Messages.Add(new Message() { Msg = msg, UserName = username, Time = DateTime.UtcNow.AddHours(1) });
		}
	}
	
	public struct Message
	{
		public DateTime Time;
		public string UserName;
		public string Msg;
	}
}
