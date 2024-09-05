using Intelectah.Dapper;
using Intelectah.Models;
using Microsoft.EntityFrameworkCore;

namespace Intelectah.Repositorio
{
    public class VeiculosRepositorio : IVeiculosRepositorio
    {
        private readonly BancoContext _bancoContext;

        public VeiculosRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public VeiculosModel ListarPorId(int Id)
        {
            return _bancoContext.Veiculos.FirstOrDefault(x => x.VeiculoID == Id);
        }
        public List<VeiculosModel> BuscarTodos()
        {
             return _bancoContext.Veiculos.ToList();
        }
        public VeiculosModel Adicionar(VeiculosModel veiculo)
        {
            var fabricante = _bancoContext.Fabricantes.Find(veiculo.FabricanteID);
            if (fabricante == null)
            {
                throw new ArgumentException($"Fabricante com ID {veiculo.FabricanteID} não encontrado.");
            }

            _bancoContext.Veiculos.Add(veiculo);
            _bancoContext.SaveChanges();

            return veiculo;
        }
        public VeiculosModel Atualizar(VeiculosModel veiculo)
        {
            VeiculosModel veiculoDB = ListarPorId(veiculo.VeiculoID);

            if (veiculoDB == null) throw new Exception("Houve um erro ao atualizar o veículo");
            {
                veiculoDB.ModeloVeiculo = veiculo.ModeloVeiculo;
                veiculoDB.AnoFabricacao = veiculo.AnoFabricacao;
                veiculoDB.ValorVeiculo = veiculo.ValorVeiculo;
                veiculoDB.FabricanteID = veiculo.FabricanteID;
                veiculoDB.Tipo = veiculo.Tipo;
                veiculoDB.Descricao = veiculo.Descricao;

                _bancoContext.Veiculos.Update(veiculoDB);
                _bancoContext.SaveChanges();
                return veiculoDB;
            }
        }
        public List<VeiculosModel> BuscarPorFabricante(int fabricanteId)
        {
            if (fabricanteId < 0) throw new Exception("");
            return _bancoContext.Veiculos.Where(v => v.FabricanteID == fabricanteId).ToList();
        }
        
        public bool Apagar(int Id)
        {
            VeiculosModel veiculoDB = ListarPorId(Id);

            if (veiculoDB == null) throw new Exception("Houve um erro ao apagar o fabricante");

            _bancoContext.Veiculos.Remove(veiculoDB);
            _bancoContext.SaveChanges();
            return true;
        }

        public bool VerificarSeVeiculoVendido(int veiculoId)
        {
            return _bancoContext.Vendas.Any(v => v.VeiculoID == veiculoId);
        }
    }
}
