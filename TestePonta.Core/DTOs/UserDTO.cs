using System.Text.Json.Serialization;

namespace TestePonta.Core.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) para representar um usuário.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Obtém ou define o ID do usuário.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém ou define o nome do usuário.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Obtém ou define o endereço de e-mail do usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Obtém ou define a senha do usuário.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Password { get; set; }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UserDTO"/>.
        /// </summary>
        public UserDTO() { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UserDTO"/> com os valores especificados.
        /// </summary>
        /// <param name="id">O ID do usuário.</param>
        /// <param name="name">O nome do usuário.</param>
        /// <param name="email">O endereço de e-mail do usuário.</param>
        /// <param name="password">A senha do usuário.</param>
        public UserDTO(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
    }


}
