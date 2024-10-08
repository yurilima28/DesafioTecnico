﻿using Intelectah.Enums;
using Intelectah.Models;
using Intelectah.Repositorio;
using Intelectah.ViewModel;
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

            var veiculosViewModel = veiculos.Select(v => new VeiculosViewModel
            {
                VeiculoID = v.VeiculoID,
                ModeloVeiculo = v.ModeloVeiculo,
                AnoFabricacao = v.AnoFabricacao,
                ValorVeiculo = v.ValorVeiculo,
                FabricanteID = v.FabricanteID,
                Tipo = v.Tipo,
                Descricao = v.Descricao,
                Fabricantes = fabricantes.Select(f => new SelectListItem
                {
                    Value = f.FabricanteID.ToString(),
                    Text = f.NomeFabricante
                }).ToList()
            }).ToList();

            return View(veiculosViewModel);
        }

        public IActionResult Criar()
        {
            var fabricantes = _fabricantesRepositorio.BuscarTodos();

            var viewModel = new VeiculosViewModel
            {
                Fabricantes = fabricantes.Select(f => new SelectListItem
                {
                    Value = f.FabricanteID.ToString(),
                    Text = f.NomeFabricante
                }).ToList(),
                TiposVeiculos = Enum.GetValues(typeof(TipoVeiculo))
                    .Cast<TipoVeiculo>()
                    .Select(t => new SelectListItem
                    {
                        Value = t.ToString(),
                        Text = t.ToString()
                    })
                    .ToList()
            };

            return View(viewModel);
        }

        public IActionResult Editar(int id)
        {
            var veiculo = _veiculosRepositorio.ListarPorId(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            var fabricantes = _fabricantesRepositorio.BuscarTodos();

            var viewModel = new VeiculosViewModel
            {
                VeiculoID = veiculo.VeiculoID,
                ModeloVeiculo = veiculo.ModeloVeiculo,
                AnoFabricacao = veiculo.AnoFabricacao,
                ValorVeiculo = veiculo.ValorVeiculo,
                FabricanteID = veiculo.FabricanteID,
                Tipo = veiculo.Tipo,
                Descricao = veiculo.Descricao,

                Fabricantes = fabricantes.Select(f => new SelectListItem
                {
                    Value = f.FabricanteID.ToString(),
                    Text = f.NomeFabricante
                }).ToList(),

                TiposVeiculos = Enum.GetValues(typeof(TipoVeiculo))
                    .Cast<TipoVeiculo>()
                    .Select(t => new SelectListItem
                    {
                        Value = t.ToString(),
                        Text = t.ToString()
                    })
                    .ToList()
            };

            return View(viewModel);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _veiculosRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Veículo deletado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Não foi possível deletar o veículo.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível deletar o veículo, tente novamente. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            var veiculo = _veiculosRepositorio.ListarPorId(id);
            if (veiculo == null)
            {
                TempData["MensagemErro"] = "Veículo não encontrado.";
                return RedirectToAction("Index");
            }

            var viewModel = new VeiculosViewModel
            {
                VeiculoID = veiculo.VeiculoID,
                ModeloVeiculo = veiculo.ModeloVeiculo,
                AnoFabricacao = veiculo.AnoFabricacao,
                ValorVeiculo = veiculo.ValorVeiculo,
                FabricanteID = veiculo.FabricanteID,
                Tipo = veiculo.Tipo
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Criar(VeiculosViewModel viewModel)
        {
            if (viewModel.ModeloVeiculo.Length > 100)
            {
                ModelState.AddModelError(nameof(viewModel.ModeloVeiculo), "O modelo do veículo não pode exceder 100 caracteres.");
            }
            if (viewModel.AnoFabricacao > DateTime.Now.Year)
            {
                ModelState.AddModelError(nameof(viewModel.AnoFabricacao), "O ano de fabricação não pode ser no futuro.");
            }

            if (ModelState.IsValid)
            {
                var veiculo = new VeiculosModel
                {
                    VeiculoID = viewModel.VeiculoID,
                    ModeloVeiculo = viewModel.ModeloVeiculo,
                    AnoFabricacao = viewModel.AnoFabricacao,
                    ValorVeiculo = viewModel.ValorVeiculo,
                    FabricanteID = viewModel.FabricanteID,
                    Tipo = viewModel.Tipo,
                    Descricao = viewModel.Descricao
                };

                _veiculosRepositorio.Adicionar(veiculo);
                TempData["MensagemSucesso"] = "Veículo cadastrado com sucesso";
                return RedirectToAction("Index");
            }

            var fabricantes = _fabricantesRepositorio.BuscarTodos();
            viewModel.Fabricantes = fabricantes.Select(f => new SelectListItem
            {
                Value = f.FabricanteID.ToString(),
                Text = f.NomeFabricante
            }).ToList();

            viewModel.TiposVeiculos = Enum.GetValues(typeof(TipoVeiculo))
                .Cast<TipoVeiculo>()
                .Select(t => new SelectListItem
                {
                    Value = t.ToString(),
                    Text = t.ToString()
                })
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(VeiculosViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var veiculo = new VeiculosModel
                {
                    VeiculoID = viewModel.VeiculoID,
                    ModeloVeiculo = viewModel.ModeloVeiculo,
                    AnoFabricacao = viewModel.AnoFabricacao,
                    ValorVeiculo = viewModel.ValorVeiculo,
                    FabricanteID = viewModel.FabricanteID,
                    Tipo = viewModel.Tipo,
                    Descricao = viewModel.Descricao,
                };

                _veiculosRepositorio.Atualizar(veiculo);
                TempData["MensagemSucesso"] = "Veículo alterado com sucesso";
                return RedirectToAction("Index");
            }

            var fabricantes = _fabricantesRepositorio.BuscarTodos();
            viewModel.Fabricantes = fabricantes.Select(f => new SelectListItem
            {
                Value = f.FabricanteID.ToString(),
                Text = f.NomeFabricante
            }).ToList();

            viewModel.TiposVeiculos = Enum.GetValues(typeof(TipoVeiculo))
                .Cast<TipoVeiculo>()
                .Select(t => new SelectListItem
                {
                    Value = t.ToString(),
                    Text = t.ToString()
                })
                .ToList();

            return View(viewModel);
        }       
    }
}
