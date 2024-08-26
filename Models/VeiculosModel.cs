﻿using Intelectah.Dapper;
using Intelectah.Enums;
using System.ComponentModel.DataAnnotations;

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
        [Range(1, int.MaxValue, ErrorMessage = "O ano de fabricação deve ser um ano válido.")]
        public int AnoFabricacao { get; set; }

        [Required]
        [Range(0.01, 9999999999.99, ErrorMessage = "O valor do veiculo deve ser um valor entre 0,01 e 9.999.999.999.")]
        public decimal ValorVeiculo { get; set; }

        [Required(ErrorMessage = "O tipo de veículo é obrigatório")]
        public TipoVeiculo Tipo { get; set; }

        [StringLength(500, ErrorMessage = "A descrição não pode conter mais de 255 caracteres")]
        public string Descricao { get; set; }

        [Required]
        public int FabricanteID { get; set; }
        public FabricantesModel Fabricante { get; set; }
    }
}
