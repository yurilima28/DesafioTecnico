using Intelectah.Models;

namespace Intelectah.Repositorio
{
    public interface IUsuariosRepositorio
    {
        UsuariosModel BuscarPorLogin(string login);
        UsuariosModel ListarPorId(int id);
        UsuariosModel AdicionarUsuario(UsuariosModel usuario);
        UsuariosModel AtualizarUsuario(UsuariosModel usuario);
        bool RemoverUsuario(int usuarioId);
        UsuariosModel ObterUsuarioPorId(int usuarioId);
        List<UsuariosModel> ObterTodosUsuarios();
        bool UsuarioExiste(string nomeUsuario);
        bool VerificarNomeUsuarioUnico(string nomeUsuario);
    }
}
