using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netpoints.Models
{
    public class ERolViewModel
    {
        public int IdRol { get; set; }
        public string Rol { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        #region "Usuario"
        public bool radusu { get; set; }
        public bool checkusuA { get; set; }
        public bool checkusuE { get; set; }
        public bool checkusuD { get; set; }
        #endregion
        #region "Rol"
        public bool radrol { get; set; }
        public bool checkrolA { get; set; }
        public bool checkrolE { get; set; }
        public bool checkrolD { get; set; }
        #endregion
        #region "Proveedor"
        public bool radprov { get; set; }
        public bool checkprovA { get; set; }
        public bool checkprovE { get; set; }
        public bool checkprovD { get; set; }
        #endregion
        #region "Redes"
        public bool radred { get; set; }
        public bool checkredA { get; set; }
        public bool checkredE { get; set; }
        public bool checkredD { get; set; }
        #endregion
        #region "Sitios"
        public bool radsitios { get; set; }
        public bool checksitA { get; set; }
        public bool checksitE { get; set; }
        public bool checksitD { get; set; }
        #endregion
        public bool repproveedor { get; set; }
        public bool repubicacion { get; set; }
        public bool repusuarios { get; set; }
        public bool bitreg { get; set; }
        public bool bitcam { get; set; }
    }
}