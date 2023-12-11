using System.ComponentModel.DataAnnotations;

namespace MonografiasIfma.Models
{
    public class RegistroViewModel
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme sua senha")]
        [Compare("Senha", ErrorMessage = "As senhas são diferentes")]
        public string? ConfirmarSenha { get; set; }
    }
}
