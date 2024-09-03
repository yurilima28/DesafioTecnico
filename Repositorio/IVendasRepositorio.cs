using Intelectah.Models;
using Intelectah.ViewModel;

namespace Intelectah.Repositorio
{
    public interface IVendasRepositorio
    {
        VendasModel ListarPorId(int vendaId);
        List<VendasModel> BuscarTodas();
        VendasModel Adicionar(VendasModel venda);
        Task AdicionarAsync(VendasModel venda);
        VendasModel Atualizar(VendasModel venda);
        bool Apagar(int vendaId);
        VendasModel ObterPorCliente(int clienteId);
    }
}
