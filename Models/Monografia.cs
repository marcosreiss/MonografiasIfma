using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonografiasIfma.Models
{
    [Table("Monografias")]
    public class Monografia
    {
        [Key]
        public int Id { get; set; }



        [Required(ErrorMessage = "*")]
        public string Titulo { get; set; }



        [Required(ErrorMessage = "*")]
        public DateTime DataApresentacao { get; set; }



        public int QtPaginas { get; set; }



        [DisplayName("PDF da monografia")]
        public byte[]? Pdf_ArquivoBinario { get; set; }



        //  [Required(ErrorMessage = "*")]           comentado para fins de testes 
        [DisplayName("Aluno")]
        [ForeignKey("Aluno_Id")]
        public int AlunoId { get; set; }

        public  Aluno? Aluno { get; set; }



        //   [Required(ErrorMessage = "*")]        comentado para fins de testes
        [DisplayName("Professor Orientador")]
        [ForeignKey("orientador_id")]
        public int OrientadorId { get; set; }

        public Orientador? Orientador { get; set; }

    }
}
