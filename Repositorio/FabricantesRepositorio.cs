using Intelectah.Dapper;
using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public class FabricantesRepositorio : IFabricantesRepositorio
    {
        private readonly BancoContext _bancoContext;
        public FabricantesRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public List<FabricantesModel> BuscarTodos()
        {
            return _bancoContext.Fabricantes.ToList();
        }
        public FabricantesModel Adicionar(FabricantesModel fabricante)
        {
            _bancoContext.Fabricantes.Add(fabricante);
            _bancoContext.SaveChanges();
            return fabricante;
        }
       
    }
}
