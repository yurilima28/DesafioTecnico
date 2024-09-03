using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IClientesRepositorio
    {
        ClientesModel ListarPorId(int id);
        List<ClientesModel> BuscarTodos();
        ClientesModel Adicionar(ClientesModel cliente);
        Task AdicionarAsync(ClientesModel cliente);
        ClientesModel Atualizar(ClientesModel cliente);
        bool Apagar(int id);
        ClientesModel ObterPorNome(string nomeCliente);
    }
}
