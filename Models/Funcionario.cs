
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonografiasIfma.Models
{
    [Table("funcionario")]
    public class Funcionario : Usuario
    {
        [Required]
        [DisplayName("Código do Funcionário")]
        public string Codigo { get; set; } //login do funcionário

        
        [Required]
        [DisplayName("Tipo de Usuário")]
        public int UserType { get; set; } // 1 - chefe; 2 - funcionário; 3 - aluno; 4 - professor;

    }
}
