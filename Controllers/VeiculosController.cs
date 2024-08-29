using Intelectah.Enums;
using Intelectah.Models;
using Intelectah.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intelectah.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly IVeiculosRepositorio _veiculosRepositorio;
        private readonly IFabricantesRepositorio _fabricantesRepositorio;

        public VeiculosController(IVeiculosRepositorio veiculosRepositorio, IFabricantesRepositorio fabricantesRepositorio)
        {
            _veiculosRepositorio = veiculosRepositorio;
            _fabricantesRepositorio = fabricantesRepositorio;
        }
        public IActionResult Index()
        {
            var veiculos = _veiculosRepositorio.BuscarTodos();
            var fabricantes = _fabricantesRepositorio.BuscarTodos();
            return View(veiculos);
        }
        public IActionResult Criar()
        {
            var fabricantes = _fabricantesRepositorio.BuscarTodos();
            ViewBag.Fabricantes = new SelectList(fabricantes, "FabricanteID", "NomeFabricante");
            return View(new VeiculosModel());
        }
        public IActionResult Editar(int Id)
        {
            VeiculosModel veiculo = _veiculosRepositorio.ListarPorId(Id);
            var fabricantes = _fabricantesRepositorio.BuscarTodos() ?? new List<FabricantesModel>();
            ViewBag.Fabricantes = new SelectList(fabricantes, "FabricanteID", "NomeFabricante");
            return View(veiculo);
        }
        public IActionResult ApagarConfirmacao(int Id)
        {
            try
            {
                bool apagado = _veiculosRepositorio.Apagar(Id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Veículo deletado com sucesso";

                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possivel deletar o veículo";

                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possivel deletar o veículo, tente novamente, detalhe do erro: {erro.Message}";
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(VeiculosModel veiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var 
                    _veiculosRepositorio.Adicionar(veiculo);
                    TempData["MensagemSucesso"] = "Veículo cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(veiculo);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível cadastrar o veículo. Detalhe do erro: {erro.Message}";
                return View(veiculo);
            }

        }

        [HttpPost]
        public IActionResult Alterar(VeiculosModel veiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _veiculosRepositorio.Atualizar(veiculo);
                    TempData["MensagemSucesso"] = "Veículo alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", veiculo);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível alterar o veículo. Detalhe do erro: {erro.Message}";
                return View("Editar", veiculo);
            }
        }
    }
}
