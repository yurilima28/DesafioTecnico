using System.ComponentModel.DataAnnotations;

namespace Intelectah.Models
{
    public class FabricantesModel
    {
        [Key]
        public int FabricanteID { get; set; }

        [Required(ErrorMessage = "O nome do fabricante é obrigatório")]
        [MaxLength(100)]
        [StringLength(100, ErrorMessage = "O nome do fabricante não pode exceder 100 caracteres.")]
        public string NomeFabricante { get; set; }

        [Required(ErrorMessage = "O país de origem é obrigatório")]
        [MaxLength(50)]
        [StringLength(50, ErrorMessage = "O país de origem não pode exceder 50 caracteres.")]
        public string PaisOrigem { get; set; }

        [Required(ErrorMessage = "O ano de fundação é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O ano de fundação deve ser um ano válido.")]
        [AnoAtual(ErrorMessage = "O ano de fundação não pode ser maior que o ano atual.")]
        public int AnoFundacao { get; set; }

    }

    public class AnoAtualAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int ano)
            {
                return ano <= DateTime.Now.Year;
            }
            return false;
        }
    }
}
