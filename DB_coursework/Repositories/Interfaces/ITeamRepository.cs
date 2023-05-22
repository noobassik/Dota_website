using DB_coursework.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Collections.Generic;

namespace DB_coursework.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        void AddTeam(TeamModel team);
        void UpdateTeam(TeamModel team);
        void DeleteTeam(int id);
        TeamModel GetTeamById(int id);
        IEnumerable<TeamModel> GetAllTeams();
        TeamModel GetMostPopularRegion();
    }
}
