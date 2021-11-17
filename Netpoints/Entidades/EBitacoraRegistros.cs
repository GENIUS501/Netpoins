using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EBitacoraRegistros
    {
        public int IdRegistro { get; set; }
        public int IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaHoraIngreso { get; set; }
        public Nullable<System.DateTime> FechaHoraSalida { get; set; }
    }
}
