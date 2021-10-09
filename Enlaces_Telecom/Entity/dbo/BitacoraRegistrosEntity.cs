using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BitacoraRegistrosEntity:EN
    {
        public int? IdRegistro { get; set; }
        public int? IdUsuario { get; set; }
        public UsuariosEntity Usuarios { get; set; }
        public DateTime FechaHoraIngreso { get; set; }
        public DateTime FechaHoraSalida { get; set; }
        public BitacoraRegistrosEntity()
        {
            Usuarios = Usuarios ?? new UsuariosEntity();
        }
    }
}
