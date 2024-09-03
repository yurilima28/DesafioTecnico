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

        public VendasModel ListarPorId(int vendaId)
        {
            return _bancoContext.Vendas
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .Include(v => v.Concessionaria)
                .FirstOrDefault(x => x.VendaId == vendaId);
        }

        public List<VendasModel> BuscarTodas()
        {
            return _bancoContext.Vendas
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .Include(v => v.Concessionaria)
                .ToList();
        }

        public VendasModel Adicionar(VendasModel venda)
        {
            _bancoContext.Vendas.Add(venda);
            _bancoContext.SaveChanges();
            return venda;
        }

        public async Task AdicionarAsync(VendasModel venda)
        {
            await _bancoContext.Vendas.AddAsync(venda);
            await _bancoContext.SaveChangesAsync();
        }

        public VendasModel Atualizar(VendasModel venda)
        {
            VendasModel vendaDB = ListarPorId(venda.VendaId);

            if (vendaDB == null) throw new Exception("Houve um erro ao atualizar a venda.");

            vendaDB.ClienteID = venda.ClienteID;
            vendaDB.DataVenda = venda.DataVenda;
            vendaDB.ValorTotal = venda.ValorTotal;
            vendaDB.UsuarioID = venda.UsuarioID;
            vendaDB.ConcessionariaID = venda.ConcessionariaID;

            _bancoContext.Vendas.Update(vendaDB);
            _bancoContext.SaveChanges();
            return vendaDB;
        }

        public bool Apagar(int vendaId)
        {
            VendasModel vendaDB = ListarPorId(vendaId);

            if (vendaDB == null) throw new Exception("Houve um erro ao apagar a venda.");

            _bancoContext.Vendas.Remove(vendaDB);
            _bancoContext.SaveChanges();
            return true;
        }

        public VendasModel ObterPorCliente(int clienteId)
        {
            return _bancoContext.Vendas
                .Include(v => v.Cliente)
                .FirstOrDefault(v => v.ClienteID == clienteId);
        }
    }
}
