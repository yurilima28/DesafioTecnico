using Intelectah.Dapper;
using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Intelectah.Controllers
{
    public class ClientesController : Controller
    {
       private readonly IClientesRepositorio _clientesRepositorio;

        public ClientesController(IClientesRepositorio clientesRepositorio) 
        {
            _clientesRepositorio = clientesRepositorio;
        }
        public async Task<IActionResult> Index()
        {
            var clientes = _clientesRepositorio.BuscarTodos();

            var viewModel = clientes.Select(c => new ClientesViewModel
            {
                ClienteID = c.ClienteID,
                Nome = c.Nome,
                Email = c.Email,
                Telefone = c.Telefone,
               
            }).ToList();

            return View(viewModel);
        }
        public async Task<IActionResult> Index()
        {
            var clientes = _clientesRepositorio.BuscarTodos();

            var viewModel = clientes.Select(c => new ClientesViewModel
            {
                ClienteID = c.ClienteID,
                Nome = c.Nome,
                Email = c.Email,
                Telefone = c.Telefone,
            }).ToList();

            return View(viewModel);
        }

        public IActionResult Criar()
        {
            var viewModel = new ClientesViewModel();
            ViewData["FormAction"] = "Criar";
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(ClientesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var cliente = new ClientesModel
                {
                    Nome = viewModel.Nome,
                    CPF = viewModel.CPF,
                    Email = viewModel.Email,
                    Telefone = viewModel.Telefone,                
                };

                await _clientesRepositorio.AdicionarAsync(cliente);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var cliente = _clientesRepositorio.ListarPorId(id);
            if (cliente == null)
            {
                TempData["MensagemErro"] = "Cliente não encontrado.";
                return RedirectToAction("Index");
            }

            var viewModel = new ClientesViewModel
            {
                ClienteID = cliente.ClienteID,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
            };

            ViewData["FormAction"] = "Alterar";
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ClientesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var cliente = new ClientesModel
                {
                    ClienteID = viewModel.ClienteID,
                    Nome = viewModel.Nome,
                    Email = viewModel.Email,
                    Telefone = viewModel.Telefone,
                };

                await _clientesRepositorio.Atualizar(cliente);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Apagar(int id)
        {
            var cliente = _clientesRepositorio.ListarPorId(id);
            if (cliente == null)
            {
                TempData["MensagemErro"] = "Cliente não encontrado.";
                return RedirectToAction("Index");
            }

            await _clientesRepositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            var cliente = _clientesRepositorio.ListarPorId(id);
            if (cliente == null)
            {
                TempData["MensagemErro"] = "Cliente não encontrado.";
                return RedirectToAction("Index");
            }

            var viewModel = new ClientesViewModel
            {
                ClienteID = cliente.ClienteID,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
            };

            return View(viewModel);
        }

    }
}
