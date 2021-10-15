using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EEnlaces
    {
        public int IdEnlace { get; set; }
        public int IdOficina { get; set; }
        public int IdRed { get; set; }
        public int IdProveedor { get; set; }
        public string Comentario { get; set; }

        public virtual EOficinas Oficinas { get; set; }
        public virtual EProveedores Proveedores { get; set; }
        public virtual ERedes Redes { get; set; }
    }
}
