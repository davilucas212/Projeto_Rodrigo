using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Projeto_Rodrigo.Models
{
    public class Sala
    {
            [Key]
            public int Id { get; set; }

            [Required]
            [StringLength(70)]
            public string Nome { get; set; }
            [Required]
            [Range(1, 10)]    
            
            public int Andar { get; set; }

            [Required]
            [Range(1, 30)]
            public int QuantidadeAssentos { get; set; }

            public List<Reserva>? Reservas { get; set; }
   
    }
}
