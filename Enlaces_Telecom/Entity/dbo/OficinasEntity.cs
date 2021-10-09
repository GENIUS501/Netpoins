using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class OficinasEntity : EN
    {
        public int? IdOficina { get; set; }
        public string NombreOficina { get; set; }
        public string UE { get; set; }
        public string Provincia { get; set; }
        public string Comentario { get; set; }
    }
}
