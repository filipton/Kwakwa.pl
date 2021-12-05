using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

/*
 * DB SCHEMA: https://app.dbdesigner.net/designer/schema/479546
 */

namespace KwakwaPl.Data
{
    public static class DBConnect
    {
        //public static NpgsqlConnection Connection;
        public static MySqlConnection Connection;
        
        public static void Init()
        {
            try
            {
                Connection = new MySqlConnection(Environment.GetEnvironmentVariable("PSCALE_DATABASE_URL"));
                Connection.Open();

                using (var reader = new MySqlCommand("SELECT * FROM Groups", Connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hasRows = reader.HasRows;
                        if (hasRows)
                        {
                            Group messageGroup = new Group()
                            {
                                Id = reader.GetInt32(0),
                                GroupName = reader.GetString(1),
                                Password = reader.GetString(2),
                                OneTimePassword = reader.GetBoolean(3),
                            };
                            
                            MessagesContainer.Groups.Add(messageGroup);
                            Console.WriteLine($"{messageGroup.GroupName}: {messageGroup.Password}");
                        }   
                    }
                }
            }
            catch
            {
                Console.WriteLine("SQL INIT ERROR!!");
            }
        }

        public static async Task ExecuteCommandWithoutReturn(string cmd)
        {
            try
            {
                await new MySqlCommand(cmd, Connection).ExecuteNonQueryAsync();
            }
            catch
            {
                Console.WriteLine("SQL COMMAND ERROR!");
            }
        }

        public static async Task<int> ExecuteCommandAndReturnId(string cmd)
        {
            try
            {
                MySqlCommand mcmd = new MySqlCommand(cmd, Connection);
                await mcmd.ExecuteNonQueryAsync();
                return (int)mcmd.LastInsertedId;
            }
            catch
            {
                Console.WriteLine("SQL COMMAND ERROR!");
            }

            return -1;
        }
    }
}