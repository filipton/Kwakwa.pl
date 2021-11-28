using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Npgsql;

namespace KwakwaPl.Data
{
    public static class NpgsqlConnect
    {
        public static NpgsqlConnection Connection;
        
        public static void Init()
        {
            Connection = new NpgsqlConnection(GetConnectionString());
            Connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(
                    "CREATE TABLE IF NOT EXISTS MessagesGroups (name varchar(16) NOT NULL,password varchar(16),passwordSecured BOOl,oneTimePassword BOOL,messagesJson TEXT);", Connection);
            cmd.ExecuteNonQuery();
            
            using (var reader = new NpgsqlCommand("SELECT * FROM MessagesGroups", Connection).ExecuteReader())
            {
                while (reader.Read())
                {
                    var hasRows = reader.HasRows;
                    if (hasRows)
                    {
                        List<Message> msg = reader.IsDBNull(4) ? new List<Message>() : JsonConvert.DeserializeObject<List<Message>>(reader.GetString(4));
                        MessageGroup messageGroup = new MessageGroup()
                        {
                            GroupName = reader.GetString(0),
                            Messages = msg,
                            OneTimePassword = reader.GetBoolean(3),
                            Password = reader.GetString(1),
                            PasswordSecured = reader.GetBoolean(2)
                        };
                        
                        MessagesContainer.Groups.Add(messageGroup);
                        Console.WriteLine($"{messageGroup.GroupName}: {messageGroup.Password}");
                    }   
                }
            }

            new Thread(async () =>
            {
                while (Connection.FullState == ConnectionState.Open)
                {
                    Thread.Sleep(60000);
                    foreach (MessageGroup messageGroup in MessagesContainer.Groups)
                    {
                        string json = JsonConvert.SerializeObject(messageGroup.Messages);
                        await NpgsqlConnect.ExecuteCommandWithoutReturn($"UPDATE MessagesGroups SET messagesJson = '{json}' WHERE name = '{messageGroup.GroupName}';");   
                    }
                }
            }).Start();
        }

        public static async Task ExecuteCommandWithoutReturn(string cmd)
        {
            await new NpgsqlCommand(cmd, Connection).ExecuteNonQueryAsync();
        }
        
        public static string GetConnectionString()
        {
            //IN PRODUCTION REMOVE STRING AFTER ??
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? "postgres://ibsbbkwoetewcs:a5f645b39e6d01691656e33ffc19d312744ea92c7adaa664f1bffd644a93a556@ec2-54-228-139-34.eu-west-1.compute.amazonaws.com:5432/dcm9heen6kfh08";
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };
            
            Console.WriteLine(builder.ToString());
            return builder.ToString();
        }
    }
}