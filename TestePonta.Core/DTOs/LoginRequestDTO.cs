using System.ComponentModel.DataAnnotations;

namespace TestePonta.Core.DTOs
{
    /// <summary>
    /// Classe que representa uma solicitação de login, contendo informações de e-mail e senha.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Obtém ou define o endereço de e-mail do usuário.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Obtém ou define a senha do usuário.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
