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

        public IActionResult Editar()
        {
            return View();
        }

        public IActionResult ApagarConfirmacao()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(FabricantesModel fabricante)
        {
            _fabricantesRepositorio.Adicionar(fabricante);
            return RedirectToAction("Index");
        }

    }
}
