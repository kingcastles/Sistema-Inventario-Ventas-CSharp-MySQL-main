using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas
{
    //Clase Permiso
    public class Permiso
    {
        //Metodos Get y Set
        public int idPermiso { get; set; }
        public string permiso { get; set; }

        //Constructor
        public Permiso(int idPermiso, string permiso)
        {
            this.idPermiso = idPermiso;
            this.permiso = permiso;
        }

        //Metodo para formato de permiso
        public string toString()
        {
            return this.idPermiso+" - "+this.permiso;
        }
    }
}
