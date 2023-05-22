using DB_coursework.Models;
using System.Collections.Generic;

namespace DB_coursework.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        void AddPlayer(PlayerModel player);
        void UpdatePlayer(PlayerModel player);
        void DeletePlayer(int id);
        PlayerModel GetPlayerById(int id);
        IEnumerable<PlayerModel> GetAllPlayers();
        PlayerModel GetMostPopularCountry();
    }
}
