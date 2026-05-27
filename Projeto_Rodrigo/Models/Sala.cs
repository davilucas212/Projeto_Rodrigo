using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_Rodrigo.Models
{
    public class Sala
    {
        
        {         public int Id { get; set; }

            public string Nome { get; set; }

            public int Andar { get; set; }

            public int QuantidadeAssentos { get; set; }

            public List<Reserva>? Reservas { get; set; }
   
    }
}
