using DB_coursework.Models;
using DB_coursework.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DB_coursework.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly MySqlConnection _connection;

        public TeamRepository(IOptions<RepositoryOptions> options)
        {
            _connection = new MySqlConnection(options.Value.ConnectionString);
        }

        public void AddTeam(TeamModel team)
        {
            _connection.Open();
            new MySqlCommand($"INSERT INTO teams (name, region) VALUES ('{team.Name}','{team.Region}')", _connection).ExecuteNonQuery();
            _connection.Close();
        }

        public void DeleteTeam(int id)
        {
            _connection.Open();
            new MySqlCommand($"DELETE FROM teams WHERE team_id = {id}", _connection).ExecuteNonQuery();
            _connection.Close();    
        }

        public IEnumerable<TeamModel> GetAllTeams()
        {
            var teams = new List<TeamModel>();
            _connection.Open();
            MySqlDataReader reader = new MySqlCommand("SELECT * FROM teams", _connection).ExecuteReader();
            while (reader.Read())
            {
                teams.Add(new TeamModel(reader));
            }
            _connection.Close();
            return teams;
        }

        public TeamModel GetTeamById(int id)
        {
            var team = new TeamModel();
            _connection.Open();
            MySqlDataReader reader = new MySqlCommand($"SELECT * FROM teams WHERE team_id = {id}", _connection).ExecuteReader();
            if (reader.Read())
            {
                team = new TeamModel(reader);
            }
            _connection.Close();
            return team;
        }

        public void UpdateTeam(TeamModel team)
        {
            _connection.Open();
            new MySqlCommand($"UPDATE teams SET name = '{team.Name}', region = '{team.Region}' WHERE team_id = {team.Id}", _connection).ExecuteNonQuery();
            _connection.Close();
        }

        public TeamModel GetMostPopularRegion()
        {
            _connection.Open();
            MySqlDataReader reader = new MySqlCommand("SELECT region FROM teams GROUP BY region ORDER BY COUNT(region) DESC LIMIT  1", _connection).ExecuteReader();

            if (reader.Read())
            {
                var region = new TeamModel(reader, 1);
                _connection.Close();
                return region;
            }
            throw new Exception();
        }
    }
}
