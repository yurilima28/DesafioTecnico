using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Intelectah.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosRepositorio _usuariosRepositorio;

        public UsuariosController(IUsuariosRepositorio usuariosRepositorio)
        {
            _usuariosRepositorio = usuariosRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuariosRepositorio.ObterTodosUsuariosAsync();
            return View(usuarios);
        }

        public async Task<IActionResult> Criar()
        {
        
            return View(new UsuariosViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Criar(UsuariosViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = new UsuariosModel
                {
                    NomeUsuario = usuarioViewModel.NomeUsuario,
                    Login = usuarioViewModel.Login,
                    Senha = usuarioViewModel.Senha,
                    Email = usuarioViewModel.Email,
                    NivelAcesso = usuarioViewModel.NivelAcesso,
                };

                await _usuariosRepositorio.AdicionarUsuarioAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioViewModel);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _usuariosRepositorio.ObterUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioViewModel = new UsuariosViewModel
            {
                UsuarioId = usuario.UsuarioID,
                NomeUsuario = usuario.NomeUsuario,
                Senha = usuario.Senha,
                Email = usuario.Email,
                NivelAcesso = usuario.NivelAcesso,
            };

            return View(usuarioViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, UsuariosViewModel usuarioViewModel)
        {
            if (id != usuarioViewModel.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var usuario = new UsuariosModel
                {
                    UsuarioID = id,
                    NomeUsuario = usuarioViewModel.NomeUsuario,
                    Senha = usuarioViewModel.Senha,
                    Email = usuarioViewModel.Email,
                    NivelAcesso = usuarioViewModel.NivelAcesso
                };

                try
                {
                    await _usuariosRepositorio.AtualizarUsuarioAsync(usuario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _usuariosRepositorio.ObterUsuarioPorIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(usuarioViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _usuariosRepositorio.ApagarAsync(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário excluído com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível encontrar o usuário para exclusão.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Não foi possível excluir o usuário. Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            var usuario = await _usuariosRepositorio.ListarPorIdAsync(id);
            if (usuario == null)
            {
                TempData["MensagemErro"] = "Usuário não encontrado.";
                return RedirectToAction("Index");
            }

            var viewModel = new UsuariosViewModel
            {
                UsuarioId = usuario.UsuarioID,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
            };

            return View(viewModel);
        }
    }
}
    

