using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ERol
    {
        public int IdRol { get; set; }
        [Required]
        public string Rol { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
