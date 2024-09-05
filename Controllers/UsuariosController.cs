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

        public IActionResult Index()
        {
            var usuarios = _usuariosRepositorio.ObterTodosUsuarios();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View(new UsuariosViewModel());
        }

        public IActionResult Editar(int id)
        {
            var usuario = _usuariosRepositorio.ObterUsuarioPorId(id);
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

        public IActionResult ApagarConfirmacao(int id)
        {
            var usuario = _usuariosRepositorio.ListarPorId(id);
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

        [HttpPost]
        public IActionResult Criar(UsuariosViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!_usuariosRepositorio.VerificarNomeUsuarioUnico(usuarioViewModel.NomeUsuario))
                {
                    ModelState.AddModelError("NomeUsuario", "Este nome de usuário já está em uso.");
                    return View(usuarioViewModel);
                }
                var usuario = new UsuariosModel
                {
                    NomeUsuario = usuarioViewModel.NomeUsuario,
                    Login = usuarioViewModel.Login,
                    Senha = usuarioViewModel.Senha,
                    Email = usuarioViewModel.Email,
                    NivelAcesso = usuarioViewModel.NivelAcesso,
                };

                _usuariosRepositorio.AdicionarUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioViewModel);
        }

        [HttpPost]
        public IActionResult Editar(int id, UsuariosViewModel usuarioViewModel)
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
                    _usuariosRepositorio.AtualizarUsuario(usuario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_usuariosRepositorio.ObterUsuarioPorId(id) == null)
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
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuariosRepositorio.RemoverUsuario(id);
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
    }
}
    

