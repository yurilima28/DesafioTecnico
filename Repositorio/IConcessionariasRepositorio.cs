using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IConcessionariasRepositorio
    {
        ConcessionariasModel ListarPorId(int id);
        List<ConcessionariasModel> BuscarTodos();
        ConcessionariasModel Adicionar(ConcessionariasModel concessionaria);
        ConcessionariasModel Atualizar(ConcessionariasModel concessionaria);
        bool Apagar(int id);
        ConcessionariasModel ObterPorNome(string nomeConcessionaria);
        bool VerificarNomeConcessionariaUnico(string nomeConcessionaria);
    }
}
