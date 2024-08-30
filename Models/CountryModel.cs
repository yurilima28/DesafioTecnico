namespace Intelectah.Models
{
    public class CountryModel
    {
        public string Cca2 { get; set; } 
        public Name Name { get; set; }   
    }

    public class Name
    {
        public string Common { get; set; }   
    }
}
