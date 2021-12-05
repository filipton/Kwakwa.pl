using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KwakwaPl.Data
{
	//TODO: Add bool for one time passwords (without cookies saving)
	
	public struct Group
	{
		public int Id;
		public string GroupName;
		public string Password;
		public bool OneTimePassword;
		
		public async Task AddMessage(string user, string msg)
		{
			await DBConnect.ExecuteCommandWithoutReturn($"INSERT INTO Messages (user, message, time, group_id) values ('{user}', '{msg}', {MicrosecondEpochConverter.DateTimeToEpoch(DateTime.UtcNow.AddHours(1))}, {Id});");
		}

		public async Task<List<Message>> GetMessages()
		{
			List<Message> tmpMsgs = new List<Message>();
			using (var reader = new MySqlCommand($"SELECT * FROM Messages WHERE group_id={Id};", DBConnect.Connection).ExecuteReader())
			{
				while (reader.Read())
				{
					var hasRows = reader.HasRows;
					if (hasRows)
					{
						Message msg = new Message()
						{
							Id = reader.GetInt32(0),
							User = reader.GetString(1),
							Msg = reader.GetString(2),
							Time = MicrosecondEpochConverter.EpochToDateTime(reader.GetInt32(3)),
							Group_Id = Id
						};
						tmpMsgs.Add(msg);
					}   
				}
			}

			return tmpMsgs;
		}
	}
	
	public struct Message
	{
		public BigInteger Id;
		public string User;
		public string Msg;
		public DateTime Time;
		public int Group_Id;
	}

	public class MicrosecondEpochConverter
	{
		private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static int DateTimeToEpoch(DateTime dt)
		{
			return (int)(dt - _epoch).TotalSeconds;
		}

		public static DateTime EpochToDateTime(int time)
		{
			return _epoch.AddSeconds(time);
		}
	}
}
