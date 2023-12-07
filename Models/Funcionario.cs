
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MonografiasIfma.Models
{
    public class Funcionario : Usuario
    {
        [Required]
        [DisplayName("Código do Funcionário")]
        public string Codigo { get; set; } //login do funcionário

        [DisplayName("Tipo de Usuário")]
        [Required]
        public int UserType { get; set; } // 1 - chefe; 2 - funcionário; 3 - aluno; 4 - professor;
    }
}
