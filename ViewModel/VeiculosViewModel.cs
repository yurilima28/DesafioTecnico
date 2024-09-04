﻿using Intelectah.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intelectah.ViewModel
{
    public class VeiculosViewModel
    {
        public int VeiculoID { get; set; }
        public string ModeloVeiculo { get; set; }
        public int AnoFabricacao { get; set; }
        public decimal ValorVeiculo { get; set; }
        public TipoVeiculo Tipo { get; set; }
        public string Descricao { get; set; }
        public int FabricanteID { get; set; }
        public int ConcessionariaID { get; set; }
        public string NomeConcessionaria { get; set; }
        public IEnumerable<SelectListItem> Fabricantes { get; set; }
        public IEnumerable<SelectListItem> TiposVeiculos { get; set; }
        public IEnumerable<SelectListItem> Concessionarias { get; set; }

        public VeiculosViewModel(int veiculoID, string modeloVeiculo, int anoFabricacao, decimal valorVeiculo, TipoVeiculo tipo, string descricao, int fabricanteID, string nomeConcessionaria)
        {
            VeiculoID = veiculoID;
            ModeloVeiculo = modeloVeiculo;
            AnoFabricacao = anoFabricacao;
            ValorVeiculo = valorVeiculo;
            Tipo = tipo;
            Descricao = descricao;
            FabricanteID = fabricanteID;
            NomeConcessionaria = nomeConcessionaria;
        }

        public VeiculosViewModel()
        {
            Fabricantes = new List<SelectListItem>();
            TiposVeiculos = new List<SelectListItem>();
            Concessionarias = new List<SelectListItem>();

        }
        public VeiculosViewModel(int veiculoID, string modeloVeiculo, int anoFabricacao, decimal valorVeiculo, TipoVeiculo tipo, string descricao, int fabricanteID, IEnumerable<SelectListItem> fabricantes, IEnumerable<SelectListItem> tiposVeiculos, IEnumerable<SelectListItem> concessionarias, string nomeConcessionaria)
        {
            VeiculoID = veiculoID;
            ModeloVeiculo = modeloVeiculo;
            AnoFabricacao = anoFabricacao;
            ValorVeiculo = valorVeiculo;
            Tipo = tipo;
            Descricao = descricao;
            FabricanteID = fabricanteID;
            Fabricantes = fabricantes;
            TiposVeiculos = tiposVeiculos;
            Concessionarias = concessionarias;
            NomeConcessionaria = nomeConcessionaria;

        }
    }
}