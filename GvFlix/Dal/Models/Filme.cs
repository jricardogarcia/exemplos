using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Models
{
    public class Filme
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public DateTime DataLancamento { get; set; }

        [Required]
        public DateTime Duracao { get; set; }

        public string Resumo { get; set; }
    }
}
