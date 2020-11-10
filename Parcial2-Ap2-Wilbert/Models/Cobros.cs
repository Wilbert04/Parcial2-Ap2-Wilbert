using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial2_Ap2_Wilbert.Models
{
    public class Cobros
    {
        [Key]
        public int IdCobros { get; set; }
        public int ClienteId { get; set; }
        public DateTime  Fecha { get; set; }
        public int Totales { get; set; }
        public double Cobro{ get; set; }
        public string Observaciones { get; set; }

        [ForeignKey("IdCobros")]
        public virtual List<CobrosDetalle> cobrosDetalle { get; set; } = new List<CobrosDetalle>();

    }


}
