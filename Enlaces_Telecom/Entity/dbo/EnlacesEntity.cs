using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EnlacesEntity : EN
    {
        public int? IdEnlace { get; set; }
        public int? IdOficina { get; set; }
        public OficinasEntity Oficinas { get; set; }
        public int? IdRed { get; set; }
        public RedesEntity Redes { get; set; }
        public int? IdProveedor { get; set; }
        public ProveedoresEntity Proveedores { get; set; }
        public string Comentario { get; set; }
        public EnlacesEntity ()
        {
            Oficinas = Oficinas ?? new OficinasEntity();
            Redes = Redes ?? new RedesEntity();
            Proveedores = Proveedores ?? new ProveedoresEntity();
        }
    }
}
