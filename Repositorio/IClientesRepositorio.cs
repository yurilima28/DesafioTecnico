using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IClientesRepositorio
    {
        ClientesModel ListarPorId(int id);
        List<ClientesModel> BuscarTodos();
        ClientesModel Adicionar(ClientesModel cliente);
        ClientesModel Atualizar(ClientesModel cliente);
        bool Apagar(int id);
        ClientesModel ObterPorNome(string nomeCliente);
        bool VerificarNomeClienteUnico(string nomeCliente, int? clienteID = null);
        bool VerificarCpfUnico(string cpf, int? clienteID = null);
    }
}
