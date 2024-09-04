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
        public async Task AdicionarUsuarioAsync(UsuariosModel usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            _bancoContext.Usuarios.Add(usuario);
            await _bancoContext.SaveChangesAsync();
        }

        public async Task AtualizarUsuarioAsync(UsuariosModel usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            var usuarioExistente = await _bancoContext.Usuarios.FindAsync(usuario.UsuarioID);
            if (usuarioExistente == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            _bancoContext.Entry(usuarioExistente).CurrentValues.SetValues(usuario);
            await _bancoContext.SaveChangesAsync();
        }

        public async Task<UsuariosModel> ListarPorIdAsync(int id)
        {
            return await _bancoContext.Usuarios.FindAsync(id);
        }

        public async Task RemoverUsuarioAsync(int usuarioId)
        {
            var usuario = await _bancoContext.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            _bancoContext.Usuarios.Remove(usuario);
            await _bancoContext.SaveChangesAsync();
        }

        public async Task<UsuariosModel> ObterUsuarioPorIdAsync(int usuarioId)
        {
            return await _bancoContext.Usuarios.FindAsync(usuarioId);
        }

        public async Task<IEnumerable<UsuariosModel>> ObterTodosUsuariosAsync()
        {
            return await _bancoContext.Usuarios.ToListAsync();
        }

        public async Task<bool> UsuarioExisteAsync(string nomeUsuario)
        {
            return await _bancoContext.Usuarios.AnyAsync(u => u.NomeUsuario == nomeUsuario);
        }

        public async Task<bool> ApagarAsync(int id)
        {
            var usuario = await ListarPorIdAsync(id); 
            if (usuario == null) return false;

            _bancoContext.Usuarios.Remove(usuario);
            await _bancoContext.SaveChangesAsync();
            return true;
        }
    }
}
