using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intelectah.Controllers
{
    public class VendasController : Controller
    {
        private readonly IVendasRepositorio _vendasRepositorio;
        private readonly IVeiculosRepositorio _veiculosRepositorio;
        private readonly IClientesRepositorio _clientesRepositorio; 
        private readonly IConcessionariasRepositorio _concessionariasRepositorio; 

        public VendasController(
            IVendasRepositorio vendasRepositorio,
            IVeiculosRepositorio veiculosRepositorio,
            IClientesRepositorio clientesRepositorio,
            IConcessionariasRepositorio concessionariasRepositorio)
        {
            _vendasRepositorio = vendasRepositorio;
            _veiculosRepositorio = veiculosRepositorio;
            _clientesRepositorio = clientesRepositorio;
            _concessionariasRepositorio = concessionariasRepositorio;
        }

        public IActionResult Index()
        {
           return View();
        }

        public IActionResult Criar()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            return RedirectToAction("Index");
        }

       
    }
}
