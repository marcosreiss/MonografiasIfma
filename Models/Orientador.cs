using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonografiasIfma.Models
{
    [Table("orientador")]
    public class Orientador : Persona
    {
        public Orientador()
        {
            this.Monografias = new List<Monografia>();
        }

        [Required(ErrorMessage = "*")]
        public string Siap {  get; set; }


        public virtual ICollection<Monografia> Monografias { get; set; }

    }
}
