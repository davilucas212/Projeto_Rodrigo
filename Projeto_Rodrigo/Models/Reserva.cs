using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Projeto_Rodrigo.Models
{
    public class Reserva
    {
            
            [Key]
            public int Id { get; set; }

            [Required]
            public DateTime Inicio { get; set; }

            [Required]
            public DateTime Fim { get; set; }

            [Range(1,10)]
            public int SalaId { get; set; }

            public Sala? Sala { get; set; }


        }
}

