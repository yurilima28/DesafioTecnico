using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IFabricantesRepositorio
    {
        FabricantesModel ListarPorId(int Id);
        List<FabricantesModel> BuscarTodos();
        FabricantesModel Adicionar(FabricantesModel fabricante);
        FabricantesModel Atualizar (FabricantesModel fabricante);
        bool Apagar (int Id);
         
    }
}
