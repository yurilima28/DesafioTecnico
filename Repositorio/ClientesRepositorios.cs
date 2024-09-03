using Intelectah.Dapper;
using Microsoft.EntityFrameworkCore;
using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ClientesRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ClientesModel ListarPorId(int id)
        {
            return _bancoContext.Clientes.FirstOrDefault(c => c.ClienteID == id);
        }

        public List<ClientesModel> BuscarTodos()
        {
            return _bancoContext.Clientes.ToList();
        }

        public ClientesModel Adicionar(ClientesModel cliente)
        {
            _bancoContext.Clientes.Add(cliente);
            _bancoContext.SaveChanges();
            return cliente;
        }

        public async Task AdicionarAsync(ClientesModel cliente)
        {
            await _bancoContext.Clientes.AddAsync(cliente);
            await _bancoContext.SaveChangesAsync();
        }

        public ClientesModel Atualizar(ClientesModel cliente)
        {
            var clienteDb = ListarPorId(cliente.ClienteID);

            if (clienteDb == null) throw new Exception("Cliente não encontrado.");

            clienteDb.Nome = cliente.Nome;
            clienteDb.Telefone = cliente.Telefone;
            cliente.Email = cliente.Email;
            cliente.CPF = cliente.CPF;

            _bancoContext.Clientes.Update(clienteDb);
            _bancoContext.SaveChanges();
            return clienteDb;
        }

        public bool Apagar(int id)
        {
            var clienteDb = ListarPorId(id);

            if (clienteDb == null) throw new Exception("Cliente não encontrado.");

            _bancoContext.Clientes.Remove(clienteDb);
            _bancoContext.SaveChanges();
            return true;
        }

        public ClientesModel ObterPorNome(string nomeCliente)
        {
            return _bancoContext.Clientes
                .FirstOrDefault(c => c.Nome.ToLower() == nomeCliente.ToLower());
        }
    }
}
