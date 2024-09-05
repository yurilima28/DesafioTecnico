using Intelectah.Dapper;
using Intelectah.Models;
using Microsoft.EntityFrameworkCore;

namespace Intelectah.Repositorio
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuariosRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public UsuariosModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(u => u.Login == login);
        }
        public UsuariosModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.Find(id);
        }
        public UsuariosModel AdicionarUsuario(UsuariosModel usuario)
        {
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }
        public UsuariosModel AtualizarUsuario(UsuariosModel usuario)
        {
            var existingUsuario = _bancoContext.Usuarios.Find(usuario.UsuarioID);
            if (existingUsuario != null)
            {
                _bancoContext.Entry(existingUsuario).CurrentValues.SetValues(usuario);
                _bancoContext.SaveChanges();
                return existingUsuario;
            }
            return null;
        }
        public bool RemoverUsuario(int usuarioId)
        {
            var usuario = _bancoContext.Usuarios.Find(usuarioId);
            if (usuario != null)
            {
                _bancoContext.Usuarios.Remove(usuario);
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }
        public UsuariosModel ObterUsuarioPorId(int usuarioId)
        {
            return _bancoContext.Usuarios.Find(usuarioId);
        }
        public List<UsuariosModel> ObterTodosUsuarios()
        {
            return _bancoContext.Usuarios.ToList();
        }
        public bool UsuarioExiste(string nomeUsuario)
        {
            return _bancoContext.Usuarios.Any(u => u.NomeUsuario == nomeUsuario);
        }

        public bool VerificarNomeUsuarioUnico(string nomeUsuario)
        {
            return !_bancoContext.Usuarios.Any(u => u.NomeUsuario == nomeUsuario);
        }
    }
}
