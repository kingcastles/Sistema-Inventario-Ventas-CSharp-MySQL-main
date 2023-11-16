﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas
{
    public class Estado
    {
        int idEstado { get; set; }
        int estado { get; set; }
        string descripcion { get; set; }

        public Estado(int idEstado, int estado, string descripcion)
        {
            this.idEstado = idEstado;
            this.estado = estado;
            this.descripcion = descripcion;
        }

        public string toString()
        {
            return this.estado + " - " + this.descripcion;
        }
    }
}
