using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Details(int id)
        {
            var usuario = await _usuariosRepositorio.ObterUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("NomeUsuario,Senha,Email,NivelAcesso")] UsuariosViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = new UsuariosModel
                {
                    NomeUsuario = usuarioViewModel.NomeUsuario,
                    Senha = usuarioViewModel.Senha,
                    Email = usuarioViewModel.Email,
                    NivelAcesso = usuarioViewModel.NivelAcesso
                };

                await _usuariosRepositorio.AdicionarUsuarioAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _usuariosRepositorio.ObterUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioViewModel = new UsuariosViewModel
            {
                NomeUsuario = usuario.NomeUsuario,
                Senha = usuario.Senha,
                Email = usuario.Email,
                NivelAcesso = usuario.NivelAcesso
            };

            return View(usuarioViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("NomeUsuario,Senha,Email,NivelAcesso")] UsuariosViewModel usuariosViewModel)
        {
            if (id != usuariosViewModel.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var usuario = new UsuariosModel
                {
                    UsuarioID = id,
                    NomeUsuario = usuariosViewModel.NomeUsuario,
                    Senha = usuariosViewModel.Senha,
                    Email = usuariosViewModel.Email,
                    NivelAcesso = usuariosViewModel.NivelAcesso
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

            return View(usuariosViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuariosRepositorio.ObterUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _usuariosRepositorio.RemoverUsuarioAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
