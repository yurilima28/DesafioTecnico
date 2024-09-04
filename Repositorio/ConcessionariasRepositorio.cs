using Intelectah.Dapper;
using Intelectah.Models;
using Microsoft.EntityFrameworkCore;

namespace Intelectah.Repositorio
{
    public class ConcessionariasRepositorio : IConcessionariasRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ConcessionariasRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<IEnumerable<ConcessionariasModel>> ListarTodosAsync()
        {
            return await _bancoContext.Concessionarias.ToListAsync();
        }

        public async Task<ConcessionariasModel> ListarPorIdAsync(int Id)
        {
            return await _bancoContext.Concessionarias.FindAsync(Id);
        }

        public async Task AdicionarAsync(ConcessionariasModel concessionaria)
        {
            _bancoContext.Concessionarias.Add(concessionaria);
            await _bancoContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ConcessionariasModel concessionaria)
        {
            _bancoContext.Concessionarias.Update(concessionaria);
            await _bancoContext.SaveChangesAsync();
        }

        public bool Apagar(int Id)
        {
            var concessionaria = _bancoContext.Concessionarias.Find(Id);
            if (concessionaria == null)
            {
                return false;
            }

            _bancoContext.Concessionarias.Remove(concessionaria);
            _bancoContext.SaveChanges();
            return true;
        }

        public ConcessionariasModel ObterPorNome(string nomeConcessionaria)
        {
            var nomeLower = nomeConcessionaria.ToLower();
            return _bancoContext.Concessionarias
                .FirstOrDefault(c => c.Nome.ToLower() == nomeLower);
        }


    }
}
