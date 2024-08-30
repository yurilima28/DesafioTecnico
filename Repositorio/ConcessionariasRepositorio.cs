﻿using Intelectah.Dapper;
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

        public async Task<ConcessionariasModel> ListarPorIdAsync(int id)
        {
            return await _bancoContext.Concessionarias.FindAsync(id);
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

        public async Task RemoverAsync(int id)
        {
            var concessionaria = await _bancoContext.Concessionarias.FindAsync(id);
            if (concessionaria != null)
            {
                _bancoContext.Concessionarias.Remove(concessionaria);
                await _bancoContext.SaveChangesAsync();
            }
        }
    }
}
