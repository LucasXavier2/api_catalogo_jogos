using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCatalogoJogos.Entities;

namespace ApiCatalogoJogos.Repositories {
    public interface IGameRepository : IDisposable {
        Task<List<Game>> Get(int page, int quantity);
        Task<Game> Get(Guid id);
        Task<List<Game>> Get(string name, string company);
        Task Insert(Game game);
        Task Update(Game game);
        Task Remove(Guid id);
    }
}