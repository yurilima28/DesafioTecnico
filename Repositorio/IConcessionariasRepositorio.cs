using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IConcessionariasRepositorio
    {
        Task<IEnumerable<ConcessionariasModel>> ListarTodosAsync();
        Task<ConcessionariasModel> ListarPorIdAsync(int id);
        Task AdicionarAsync(ConcessionariasModel concessionaria);
        Task AtualizarAsync(ConcessionariasModel concessionaria);
        Task RemoverAsync(int id);
        ConcessionariasModel ObterPorNome(string nomeConcessionaria);
    }
}
