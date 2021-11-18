using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Models
{
    public class Diretor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public DateTime? DataNascimento { get; set; }
    }
}
