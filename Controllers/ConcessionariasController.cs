using Intelectah.Dapper;
using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Intelectah.Controllers
{
    public class ConcessionariasController : Controller
    {

        private readonly IConcessionariasRepositorio _concessionariasRepositorio;
        private readonly BancoContext _bancoContext;

        public ConcessionariasController(IConcessionariasRepositorio concessionariasRepositorio, BancoContext bancoContext)
        {
            _concessionariasRepositorio = concessionariasRepositorio;
            _bancoContext = bancoContext;
        }

        public async Task<IActionResult> Index()
        {
            var concessionarias = await _concessionariasRepositorio.ListarTodosAsync();

            var viewModel = concessionarias.Select(c => new ConcessionariasViewModel
            {
                ConcessionariaID = c.ConcessionariaID,
                Nome = c.Nome,
                Endereco = new EnderecoViewModel
                {
                    EnderecoCompleto = c.EnderecoCompleto,
                    Cidade = c.Cidade,
                    Estado = c.Estado,
                    CEP = c.CEP
                },
                Telefone = c.Telefone,
                Email = c.Email,
                CapacidadeMax = c.CapacidadeMax
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Criar()
        {
            var viewModel = new ConcessionariasViewModel
            {
                Endereco = new EnderecoViewModel()
            };

            ViewData["FormAction"] = "Criar";
            return View(viewModel);
        }

        public async Task<IActionResult> Apagar(int Id)
        {
            var concessionaria = _concessionariasRepositorio.ListarPorIdAsync(Id);
            if (concessionaria == null)
            {
                return NotFound();
            }

            await _concessionariasRepositorio.RemoverAsync(Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ApagarConfirmacao(int Id)
        {
            var concessionaria = await _concessionariasRepositorio.ListarPorIdAsync(Id);

            if (concessionaria == null)
            {
                TempData["MensagemErro"] = "Concessionária não encontrada.";
                return RedirectToAction("Index");
            }

            var viewModel = new ConcessionariasViewModel
            {
                ConcessionariaID = concessionaria.ConcessionariaID,
                Nome = concessionaria.Nome,
                Endereco = new EnderecoViewModel
                {
                    EnderecoCompleto = concessionaria.EnderecoCompleto,
                    Cidade = concessionaria.Cidade,
                    Estado = concessionaria.Estado,
                    CEP = concessionaria.CEP
                },
                Telefone = concessionaria.Telefone,
                Email = concessionaria.Email,
                CapacidadeMax = concessionaria.CapacidadeMax
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var concessionaria = await _concessionariasRepositorio.ListarPorIdAsync(id);

            if (concessionaria == null)
            {
                TempData["MensagemErro"] = "Concessionária não encontrada.";
                return RedirectToAction("Index");
            }

            var viewModel = new ConcessionariasViewModel
            {
                ConcessionariaID = concessionaria.ConcessionariaID,
                Nome = concessionaria.Nome,
                Endereco = new EnderecoViewModel
                {
                    EnderecoCompleto = concessionaria.EnderecoCompleto,
                    Cidade = concessionaria.Cidade,
                    Estado = concessionaria.Estado,
                    CEP = concessionaria.CEP
                },
                Telefone = concessionaria.Telefone,
                Email = concessionaria.Email,
                CapacidadeMax = concessionaria.CapacidadeMax
            };

            ViewData["FormAction"] = "Alterar";
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(ConcessionariasViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var concessionaria = new ConcessionariasModel
                {
                    Nome = viewModel.Nome,
                    EnderecoCompleto = viewModel.Endereco.EnderecoCompleto,
                    Cidade = viewModel.Endereco.Cidade,
                    Estado = viewModel.Endereco.Estado,
                    CEP = viewModel.Endereco.CEP,
                    Telefone = viewModel.Telefone,
                    Email = viewModel.Email,
                    CapacidadeMax = viewModel.CapacidadeMax
                };

                await _concessionariasRepositorio.AdicionarAsync(concessionaria);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ConcessionariasViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var concessionaria = new ConcessionariasModel
                {
                    ConcessionariaID = viewModel.ConcessionariaID,
                    Nome = viewModel.Nome,
                    EnderecoCompleto = viewModel.Endereco.EnderecoCompleto,
                    Cidade = viewModel.Endereco.Cidade,
                    Estado = viewModel.Endereco.Estado,
                    CEP = viewModel.Endereco.CEP,
                    Telefone = viewModel.Telefone,
                    Email = viewModel.Email,
                    CapacidadeMax = viewModel.CapacidadeMax
                };

                await _concessionariasRepositorio.AtualizarAsync(concessionaria);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
