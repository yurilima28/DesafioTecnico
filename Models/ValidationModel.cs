using Intelectah.Repositorio;
using System.ComponentModel.DataAnnotations;
using Intelectah.Dapper;

public class ValidationModel
{

    public class UniqueNomeFabricanteAttribute : ValidationAttribute
    {
        public UniqueNomeFabricanteAttribute()
        {
            ErrorMessage = "O nome do fabricante já está em uso.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var fabricantesRepositorio = (IFabricantesRepositorio)validationContext.GetService(typeof(IFabricantesRepositorio));
                string nomeFabricante = value.ToString();

                var existingFabricante = fabricantesRepositorio?.ObterPorNome(nomeFabricante);
                if (existingFabricante != null)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }

    public class UniqueNomeConcessionariaAttribute : ValidationAttribute
    {
        public UniqueNomeConcessionariaAttribute()
        {
            ErrorMessage = "O nome da concessonária já está em uso.";
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var concessionariaRepositorio = (IConcessionariasRepositorio)validationContext.GetService(typeof(IConcessionariasRepositorio));
                string nomeConcessionaria = value.ToString();

                var existingConcessionaria = concessionariaRepositorio?.ListarTodosAsync();
                if (existingConcessionaria != null)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }

    public class AnoAtualAttribute : ValidationAttribute
    {
        public AnoAtualAttribute()
        {
            ErrorMessage = "O ano de fundação deve ser menor ou igual ao ano atual.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && int.TryParse(value.ToString(), out int ano))
            {
                if (ano > DateTime.Now.Year)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}