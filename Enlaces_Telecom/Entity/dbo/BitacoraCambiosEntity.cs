using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BitacoraCambiosEntity: EN
    {
        public int? IdCambio { get; set; }
        public int? IdUsuario { get; set; }
        public UsuariosEntity Usuarios { get; set; }
        public DateTime FechaHora { get; set; }
        public string Tipo { get; set; }
        public string Detalle { get; set; }
        public BitacoraCambiosEntity()
        {
            Usuarios = Usuarios ?? new UsuariosEntity();
        }
    }


}
