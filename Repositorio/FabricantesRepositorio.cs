﻿using Intelectah.Dapper;
using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public class FabricantesRepositorio : IFabricantesRepositorio
    {
        private readonly BancoContext _bancoContext;
        public FabricantesRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public FabricantesModel ListarPorId(int Id)
        {
            return _bancoContext.Fabricantes.FirstOrDefault(x => x.FabricanteID == Id);
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
            {
                fabricanteDB.NomeFabricante = fabricante.NomeFabricante;
                fabricanteDB.PaisOrigem = fabricante.PaisOrigem;
                fabricanteDB.AnoFundacao = fabricante.AnoFundacao;

                _bancoContext.Fabricantes.Update(fabricanteDB);
                _bancoContext.SaveChanges();
                return fabricanteDB;
            }
        }

        public bool Apagar(int Id)
        {
            FabricantesModel fabricanteDB = ListarPorId(Id);

            if (fabricanteDB == null) throw new Exception("Houve um erro ao apagar o fabricante");

            _bancoContext.Fabricantes.Remove(fabricanteDB);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
