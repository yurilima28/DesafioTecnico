﻿using Intelectah.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Intelectah.Models
{
    public class UsuariosModel
    {
        [Key]
        public int UsuarioID { get; set; }

        public string Login { get; set; }
        public bool SenhaValida(string senha)
        {
            return Senha  == senha;

        }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O nome de usuário não pode exceder 50 caracteres.")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MaxLength(255, ErrorMessage = "A senha não pode exceder 255 caracteres.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O email não pode exceder 100 caracteres.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O nível de acesso é obrigatório.")]
        public PerfilEnum NivelAcesso { get; set; }

        public UsuariosModel() { }

        public UsuariosModel(string nomeUsuario, string senha, string email, PerfilEnum nivelAcesso, string login)
        {
            NomeUsuario = nomeUsuario;
            Senha = senha;
            Email = email;
            NivelAcesso = nivelAcesso;
            Login = login;
        }

    }

}
