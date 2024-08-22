using Intelectah.Models;
using Intelectah.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Intelectah.Controllers
{
    public class FabricantesController : Controller
    {
        private readonly IFabricantesRepositorio _fabricantesRepositorio;
        public FabricantesController(IFabricantesRepositorio fabricantesRepositorio)
        {
            _fabricantesRepositorio = fabricantesRepositorio;
        }

        public IActionResult Index()
        {
            List<FabricantesModel> fabricantes = _fabricantesRepositorio.BuscarTodos();
            return View(fabricantes);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int Id)
        {
            FabricantesModel fabricante = _fabricantesRepositorio.ListarPorId(Id);
            return View(fabricante);
        }

        public IActionResult ApagarConfirmacao(int Id)
        {
            FabricantesModel fabricante = _fabricantesRepositorio.ListarPorId(Id);
            return View(fabricante);
        }

        public IActionResult Apagar(int Id)
        {
            try
            {
                bool apagado = _fabricantesRepositorio.Apagar(Id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Fabricante deletado com sucesso";

                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possivel deletar o fabricante";

                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possivel deletar o fabricante, tente novamente, detalhe do erro:{erro.Message}";
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(FabricantesModel fabricante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _fabricantesRepositorio.Adicionar(fabricante);
                    TempData["MensagemSucesso"] = "Fabricante cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(fabricante);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possivel cadastrar o fabricante, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Alterar(FabricantesModel fabricante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _fabricantesRepositorio.Atualizar(fabricante);
                    TempData["MensagemSucesso"] = "Fabricante alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Editar", fabricante);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possivel alterar o fabricante, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
