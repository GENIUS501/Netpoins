using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class EnlacesEdit
    {
        public EnlacesEntity enlaces { get; set; }
        public List<OficinasEntity> ddlOficinas { get; set; }
        public List<RedesEntity> ddlRedes { get; set; }
        public List<ProveedoresEntity> ddlProveedores { get; set; }
    }
}