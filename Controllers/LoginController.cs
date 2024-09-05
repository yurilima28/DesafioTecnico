using Intelectah.Models;
using Intelectah.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Intelectah.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuariosRepositorio _usuariosRepositorio;

        public LoginController(IUsuariosRepositorio usuariosRepousitorio)
        {
            _usuariosRepositorio = usuariosRepousitorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuariosModel usuario = _usuariosRepositorio.BuscarPorLogin(loginModel.Login);
                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            return RedirectToAction("Index", "Home");

                        }
                        TempData["MensagemErro"] = $"Senha do usuário é inválida, tente novamente.";

                    }
                    TempData["MensagemErro"] = $"Usuário e/ou senha é inválida, tente novamente.";

                }
                return View("Index");

            }
            catch (Exception erro)
            {
                {
                    TempData["MensagemErro"] = $"Não conseguimos realizar seu login: {erro.Message}";
                    return RedirectToAction("Index");
                }

            }
        }
    }
}

