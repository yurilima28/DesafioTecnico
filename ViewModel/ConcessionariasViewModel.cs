namespace Intelectah.ViewModel
{
    public class ConcessionariasViewModel
    {
        public int ConcessionariaID { get; set; }
        public string Nome { get; set; }
        public EnderecoViewModel Endereco { get; set; } = new EnderecoViewModel();
        public string Telefone { get; set; }    
        public string Email { get; set; }
        public int CapacidadeMax { get; set; }
    }

    public class EnderecoViewModel
    {
        public string EnderecoCompleto { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
    }
}

