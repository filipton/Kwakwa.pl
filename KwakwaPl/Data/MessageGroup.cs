namespace KwakwaPl.Data
{
	public struct MessageGroup
	{
		public string GroupName;
		public string Password;
		public List<Message> Messages;

		public void AddMessage(string username, string msg)
		{
			Messages.Add(new Message() { Msg = msg, UserName = username, Time = DateTime.UtcNow.AddHours(1) });
		}
	}
}
