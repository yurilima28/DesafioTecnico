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

        public ConcessionariasModel ListarPorId(int id)
        {
            return _bancoContext.Concessionarias.Find(id);
        }

        public List<ConcessionariasModel> BuscarTodos()
        {
            return _bancoContext.Concessionarias.ToList();
        }

        public ConcessionariasModel Adicionar(ConcessionariasModel concessionaria)
        {
            _bancoContext.Concessionarias.Add(concessionaria);
            _bancoContext.SaveChanges();
            return concessionaria; 
        }

        public ConcessionariasModel Atualizar(ConcessionariasModel concessionaria)
        {
            var existente = _bancoContext.Concessionarias.Find(concessionaria.ConcessionariaID);
            if (existente != null)
            {
                _bancoContext.Entry(existente).CurrentValues.SetValues(concessionaria);
                _bancoContext.SaveChanges();
                return concessionaria; 
            }
            return null; 
        }

        public bool Apagar(int id)
        {
            var concessionaria = _bancoContext.Concessionarias.Find(id);
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
            return _bancoContext.Concessionarias.FirstOrDefault(c => c.Nome.ToLower() == nomeLower);
        }

        public bool VerificarNomeConcessionariaUnico(string nomeConcessionaria)
        {
            var nomeMinusc = nomeConcessionaria.ToLower();
            return !_bancoContext.Concessionarias.Any(c => c.Nome.ToLower() == nomeMinusc);
        }
     
    }
}
