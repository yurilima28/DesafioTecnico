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
        private readonly IClientesRepositorio _clientesRepositorio;
        private readonly IConcessionariasRepositorio _concessionariasRepositorio;
        private readonly IVeiculosRepositorio _veiculosRepositorio;
        private readonly IFabricantesRepositorio _fabricantesRepositorio;
        private readonly IUsuariosRepositorio _usuariosRepositorio;

        public VendasController(IVendasRepositorio vendasRepositorio, IClientesRepositorio clientesRepositorio, IConcessionariasRepositorio concessionariasRepositorio, IVeiculosRepositorio veiculosRepositorio, IFabricantesRepositorio fabricantesRepositorio, IUsuariosRepositorio usuariosRepositorio)
        {
            _vendasRepositorio = vendasRepositorio;
            _clientesRepositorio = clientesRepositorio;
            _concessionariasRepositorio = concessionariasRepositorio;
            _veiculosRepositorio = veiculosRepositorio;
            _fabricantesRepositorio = fabricantesRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
        }

        public IActionResult Index()
        {
            List<VendasModel> vendas = _vendasRepositorio.BuscarTodos();

            var vendasViewModel = vendas.Select(v => new VendasViewModel
            {
                VendaId = v.VendaId,
                ClienteID = v.ClienteID,
                DataVenda = v.DataVenda,
                ValorTotal = v.ValorTotal,
                UsuarioID = v.UsuarioID,
                FabricanteID = v.FabricanteID,
                VeiculoID = v.VeiculoID
            }).ToList();

            return View(vendasViewModel);
        }
 
        public IActionResult Criar()
        {
            var viewModel = new VendasViewModel
            {
                ProtocoloVenda = GerarProtocoloVenda()
            };
            var clientes = _clientesRepositorio.BuscarTodos();

            ViewBag.Clientes = clientes.Select(c => new SelectListItem
            {
                Value = c.ClienteID.ToString(),
                Text = c.Nome
            }).ToList();

            var usuarios = _usuariosRepositorio.ObterTodosUsuarios();
            ViewBag.Usuarios = usuarios.Select(u => new SelectListItem
            {
                Value = u.UsuarioID.ToString(),
                Text = u.NomeUsuario
            }).ToList();

            var concessionarias = _concessionariasRepositorio.BuscarTodos();
            ViewBag.Concessionarias = concessionarias.Select(co => new SelectListItem
            {
                Value = co.ConcessionariaID.ToString(),
                Text = co.Nome
            }).ToList();

            var fabricantes = _fabricantesRepositorio.BuscarTodos();
            ViewBag.Fabricantes = fabricantes.Select(f => new SelectListItem
            {
                Value = f.FabricanteID.ToString(),
                Text = f.NomeFabricante
            }).ToList();

            var veiculos = _veiculosRepositorio.BuscarTodos();
            ViewBag.Modelos = veiculos.Select(v => new SelectListItem
            {
                Value = v.VeiculoID.ToString(),
                Text = v.ModeloVeiculo
            }).ToList();

            return View(viewModel);
        }
        public IActionResult Editar(int id)
        {
            var venda = _vendasRepositorio.ListarPorId(id);
            if (venda == null)
            {
                TempData["MensagemErro"] = "Venda não encontrada.";
                return RedirectToAction("Index");
            }

            var vendaViewModel = MapearParaViewModel(venda);
            PrepararDadosDropdowns();
            return View(vendaViewModel);
        }

        [HttpPost]
        public IActionResult Criar(VendasViewModel vendaViewModel)
        {
            if (ModelState.IsValid)
            {
                var novaVenda = new VendasModel
                {
                    ClienteID = vendaViewModel.ClienteID,
                    DataVenda = vendaViewModel.DataVenda,
                    ValorTotal = vendaViewModel.ValorTotal,
                    UsuarioID = vendaViewModel.UsuarioID,
                    ConcessionariaID = vendaViewModel.ConcessionariaID,
                    FabricanteID = vendaViewModel.FabricanteID,
                    VeiculoID = vendaViewModel.VeiculoID,
                    ProtocoloVenda = GerarProtocoloVenda()
                };

                _vendasRepositorio.Adicionar(novaVenda);
                TempData["MensagemSucesso"] = "Venda criada com sucesso!";
                return RedirectToAction("Index");
            }

            PrepararDadosDropdowns();
            return View(vendaViewModel);
        }

  
        [HttpPost]
        public IActionResult Editar(VendasViewModel vendaViewModel)
        {
            if (ModelState.IsValid)
            {
                var venda = _vendasRepositorio.ListarPorId(vendaViewModel.VendaId);
                if (venda == null)
                {
                    TempData["MensagemErro"] = "Venda não encontrada.";
                    return RedirectToAction("Index");
                }

                venda.ClienteID = vendaViewModel.ClienteID;
                venda.DataVenda = vendaViewModel.DataVenda;
                venda.ValorTotal = vendaViewModel.ValorTotal;
                venda.UsuarioID = vendaViewModel.UsuarioID;
                venda.ConcessionariaID = vendaViewModel.ConcessionariaID;
                venda.FabricanteID = vendaViewModel.FabricanteID;
                venda.VeiculoID = vendaViewModel.VeiculoID;

                _vendasRepositorio.Atualizar(venda);
                TempData["MensagemSucesso"] = "Venda atualizada com sucesso!";
                return RedirectToAction("Index");
            }

            PrepararDadosDropdowns();
            return View(vendaViewModel);
        }

        [HttpPost]
        public IActionResult Apagar(int id)
        {
            var venda = _vendasRepositorio.ListarPorId(id);
            if (venda == null)
            {
                TempData["MensagemErro"] = "Venda não encontrada.";
                return RedirectToAction("Index");
            }

            bool apagou = _vendasRepositorio.Apagar(id);
            if (apagou)
            {
                TempData["MensagemSucesso"] = "Venda excluída com sucesso.";
            }
            else
            {
                TempData["MensagemErro"] = "Erro ao excluir a venda.";
            }
            return RedirectToAction("Index");
        }

        private string GerarProtocoloVenda()
        {
            var dataAtual = DateTime.Now;
            return dataAtual.ToString("yyyyMMdd-HHmmss");
        }

        private VendasViewModel MapearParaViewModel(VendasModel venda)
        {
            return new VendasViewModel
            {
                VendaId = venda.VendaId,
                ClienteID = venda.ClienteID,
                DataVenda = venda.DataVenda,
                ValorTotal = venda.ValorTotal,
                UsuarioID = venda.UsuarioID,
                FabricanteID = venda.FabricanteID,
                VeiculoID = venda.VeiculoID,
            };
        }

        private void PrepararDadosDropdowns()
        {
            ViewBag.Clientes = _clientesRepositorio.BuscarTodos().Select(c => new SelectListItem
            {
                Value = c.ClienteID.ToString(),
                Text = c.Nome
            }).ToList();

            ViewBag.Concessionarias = _concessionariasRepositorio.BuscarTodos().Select(co => new SelectListItem
            {
                Value = co.ConcessionariaID.ToString(),
                Text = co.Nome
            }).ToList(); 

            ViewBag.Veiculos = _veiculosRepositorio.BuscarTodos().Select(v => new SelectListItem
            {
                Value = v.VeiculoID.ToString(),
                Text = v.ModeloVeiculo
            }).ToList();

            ViewBag.Fabricantes = _fabricantesRepositorio.BuscarTodos().Select(f => new SelectListItem
            {
                Value = f.FabricanteID.ToString(),
                Text = f.NomeFabricante
            }).ToList();

            ViewBag.Usuarios = _usuariosRepositorio.ObterTodosUsuarios().Select(u => new SelectListItem
            {
                Value = u.UsuarioID.ToString(),
                Text = u.NomeUsuario
            }).ToList();
        }

    }
}
