
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MonografiasIfma.Models
{
    public class Funcionario : Usuario
    {
        [Required]
        [DisplayName("Código do Funcionário")]
        public string Codigo { get; set; } //login do funcionário


    }
}
