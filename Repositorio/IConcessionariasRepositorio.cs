using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IConcessionariasRepositorio
    {
        Task<IEnumerable<ConcessionariasModel>> ListarTodosAsync();
        Task<ConcessionariasModel> ListarPorIdAsync(int id);
        Task AdicionarAsync(ConcessionariasModel concessionaria);
        Task AtualizarAsync(ConcessionariasModel concessionaria);
        bool Apagar(int Id);
        ConcessionariasModel ObterPorNome(string nomeConcessionaria);
        bool VerificarNomeConcessionariaUnico(string nomeConcessionaria, int? concessionariaID = null);

    }
}
