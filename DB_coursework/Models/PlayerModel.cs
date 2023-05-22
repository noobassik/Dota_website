using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Signers;

namespace DB_coursework.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public TeamModel Team { get; set; }
        public PlayerModel() { }
        public PlayerModel(int id, string name, string country) 
        {
            Id = id;
            Name = name;
            Country = country;
        }
        public PlayerModel(MySqlDataReader reader)
        {
            Id = int.Parse(reader.GetString("player_id"));
            Name = reader.GetString("name");
            Country = reader.GetString("country");
        }

        public PlayerModel(MySqlDataReader reader, int id)
        {
            Country = reader.GetString("country");
        }
    }
}
