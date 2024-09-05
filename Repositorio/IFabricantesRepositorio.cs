using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IFabricantesRepositorio
    {
        FabricantesModel ListarPorId(int id);
        List<FabricantesModel> BuscarTodos();
        FabricantesModel Adicionar(FabricantesModel fabricante);
        FabricantesModel Atualizar(FabricantesModel fabricante);
        bool Apagar(int id);
        FabricantesModel ObterPorNome(string nomeFabricante);
        bool VerificarNomeFabricanteUnico(string nomeFabricante);
    }

}
