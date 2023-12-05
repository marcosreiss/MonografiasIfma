﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonografiasIfma.Models
{
    [Table("Aluno")]
    public class Aluno : Usuario
    {
        [Required(ErrorMessage = "*")]
        public string Matricula { get; set; }

        public virtual ICollection<Monografia>? Monografias { get; set; }

    }
}
