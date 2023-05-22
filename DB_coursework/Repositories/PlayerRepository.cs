using DB_coursework.Models;
using DB_coursework.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace DB_coursework.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MySqlConnection _connection;
        public PlayerRepository(IOptions<RepositoryOptions> options)
        {
            _connection = new MySqlConnection(options.Value.ConnectionString);
        }
        public void AddPlayer(PlayerModel player)
        {
            _connection.Open();
            int team_id = (int)new MySqlCommand($"SELECT team_id FROM teams WHERE name LIKE '{player.Team.Name}'", _connection).ExecuteScalar();
            _connection.Close();
            _connection.Open();
            System.Console.WriteLine($"INSERT INTO players (name, country, teams_team_id) VALUES ('{player.Name}','{player.Country}',{team_id})");
            new MySqlCommand($"INSERT INTO players (name, country, teams_team_id) VALUES ('{player.Name}','{player.Country}', {team_id})", _connection).ExecuteNonQuery();
            _connection.Close();
        }

        public void DeletePlayer(int id)
        {
            _connection.Open();
            new MySqlCommand($"DELETE FROM players WHERE player_id = {id}", _connection).ExecuteNonQuery();
            _connection.Close();
        }
        public IEnumerable<PlayerModel> GetAllPlayers()
        {
            var players = new List<PlayerModel>();
            _connection.Open();
            MySqlDataReader reader = new MySqlCommand("SELECT * FROM players", _connection).ExecuteReader();
            while (reader.Read())
            {
                players.Add(new PlayerModel(reader));
            }
            _connection.Close();

            foreach (PlayerModel player in players) 
            {
                var team = new TeamModel();
                _connection.Open();
                MySqlDataReader reader2 = new MySqlCommand($"SELECT * FROM teams WHERE team_id = (SELECT players.teams_team_id FROM players WHERE player_id = {player.Id})", _connection).ExecuteReader();
                if (reader2.Read())
                {
                    team = new TeamModel(reader2);
                }
                _connection.Close();

                player.Team = team;
            }
            _connection.Close();

            return players;
        }
        public PlayerModel GetPlayerById(int id)
        {
            var player = new PlayerModel();
            string team_id = "";
            _connection.Open();
            MySqlDataReader reader = new MySqlCommand($"SELECT * FROM players WHERE player_id = {id}", _connection).ExecuteReader();
            if (reader.Read())
            {
                team_id = reader.GetString("teams_team_id");
                player = new PlayerModel(reader);
            }
            _connection.Close();

            var team = new TeamModel();
            _connection.Open();
            MySqlDataReader reader2 = new MySqlCommand($"SELECT * FROM teams WHERE team_id = {team_id}", _connection).ExecuteReader();
            if (reader2.Read())
            {
                team = new TeamModel(reader2);
            }
            _connection.Close();

            player.Team = team;

            return player;
        }

        public void UpdatePlayer(PlayerModel player)
        {
            _connection.Open();
            new MySqlCommand($"UPDATE players SET name = '{player.Name}', country = '{player.Country}' WHERE player_id = {player.Id}", _connection).ExecuteNonQuery();
            _connection.Close();
        }

        public PlayerModel GetMostPopularCountry()
        {
            _connection.Open();
            MySqlDataReader reader = new MySqlCommand("SELECT country FROM players GROUP BY country ORDER BY COUNT(country) DESC LIMIT  1", _connection).ExecuteReader();

            if (reader.Read())
            {
                var country = new PlayerModel(reader, 0);
                _connection.Close();
                return country;
            }
            throw new Exception();
        }
    }
}
