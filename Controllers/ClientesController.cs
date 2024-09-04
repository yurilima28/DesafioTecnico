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
                CPF = c.CPF,
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

            return View("Editar", viewModel);
        }
   

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            var cliente = _clientesRepositorio.ListarPorId(id);
            if (cliente == null)
            {
                TempData["MensagemErro"] = "Cliente não encontrado.";
                return RedirectToAction("Index");
            }

            var viewModel = new ClientesModel
            {
                ClienteID = cliente.ClienteID,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Telefone = cliente.Telefone
            };

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

                _clientesRepositorio.Atualizar(cliente);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _clientesRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Cliente deletado com sucesso.";
                }
                else
                {
                    TempData["MensagemErro"] = "Cliente não encontrado ou não foi possível deletar o cliente.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível deletar o cliente. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }     

    }
}
