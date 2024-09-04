using Intelectah.Enums;
using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Intelectah.Controllers
{
    public class VendasController : Controller
    {
        private readonly IVendasRepositorio _vendasRepositorio;
        private readonly IClientesRepositorio _clientesRepositorio;
        private readonly IConcessionariasRepositorio _concessionariasRepositorio;
        private readonly IVeiculosRepositorio _veiculosRepositorio;
        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IFabricantesRepositorio _fabricantesRepositorio;

        public VendasController(
            IVendasRepositorio vendasRepositorio,
            IClientesRepositorio clientesRepositorio,
            IConcessionariasRepositorio concessionariasRepositorio,
            IVeiculosRepositorio veiculosRepositorio,
            IUsuariosRepositorio usuariosRepositorio,
            IFabricantesRepositorio fabricantesRepositorio)
        {
            _vendasRepositorio = vendasRepositorio;
            _clientesRepositorio = clientesRepositorio;
            _concessionariasRepositorio = concessionariasRepositorio;
            _veiculosRepositorio = veiculosRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
            _fabricantesRepositorio = fabricantesRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            var vendas = _vendasRepositorio.BuscarTodas();
            var vendasViewModel = vendas.Select(v => new VendasViewModel
            {
                VendaId = v.VendaId,
                ClienteID = v.ClienteID,
                DataVenda = v.DataVenda,
                ValorTotal = v.ValorTotal,
                UsuarioID = v.UsuarioID,
                ConcessionariaID = v.ConcessionariaID,
                NomeCliente = v.Cliente.Nome,
                NomeUsuario = v.Usuario.NomeUsuario,
                NomeConcessionaria = v.Concessionaria.Nome,
                FabricanteID = v.FabricanteID, 
                VeiculoID = v.VeiculoID,
            }).ToList();

            return View(vendasViewModel);
        }

        public async Task<IActionResult> Criar()
        {
            var clientes = _clientesRepositorio.BuscarTodos();
            var concessionarias = await _concessionariasRepositorio.ListarTodosAsync();
            var usuarios = await _usuariosRepositorio.ObterTodosUsuariosAsync();

            ViewBag.Clientes = clientes.Select(c => new SelectListItem
            {
                Value = c.ClienteID.ToString(),
                Text = c.Nome
            }).ToList();

            ViewBag.Concessionarias = concessionarias.Select(c => new SelectListItem
            {
                Value = c.ConcessionariaID.ToString(),
                Text = c.Nome
            }).ToList();

            ViewBag.Usuarios = usuarios.Select(u => new SelectListItem
            {
                Value = u.UsuarioID.ToString(),
                Text = u.NomeUsuario
            }).ToList();

            return View(new VendasViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Criar(VendasViewModel vendaViewModel)
        {
            if (ModelState.IsValid)
            {
                var venda = new VendasModel
                {
                    ClienteID = vendaViewModel.ClienteID,
                    ValorTotal = vendaViewModel.ValorTotal,
                    DataVenda = vendaViewModel.DataVenda,
                    UsuarioID = vendaViewModel.UsuarioID,
                    ConcessionariaID = vendaViewModel.ConcessionariaID,
                    FabricanteID = vendaViewModel.FabricanteID,
                    VeiculoID = vendaViewModel.VeiculoID,
                    
                };

                await _vendasRepositorio.AdicionarAsync(venda);
                TempData["MensagemSucesso"] = "Venda criada com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            var clientes = _clientesRepositorio.BuscarTodos();
            var concessionarias = await _concessionariasRepositorio.ListarTodosAsync();
            var usuarios = await _usuariosRepositorio.ObterTodosUsuariosAsync();
            var fabricantes = _fabricantesRepositorio.BuscarTodos();
            var veiculos = _veiculosRepositorio.BuscarTodos();

            ViewBag.Clientes = clientes.Select(c => new SelectListItem
            {
                Value = c.ClienteID.ToString(),
                Text = c.Nome
            }).ToList();

            ViewBag.Concessionarias = concessionarias.Select(c => new SelectListItem
            {
                Value = c.ConcessionariaID.ToString(),
                Text = c.Nome
            }).ToList();

            ViewBag.Usuarios = usuarios.Select(u => new SelectListItem
            {
                Value = u.UsuarioID.ToString(),
                Text = u.NomeUsuario
            }).ToList();

            ViewBag.Fabricantes = fabricantes.Select(f => new SelectListItem
            {
                Value = f.FabricanteID.ToString(),
                Text = f.NomeFabricante
            }).ToList();

            ViewBag.Modelos = veiculos.Select(m => new SelectListItem
            {
                Value = m.VeiculoID.ToString(),
                Text = m.ModeloVeiculo
            }).ToList();

            return View(vendaViewModel);

        }

        public async Task<ActionResult> Editar(int id)
        {
            var venda = _vendasRepositorio.ListarPorId(id);
            if (venda == null)
            {
                TempData["MensagemErro"] = "Venda não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            var clientes = _clientesRepositorio.BuscarTodos();
            var concessionarias = await _concessionariasRepositorio.ListarTodosAsync();
            var usuarios = await _usuariosRepositorio.ObterTodosUsuariosAsync();

            ViewBag.Clientes = clientes.Select(c => new SelectListItem
            {
                Value = c.ClienteID.ToString(),
                Text = c.Nome
            }).ToList();

            ViewBag.Concessionarias = concessionarias.Select(c => new SelectListItem
            {
                Value = c.ConcessionariaID.ToString(),
                Text = c.Nome
            }).ToList();

            ViewBag.Usuarios = usuarios.Select(u => new SelectListItem
            {
                Value = u.UsuarioID.ToString(),
                Text = u.NomeUsuario
            }).ToList();

            var vendaViewModel = new VendasViewModel
            {
                VendaId = venda.VendaId,
                ClienteID = venda.ClienteID,
                ValorTotal = venda.ValorTotal,
                DataVenda = venda.DataVenda,
                UsuarioID = venda.UsuarioID,
                ConcessionariaID = venda.ConcessionariaID,
                NomeCliente = venda.Cliente.Nome,
                NomeUsuario = venda.Usuario.NomeUsuario,
                NomeConcessionaria = venda.Concessionaria.Nome
            };

            return View(vendaViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(VendasViewModel vendaViewModel)
        {
            if (vendaViewModel.VendaId <= 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var venda = new VendasModel
                {
                    VendaId = vendaViewModel.VendaId,
                    ClienteID = vendaViewModel.ClienteID,
                    ValorTotal = vendaViewModel.ValorTotal,
                    DataVenda = vendaViewModel.DataVenda,
                    UsuarioID = vendaViewModel.UsuarioID,
                    ConcessionariaID = vendaViewModel.ConcessionariaID
                };

                try
                {
                     _vendasRepositorio.Atualizar(venda);
                    TempData["MensagemSucesso"] = "Venda atualizada com sucesso.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Erro ao atualizar venda: {ex.Message}";
                }
            }

            var clientes = _clientesRepositorio.BuscarTodos();
            var concessionarias = await _concessionariasRepositorio.ListarTodosAsync();
            var usuarios = await _usuariosRepositorio.ObterTodosUsuariosAsync();

            ViewBag.Clientes = clientes.Select(c => new SelectListItem
            {
                Value = c.ClienteID.ToString(),
                Text = c.Nome
            }).ToList();

            ViewBag.Concessionarias = concessionarias.Select(c => new SelectListItem
            {
                Value = c.ConcessionariaID.ToString(),
                Text = c.Nome
            }).ToList();

            ViewBag.Usuarios = usuarios.Select(u => new SelectListItem
            {
                Value = u.UsuarioID.ToString(),
                Text = u.NomeUsuario
            }).ToList();

            return View(vendaViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado =  _vendasRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Venda excluída com sucesso.";
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível encontrar a venda para exclusão.";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Não foi possível excluir a venda. Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            var venda = _vendasRepositorio.ListarPorId(id);
            if (venda == null)
            {
                TempData["MensagemErro"] = "Venda não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            var vendaViewModel = new VendasViewModel
            {
                VendaId = venda.VendaId,
                ClienteID = venda.ClienteID,
                ValorTotal = venda.ValorTotal,
                DataVenda = venda.DataVenda,
                UsuarioID = venda.UsuarioID,
                ConcessionariaID = venda.ConcessionariaID,
                NomeCliente = venda.Cliente.Nome,
                NomeUsuario = venda.Usuario.NomeUsuario,
                NomeConcessionaria = venda.Concessionaria.Nome
            };

            return View(vendaViewModel);
        }
    }
}
