using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class ReporteUbicacionEntity : EN
    {
        public int? IdUsuario { get; set; }
        public UsuariosEntity Usuarios { get; set; }
        public int? IdOficina { get; set; }
        public OficinasEntity Oficinas { get; set; }
        public int? IdEnlaces { get; set; }
        public EnlacesEntity Enlaces { get; set; }
        public ReporteUbicacionEntity()
        {
            Usuarios = Usuarios ?? new UsuariosEntity();
            Oficinas = Oficinas ?? new OficinasEntity();
            Enlaces = Enlaces ?? new EnlacesEntity();
        }
    }
}
