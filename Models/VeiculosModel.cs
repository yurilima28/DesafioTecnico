using Intelectah.Enums;
using System.ComponentModel.DataAnnotations;
using static ValidationModel;

namespace Intelectah.Models
{
    public class VeiculosModel
    {
        [Key]
        public int VeiculoID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O nome do modelo não pode exceder 100 caracteres.")]
        public string ModeloVeiculo { get; set; }

        [Required(ErrorMessage = "O ano de fabricação é obrigatório")]
        [AnoFabricacao]
        public int AnoFabricacao { get; set; }

        [Required]
        [Range(0.01, 9999999999.99, ErrorMessage = "O valor do veículo deve ser um valor entre 0,01 e 9.999.999.999.")]
        public decimal ValorVeiculo { get; set; }

        [Required(ErrorMessage = "O tipo de veículo é obrigatório")]
        public TipoVeiculo Tipo { get; set; }

        [StringLength(500, ErrorMessage = "A descrição não pode conter mais de 500 caracteres")]
        public string Descricao { get; set; }

        [Required]
        public int FabricanteID { get; set; }

        public FabricantesModel Fabricantes { get; set; }

        public VeiculosModel(int veiculoID, string modeloVeiculo, int anoFabricacao, decimal valorVeiculo, TipoVeiculo tipo, string descricao, int fabricanteID)
        {
            VeiculoID = veiculoID;
            ModeloVeiculo = modeloVeiculo;
            AnoFabricacao = anoFabricacao;
            ValorVeiculo = valorVeiculo;
            Tipo = tipo;
            Descricao = descricao;
            FabricanteID = fabricanteID;
        }

        public VeiculosModel()
        {
        }
    }
}
