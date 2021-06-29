using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.Entities;

namespace ApiCatalogoJogos.Repositories {
    public class GameRepository : IGameRepository {

        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>() {
            {Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), new Game {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Name = "Valorant",
                Company = "Riot Games",
                Price = 0.0
            }}, 
            {Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"), new Game {
                Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                Name = "Hades",
                Company = "Supergiant Games",
                Price = 33.24
            }}
        };

        public Task<List<Game>> Get(int page, int quantity) {
            return Task.FromResult(games.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task<Game> Get(Guid id) {
            if (!games.ContainsKey(id)) {
                return null;
            }

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Get(string name, string company) {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Company.Equals(company)).ToList());
        }

        public Task Insert(Game game) {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Update(Game game) {
            games[game.Id] = game;
            return Task.CompletedTask;
        }
        public Task Remove(Guid id) {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose() {
            // throw new NotImplementedException();
        }

    }
}