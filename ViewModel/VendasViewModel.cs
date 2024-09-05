using Intelectah.Models;

namespace Intelectah.ViewModel
{
    public class VendasViewModel
    {
        public int VendaId { get; set; }
        public int ClienteID { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public int UsuarioID { get; set; }
        public string NomeCliente { get; set; }  
        public string NomeUsuario { get; set; }
        public int ConcessionariaID { get; set; }
        public string NomeConcessionaria { get; set; }
        public int FabricanteID { get; set; }
        public int VeiculoID { get; set; }
        
        public virtual ICollection<UsuariosModel> Usuarios { get; set; }
        public virtual ICollection<VeiculosModel> Veiculos { get; set; }
        public virtual ICollection<ConcessionariasModel> Concessionarias { get; set; }
        public virtual ICollection<FabricantesModel> Fabricantes { get; set; }

        public VendasViewModel()
        {
        }
        public VendasViewModel(int vendaId, int clienteId, DateTime dataVenda, decimal valorTotal, int usuarioId, string nomeCliente, string nomeUsuario, string nomeConcessionaria, int fabricanteId)
        {
            VendaId = vendaId;
            ClienteID = clienteId;
            DataVenda = dataVenda;
            ValorTotal = valorTotal;
            UsuarioID = usuarioId;
            NomeCliente = nomeCliente;
            NomeUsuario = nomeUsuario;
            NomeConcessionaria = nomeConcessionaria;
            FabricanteID = fabricanteId;
        }
    }
}
