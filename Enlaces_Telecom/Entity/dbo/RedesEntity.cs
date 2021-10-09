using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class RedesEntity : EN
    {
        public int? IdRed { get; set; }
        public string Linea { get; set; }
        public string Gateway { get; set; }
        public string Interface { get; set; }
        public string TipoEnlace { get; set; }
        public string Bandwidth { get; set; }
        public string MedioEnlace { get; set; }
        public string Comentario { get; set; }
    }
}
