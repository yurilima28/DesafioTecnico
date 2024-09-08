using Intelectah.Dapper;
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
        private readonly BancoContext _bancoContext;

        public VendasController(
            IVendasRepositorio vendasRepositorio,
            IClientesRepositorio clientesRepositorio,
            IConcessionariasRepositorio concessionariasRepositorio,
            IVeiculosRepositorio veiculosRepositorio,
            IFabricantesRepositorio fabricantesRepositorio,
            IUsuariosRepositorio usuariosRepositorio,
            BancoContext bancoContext)
        {
            _vendasRepositorio = vendasRepositorio;
            _clientesRepositorio = clientesRepositorio;
            _concessionariasRepositorio = concessionariasRepositorio;
            _veiculosRepositorio = veiculosRepositorio;
            _fabricantesRepositorio = fabricantesRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
            _bancoContext = bancoContext;
        }

        public IActionResult Index()
        {
            var vendas = _vendasRepositorio.BuscarTodos();
            var vendasViewModel = vendas.Select(v => new VendasViewModel
            {
                VendaId = v.VendaId,
                ClienteID = v.ClienteID,
                Cliente = v.Cliente, 
                DataVenda = v.DataVenda,
                ValorTotal = v.ValorTotal,
                UsuarioID = v.UsuarioID,
                Usuario = v.Usuario, 
                ConcessionariaID = v.ConcessionariaID,
                Concessionaria = v.Concessionaria,
                FabricanteID = v.FabricanteID,
                Fabricante = v.Fabricante, 
                VeiculoID = v.VeiculoID,
                Veiculo = v.Veiculo, 
            }).ToList();

            return View(vendasViewModel);
        }

        public IActionResult Criar()
        {
            var viewModel = new VendasViewModel
            {
                ProtocoloVenda = GerarProtocoloVenda()
            };
            PrepararDadosDropdowns();
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
        public IActionResult Criar(VendasViewModel vendasViewModel)
        {
            if (ModelState.IsValid)
            {
                var novaVenda = new VendasModel
                {
                    ClienteID = vendasViewModel.ClienteID,
                    DataVenda = vendasViewModel.DataVenda,
                    ValorTotal = vendasViewModel.ValorTotal,
                    UsuarioID = vendasViewModel.UsuarioID,
                    ConcessionariaID = vendasViewModel.ConcessionariaID,
                    FabricanteID = vendasViewModel.FabricanteID,
                    VeiculoID = vendasViewModel.VeiculoID,
                    ProtocoloVenda = GerarProtocoloVenda()
                };

                _vendasRepositorio.Adicionar(novaVenda);

                TempData["MensagemSucesso"] = "Venda criada com sucesso!";
                return RedirectToAction("Index");
            }

            PrepararDadosDropdowns();
            return View(vendasViewModel);
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

                AtualizarVenda(venda, vendaViewModel);
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
            TempData["MensagemSucesso"] = apagou ? "Venda excluída com sucesso." : "Erro ao excluir a venda.";
            return RedirectToAction("Index");
        }

        public JsonResult ObterModelosPorFabricante(int fabricanteId)
        {
            var modelos = _bancoContext.Veiculos
                .Where(m => m.FabricanteID == fabricanteId)
                .Select(m => new { id = m.VeiculoID, nome = m.ModeloVeiculo })
                .ToList();

            return Json(modelos);
        }

        private string GerarProtocoloVenda()
        {
            return DateTime.Now.ToString("yyyyMMdd-HHmmss");
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
                ConcessionariaID = venda.ConcessionariaID,
                FabricanteID = venda.FabricanteID,
                VeiculoID = venda.VeiculoID,
                ProtocoloVenda = venda.ProtocoloVenda,
            };
        }

        private void AtualizarVenda(VendasModel venda, VendasViewModel vendaViewModel)
        {
            venda.ClienteID = vendaViewModel.ClienteID;
            venda.DataVenda = vendaViewModel.DataVenda;
            venda.ValorTotal = vendaViewModel.ValorTotal;
            venda.UsuarioID = vendaViewModel.UsuarioID;
            venda.ConcessionariaID = vendaViewModel.ConcessionariaID;
            venda.FabricanteID = vendaViewModel.FabricanteID;
            venda.VeiculoID = vendaViewModel.VeiculoID;
        }

        private void PrepararDadosDropdowns()
        {
            ViewBag.Clientes = ObterSelectList(_clientesRepositorio.BuscarTodos(), c => c.ClienteID.ToString(), c => c.Nome);
            ViewBag.Concessionarias = ObterSelectList(_concessionariasRepositorio.BuscarTodos(), co => co.ConcessionariaID.ToString(), co => co.Nome);
            ViewBag.Fabricantes = ObterSelectList(_fabricantesRepositorio.BuscarTodos(), f => f.FabricanteID.ToString(), f => f.NomeFabricante);
            ViewBag.Usuarios = ObterSelectList(_usuariosRepositorio.ObterTodosUsuarios(), u => u.UsuarioID.ToString(), u => u.NomeUsuario);
            ViewBag.Modelos = _veiculosRepositorio.BuscarTodos().Select(v => new SelectListItem
            {
                Value = v.VeiculoID.ToString(),
                Text = v.ModeloVeiculo
            }).ToList();
        }

        private IEnumerable<SelectListItem> ObterSelectList<T>(IEnumerable<T> items, Func<T, string> valueSelector, Func<T, string> textSelector)
        {
            return items.Select(item => new SelectListItem
            {
                Value = valueSelector(item),
                Text = textSelector(item)
            }).ToList();
        }
    }
}
