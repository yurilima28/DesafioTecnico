using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IUsuariosRepositorio
    {
        Task AdicionarUsuarioAsync(UsuariosModel usuario);
        Task AtualizarUsuarioAsync(UsuariosModel usuario);
        Task RemoverUsuarioAsync(int usuarioId);
        Task<UsuariosModel> ObterUsuarioPorIdAsync(int usuarioId);
        Task<IEnumerable<UsuariosModel>> ObterTodosUsuariosAsync();
        Task<bool> UsuarioExisteAsync(string nomeUsuario);
        Task<bool> ApagarAsync(int Id);
        Task<UsuariosModel> ListarPorIdAsync(int id);

    }
}
