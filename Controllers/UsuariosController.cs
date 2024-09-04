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
        private readonly IConcessionariasRepositorio _concessionariasRepositorio;

        public UsuariosController(IUsuariosRepositorio usuariosRepositorio, IConcessionariasRepositorio concessionariasRepositorio)
        {
            _usuariosRepositorio = usuariosRepositorio;
            _concessionariasRepositorio = concessionariasRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuariosRepositorio.ObterTodosUsuariosAsync();
            return View(usuarios);
        }

        public async Task<IActionResult> Criar()
        {
            var concessionarias = await _concessionariasRepositorio.ListarTodosAsync();
            ViewBag.Concessionarias = concessionarias?.Select(c => new SelectListItem
            {
                Value = c.ConcessionariaID.ToString(),
                Text = c.Nome
            }).ToList();

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
                    Senha = usuarioViewModel.Senha,
                    Email = usuarioViewModel.Email,
                    NivelAcesso = usuarioViewModel.NivelAcesso,
                    ConcessionariaID = usuarioViewModel.ConcessionariaID
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
                ConcessionariaID = usuario.ConcessionariaID
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

        public async Task<IActionResult> Apagar(int id)
        {
            var usuario = await _usuariosRepositorio.ObterUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Apagar")]
        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            await _usuariosRepositorio.RemoverUsuarioAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
    

