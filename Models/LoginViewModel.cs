using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MonografiasIfma.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Usuário Obrigatório")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }

        [DisplayName("Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
