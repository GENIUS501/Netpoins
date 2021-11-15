using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EBitacoraCambios
    {
        public int IdCambio { get; set; }
        public int IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaHora { get; set; }
        public string Tipo { get; set; }
        public string Detalle { get; set; }
    }
}
