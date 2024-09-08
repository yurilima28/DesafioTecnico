using Intelectah.Dapper;
using Intelectah.Models;
using Intelectah.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Intelectah.Repositorio
{
    public class VendasRepositorio : IVendasRepositorio
    {
        private readonly BancoContext _bancoContext;

        public VendasRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public VendasModel ListarPorId(int id)
        {
            return _bancoContext.Vendas
                            .Include(v => v.Cliente)
                            .Include(v => v.Usuario)
                            .Include(v => v.Concessionaria)
                            .Include(v => v.Fabricante)
                            .Include(v => v.Veiculo)
                            .FirstOrDefault(v => v.VendaId == id);
        }

        public List<VendasModel> BuscarTodos()
        {
            return _bancoContext.Vendas
                          .Include(v => v.Cliente) // Carregar Cliente
                          .Include(v => v.Usuario) // Carregar Usuario
                          .Include(v => v.Concessionaria) // Carregar Concessionaria
                          .Include(v => v.Fabricante) // Carregar Fabricante
                          .Include(v => v.Veiculo) // Carregar Veiculo
                          .ToList();
        }

        public VendasModel Adicionar(VendasModel venda)
        {
            _bancoContext.Vendas.Add(venda);
            _bancoContext.SaveChanges();
            return venda;
        }

        public VendasModel Atualizar(VendasModel venda)
        {
            _bancoContext.Vendas.Update(venda);
            _bancoContext.SaveChanges();
            return venda;
        }

        public bool Apagar(int id)
        {
            var venda = ListarPorId(id);
            if (venda != null)
            {
                _bancoContext.Vendas.Remove(venda);
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public VendasModel ObterPorProtocolo(string protocoloVenda)
        {
            return _bancoContext.Vendas.FirstOrDefault(v => v.ProtocoloVenda == protocoloVenda);
        }

        public bool VerificarProtocoloUnico(string protocoloVenda)
        {
            return !_bancoContext.Vendas.Any(v => v.ProtocoloVenda == protocoloVenda);
        }
    }
}
