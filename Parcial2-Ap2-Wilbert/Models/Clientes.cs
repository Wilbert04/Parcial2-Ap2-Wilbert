using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial2_Ap2_Wilbert.Models
{
    public class Clientes
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nombres { get; set; }

    }
}
