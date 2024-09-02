﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Intelectah.Models;
using static ValidationModel;

namespace Intelectah.ViewModel
{
    public class ConcessionariasViewModel
    {
        public int ConcessionariaID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O nome da concessionária deve ter no máximo 100 caracteres.")]
        [UniqueNomeConcessonaria]
        public string Nome { get; set; }

        [Required]
        public EnderecoViewModel Endereco { get; set; } = new EnderecoViewModel();

        [Required]
        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres.")]
        [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "O telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.")]
        public string Telefone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string Email { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A capacidade máxima de veículos deve ser um valor positivo.")]
        public int CapacidadeMax { get; set; }
    }

    public class EnderecoViewModel
    {
        [Required]
        [StringLength(255, ErrorMessage = "O endereço completo deve ter no máximo 255 caracteres.")]
        public string EnderecoCompleto { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "A cidade deve ter no máximo 50 caracteres.")]
        public string Cidade { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "O estado deve ter no máximo 50 caracteres.")]
        public string Estado { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "O CEP deve ter no máximo 10 caracteres.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato 12345-678.")]
        public string CEP { get; set; }
    }
}

