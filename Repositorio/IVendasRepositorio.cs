using Intelectah.Models;
using Intelectah.ViewModel;

namespace Intelectah.Repositorio
{
    public interface IVendasRepositorio
    {
        VendasModel ListarPorId(int id);
        List<VendasModel> BuscarTodos();
        VendasModel Adicionar(VendasModel venda);
        VendasModel Atualizar(VendasModel venda);
        bool Apagar(int id);
        VendasModel ObterPorProtocolo(string protocoloVenda);
        bool VerificarProtocoloUnico(string protocoloVenda);
    }
}
