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
            return _bancoContext.Clientes.Find(id);
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

        public ClientesModel Atualizar(ClientesModel cliente)
        {
            _bancoContext.Clientes.Update(cliente);
            _bancoContext.SaveChanges();
            return cliente;
        }

        public bool Apagar(int id)
        {
            var cliente = _bancoContext.Clientes.Find(id);
            if (cliente == null)
            {
                return false;
            }

            _bancoContext.Clientes.Remove(cliente);
            _bancoContext.SaveChanges();
            return true;
        }

        public ClientesModel ObterPorNome(string nomeCliente)
        {
            var nomeLower = nomeCliente.ToLower();
            return _bancoContext.Clientes.FirstOrDefault(c => c.Nome.ToLower() == nomeLower);
        }

        public bool VerificarNomeClienteUnico(string nomeCliente, int? clienteID = null)
        {
            var nomeMinusc = nomeCliente.ToLower();

            return !_bancoContext.Clientes.Any(c => c.Nome.ToLower() == nomeMinusc && (!clienteID.HasValue || c.ClienteID != clienteID.Value));
        }

        public bool VerificarCpfUnico(string cpf, int? clienteID = null)
        {
            var cpfLower = cpf.ToLower();

            return !_bancoContext.Clientes.Any(c => c.CPF == cpfLower && (!clienteID.HasValue || c.ClienteID != clienteID.Value));
        }
    }
}
