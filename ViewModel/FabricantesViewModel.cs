using Intelectah.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Intelectah.ViewModel
{
    public class FabricantesViewModel
    {
        public int FabricanteID { get; set; }

        [Required(ErrorMessage = "O nome do fabricante é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do fabricante não pode exceder 100 caracteres.")]
        public string NomeFabricante { get; set; }

        [Required(ErrorMessage = "O país de origem é obrigatório")]
        [StringLength(50, ErrorMessage = "O país de origem não pode exceder 50 caracteres.")]
        public string PaisOrigem { get; set; }

        [Required(ErrorMessage = "O ano de fundação é obrigatório")]
        public int AnoFundacao { get; set; }

        [Required(ErrorMessage = "A URL é obrigatória")]
        [StringLength(255, ErrorMessage = "A URL não pode conter mais de 255 caracteres")]
        public string URL { get; set; }

        public IEnumerable<SelectListItem> OpcaoPaises { get; set; }

        public FabricantesViewModel()
        {

            OpcaoPaises = new List<SelectListItem>();
        }

        public FabricantesViewModel(int fabricanteID, string nomeFabricante, string paisOrigem, int anoFundacao, string url, IEnumerable<SelectListItem> opcaoPaises)
        {
            FabricanteID = fabricanteID;
            NomeFabricante = nomeFabricante;
            PaisOrigem = paisOrigem;
            AnoFundacao = anoFundacao;
            URL = url;
            OpcaoPaises = opcaoPaises;
        }
    }
}
