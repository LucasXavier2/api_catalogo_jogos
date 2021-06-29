using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.ViewModel;

namespace ApiCatalogoJogos.Services {
    public class GameService : IGameService {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository) {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Get(int page, int quantity) {
            var games = await _gameRepository.Get(page, quantity);
            return games.Select(game => 
                new GameViewModel {
                    Id = game.Id,
                    Name = game.Name,
                    Company = game.Company,
                    Price = game.Price
                }
            ).ToList();
        }

        public async Task<GameViewModel> Get(Guid id) {
            var game = await _gameRepository.Get(id);
            
            if (game == null) {
                return null;
            }

            return new GameViewModel {
                Id = game.Id,
                Name = game.Name,
                Company = game.Company,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game) {
            var gameEntity = await _gameRepository.Get(game.Name, game.Company);

            if (gameEntity.Count > 0) {
                throw new RegisteredGameException();
            }

            var gameToInsert = new Game {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Company = game.Company,
                Price = game.Price
            };

            await _gameRepository.Insert(gameToInsert);

            return new GameViewModel {
                Id = gameToInsert.Id,
                Name = gameToInsert.Name,
                Company = gameToInsert.Company,
                Price = gameToInsert.Price
            };
        }

        public async Task Remove(Guid id) {
            var game = await _gameRepository.Get(id);

            if (game == null) {
                throw new NotRegisteredGameException();
            }

            await _gameRepository.Remove(id);
        }

        public async Task Update(Guid id, GameInputModel game) {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null) {
                throw new NotRegisteredGameException();
            }

            gameEntity.Name = game.Name;
            gameEntity.Company = game.Company;
            gameEntity.Price = game.Price;

            await _gameRepository.Update(gameEntity);
        }

        public async Task Update(Guid id, double price) {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null) {
                throw new NotRegisteredGameException();
            }

            gameEntity.Price = price;
            await _gameRepository.Update(gameEntity);
        }

        public void Dispose() {
            _gameRepository?.Dispose();
        }

    }
}