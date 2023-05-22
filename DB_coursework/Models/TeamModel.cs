using MySql.Data.MySqlClient;
using System;

namespace DB_coursework.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }

        public TeamModel() { }
        public TeamModel(string name, string region)
        {
            Name = name;
            Region = region;
        }

        public TeamModel(MySqlDataReader reader)
        {
            Id = int.Parse(reader.GetString("team_id"));
            Name = reader.GetString("name");
            Region = reader.GetString("region");
        }
        
        public TeamModel(MySqlDataReader reader, int id)
        {
            Region = reader.GetString("region");
        }
    }
}
