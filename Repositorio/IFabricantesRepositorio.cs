using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IFabricantesRepositorio
    {
        List<FabricantesModel> BuscarTodos();
        FabricantesModel Adicionar(FabricantesModel fabricante);
    }
}
