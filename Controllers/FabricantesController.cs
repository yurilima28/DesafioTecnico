using Intelectah.Dapper;
using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.Services;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Intelectah.Controllers
{
    public class FabricantesController : Controller
    {
        private readonly IFabricantesRepositorio _fabricantesRepositorio;
        private readonly BancoContext _bancoContext;
        private readonly CountryService _countryService;

        public FabricantesController(IFabricantesRepositorio fabricantesRepositorio, CountryService countryService, BancoContext bancoContext)
        {
            _fabricantesRepositorio = fabricantesRepositorio;
            _countryService = countryService;
            _bancoContext = bancoContext;
        }

        private static readonly Dictionary<string, string> CountryTranslations = new Dictionary<string, string>
        {
            { "Brazil", "Brasil" }
        };

        private List<SelectListItem> GetListaPaises()
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync("https://restcountries.com/v3.1/all").Result;
                var countries = JsonConvert.DeserializeObject<List<CountryModel>>(response);

                var selectList = countries.Select(c => new SelectListItem
                {
                    Value = c.Cca2,
                    Text = CountryTranslations.ContainsKey(c.Name.Common) ? CountryTranslations[c.Name.Common] : c.Name.Common
                }).OrderBy(c => c.Text).ToList();
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

        public IActionResult Criar()
        {
            var viewModel = new FabricantesViewModel
            {
                OpcaoPaises = GetListaPaises()
            };
            return View(viewModel);
        }

        public IActionResult Editar(int id)
        {
            var fabricante = _fabricantesRepositorio.ListarPorId(id);

            if (fabricante == null)
            {
                return NotFound();
            }

            var viewModel = new FabricantesViewModel
            {
                FabricanteID = fabricante.FabricanteID,
                NomeFabricante = fabricante.NomeFabricante,
                PaisOrigem = fabricante.PaisOrigem,
                AnoFundacao = fabricante.AnoFundacao,
                URL = fabricante.URL,
                OpcaoPaises = GetListaPaises()
            };
            return View(viewModel);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _fabricantesRepositorio.Apagar(id);
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

        [HttpPost]
        public IActionResult Criar(FabricantesViewModel viewModel)
        {
            if (!_fabricantesRepositorio.VerificarNomeFabricanteUnico(viewModel.NomeFabricante))
            {
                ModelState.AddModelError(nameof(viewModel.NomeFabricante), "O nome do fabricante já existe.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var fabricantesModel = new FabricantesModel
                    {
                        FabricanteID = viewModel.FabricanteID,
                        NomeFabricante = viewModel.NomeFabricante,
                        PaisOrigem = viewModel.PaisOrigem,
                        AnoFundacao = viewModel.AnoFundacao,
                        URL = viewModel.URL
                    };

                    _fabricantesRepositorio.Adicionar(fabricantesModel);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Ocorreu um erro ao salvar o fabricante: {ex.Message}");
                }
            }

            viewModel.OpcaoPaises = GetListaPaises();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(FabricantesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fabricante = new FabricantesModel
                    {
                        FabricanteID = viewModel.FabricanteID,
                        NomeFabricante = viewModel.NomeFabricante,
                        PaisOrigem = viewModel.PaisOrigem,
                        AnoFundacao = viewModel.AnoFundacao,
                        URL = viewModel.URL
                    };

                    _fabricantesRepositorio.Atualizar(fabricante);

                    TempData["MensagemSucesso"] = "Fabricante alterado com sucesso";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Erro ao atualizar o fabricante: {ex.Message}";
                }
            }

            viewModel.OpcaoPaises = GetListaPaises();
            return View(viewModel);
        }
    }
}
