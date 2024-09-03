using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.Services;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using static Intelectah.Services.CountryService;

namespace Intelectah.Controllers
{
    public class FabricantesController : Controller
    {
        private readonly IFabricantesRepositorio _fabricantesRepositorio;
        private readonly CountryService _countryService;

        public FabricantesController(IFabricantesRepositorio fabricantesRepositorio, CountryService countryService)
        {
            _fabricantesRepositorio = fabricantesRepositorio;
            _countryService = countryService;
        }
        private static readonly Dictionary<string, string> CountryTranslations = new Dictionary<string, string>
        {

            { "Brazil", "Brasil" }
        };
        private async Task<IEnumerable<SelectListItem>> GetListaPaisesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync("https://restcountries.com/v3.1/all");
                var countries = JsonConvert.DeserializeObject<List<CountryModel>>(response);

                var selectList = countries.Select(c => new SelectListItem
                {
                    Value = c.Cca2,
                    Text = CountryTranslations.ContainsKey(c.Name.Common) ? CountryTranslations[c.Name.Common] : c.Name.Common
                }).ToList();

                selectList = selectList.OrderBy(c => c.Text).ToList();

                return selectList;
            }
        }
        public async Task<IActionResult> Index()
        {
            List<FabricantesModel> fabricantes = _fabricantesRepositorio.BuscarTodos();

            List<FabricantesViewModel> viewModel = fabricantes.Select(f => new FabricantesViewModel
            {
                FabricanteID = f.FabricanteID,
                NomeFabricante = f.NomeFabricante,
                PaisOrigem = f.PaisOrigem,
                AnoFundacao = f.AnoFundacao,
                URL = f.URL,
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Criar()
        {
            var fabricantesModel = new FabricantesModel();

            var viewModel = new FabricantesViewModel
            {
                FabricanteID = fabricantesModel.FabricanteID,
                NomeFabricante = fabricantesModel.NomeFabricante,
                PaisOrigem = fabricantesModel.PaisOrigem,
                AnoFundacao = fabricantesModel.AnoFundacao,
                URL = fabricantesModel.URL,
                OpcaoPaises = await GetListaPaisesAsync(),
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Editar(int Id)
        {
            FabricantesModel fabricante = _fabricantesRepositorio.ListarPorId(Id);

            if (fabricante == null)
            {
                TempData["MensagemErro"] = "Fabricante não encontrado.";
                return RedirectToAction("Index");
            }

            var opcaoPaises = await GetListaPaisesAsync();

            var viewModel = new FabricantesViewModel
            {
                FabricanteID = fabricante.FabricanteID,
                NomeFabricante = fabricante.NomeFabricante,
                PaisOrigem = fabricante.PaisOrigem,
                AnoFundacao = fabricante.AnoFundacao,
                URL = fabricante.URL,
                OpcaoPaises = opcaoPaises
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ApagarConfirmacao(int Id)
        {
            var fabricante = _fabricantesRepositorio.ListarPorId(Id);
            if (fabricante == null)
            {
                TempData["MensagemErro"] = "Fabricante não encontrado.";
                return RedirectToAction("Index");
            }

            var viewModel = new FabricantesViewModel
            {
                FabricanteID = fabricante.FabricanteID,
                NomeFabricante = fabricante.NomeFabricante,
                PaisOrigem = fabricante.PaisOrigem,
                AnoFundacao = fabricante.AnoFundacao,
                URL = fabricante.URL,
            };

            return View(viewModel);
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
                    TempData["MensagemErro"] = "Não foi possível deletar o fabricante.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível deletar o fabricante, tente novamente. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(FabricantesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var fabricantesModel = new FabricantesModel
                {
                    FabricanteID = viewModel.FabricanteID,
                    NomeFabricante = viewModel.NomeFabricante,
                    PaisOrigem = viewModel.PaisOrigem,
                    AnoFundacao = viewModel.AnoFundacao,
                    URL = viewModel.URL
                };

                await _fabricantesRepositorio.AdicionarAsync(fabricantesModel);
                return RedirectToAction("Index");
            }

            viewModel.OpcaoPaises = await GetListaPaisesAsync();

            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Alterar(FabricantesViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var fabricante = new FabricantesModel
                    {
                        FabricanteID = viewModel.FabricanteID,
                        NomeFabricante = viewModel.NomeFabricante,
                        PaisOrigem = viewModel.PaisOrigem,
                        AnoFundacao = viewModel.AnoFundacao,
                        URL = viewModel.URL,
                    };

                    _fabricantesRepositorio.Atualizar(fabricante);
                    TempData["MensagemSucesso"] = "Fabricante alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", viewModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível alterar o fabricante, tente novamente. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
