using Intelectah.Dapper;
using Intelectah.Models;
using Microsoft.EntityFrameworkCore;

namespace Intelectah.Repositorio
{
    public class FabricantesRepositorio : IFabricantesRepositorio
    {
        private readonly BancoContext _bancoContext;
        public FabricantesRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public FabricantesModel ListarPorId(int id)
        {
            return _bancoContext.Fabricantes.FirstOrDefault(x => x.FabricanteID == id);
        }
        public List<FabricantesModel> BuscarTodos()
        {
            return _bancoContext.Fabricantes.ToList();
        }

        public FabricantesModel Adicionar(FabricantesModel fabricante)
        {
            _bancoContext.Fabricantes.Add(fabricante);
            _bancoContext.SaveChanges();
            return fabricante;
        }

        public FabricantesModel Atualizar(FabricantesModel fabricante)
        {
            FabricantesModel fabricanteDB = ListarPorId(fabricante.FabricanteID);

            if (fabricanteDB == null) throw new Exception("Houve um erro ao atualizar o fabricante");

            fabricanteDB.NomeFabricante = fabricante.NomeFabricante;
            fabricanteDB.PaisOrigem = fabricante.PaisOrigem;
            fabricanteDB.AnoFundacao = fabricante.AnoFundacao;
            fabricanteDB.URL = fabricante.URL;

            _bancoContext.Fabricantes.Update(fabricanteDB);
            _bancoContext.SaveChanges();
            return fabricanteDB;

        }

        public bool Apagar(int id)
        {
            FabricantesModel fabricanteDB = ListarPorId(id);

            if (fabricanteDB == null) throw new Exception("Houve um erro ao apagar o fabricante");

            _bancoContext.Fabricantes.Remove(fabricanteDB);
            _bancoContext.SaveChanges();
            return true;
        }

        public FabricantesModel ObterPorNome(string nomeFabricante)
        {
            return _bancoContext.Fabricantes.FirstOrDefault(f => f.NomeFabricante.ToLower() == nomeFabricante.ToLower());
        }
        public bool VerificarNomeFabricanteUnico(string nomeFabricante)
        {
            return !_bancoContext.Fabricantes.Any(f => f.NomeFabricante.ToLower() == nomeFabricante.ToLower());
        }
    }
}
