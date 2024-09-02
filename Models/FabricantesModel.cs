using Intelectah.Dapper;
using System.ComponentModel.DataAnnotations;
using Intelectah.Models;
using static ValidationModel;

namespace Intelectah.Models
{
    public class FabricantesModel
    {
        [Key]
        public int FabricanteID { get; set; }

        [Required(ErrorMessage = "O nome do fabricante é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do fabricante não pode exceder 100 caracteres.")]
        [UniqueNomeFabricante]
        public string NomeFabricante { get; set; }

        [Required(ErrorMessage = "O país de origem é obrigatório")]
        [StringLength(50, ErrorMessage = "O país de origem não pode exceder 50 caracteres.")]
        public string PaisOrigem { get; set; }

        [Required(ErrorMessage = "O ano de fundação é obrigatório")]
        [AnoAtual]
        public int AnoFundacao { get; set; }

        [Required(ErrorMessage = "A URL é obrigatória")]
        [StringLength(255, ErrorMessage = "A URL não pode conter mais de 255 carcteres")]
        public string URL { get; set; }
        public ICollection<VeiculosModel> Veiculos { get; set; }

        public FabricantesModel(int fabricanteID, string nomeFabricante, string paisOrigem, int anoFundacao, string url)
        {
            FabricanteID = fabricanteID;
            NomeFabricante = nomeFabricante;
            PaisOrigem = paisOrigem;
            AnoFundacao = anoFundacao;
            URL = url;
        }
        public FabricantesModel()
        {
        }
    }
}
