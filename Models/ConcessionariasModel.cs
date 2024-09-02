using System.ComponentModel.DataAnnotations;
using static ValidationModel;

namespace Intelectah.Models
{
    public class ConcessionariasModel
    {
        [Key]
        public int ConcessionariaID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O nome da concessionária deve ter no máximo 100 caracteres.")]
        [UniqueNomeConcessionaria]
        public string Nome { get; set; }

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
        public int CapacidadeMax{ get; set; }

        public ConcessionariasModel(int concessionariaID, string nome, string enderecoCompleto, string cidade, string estado, string cep, string telefone, string email, int capacidadeMax)
        {
            ConcessionariaID = concessionariaID;
            Nome = nome;
            EnderecoCompleto = enderecoCompleto;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            Telefone = telefone;
            Email = email;
            CapacidadeMax = capacidadeMax;
        }

        public ConcessionariasModel() { }

    }
}
