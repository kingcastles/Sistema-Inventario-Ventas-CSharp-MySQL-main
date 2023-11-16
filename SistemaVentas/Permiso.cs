﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas
{
    public class Permiso
    {
        int idPermiso { get; set; }
        string permiso { get; set; }

        public Permiso(int idPermiso, string permiso)
        {
            this.idPermiso = idPermiso;
            this.permiso = permiso;
        }

        public string toString()
        {
            return this.idPermiso+" - "+this.permiso;
        }
    }
}
