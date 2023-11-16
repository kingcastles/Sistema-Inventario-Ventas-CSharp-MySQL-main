using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas
{
    //Clase Estados
    public class Estado
    {
        //Metodos Get y Set
        public int idEstado { get; set; }
        public int estado { get; set; }
        public string descripcion { get; set; }

        //Constructor
        public Estado(int idEstado, int estado, string descripcion)
        {
            this.idEstado = idEstado;
            this.estado = estado;
            this.descripcion = descripcion;
        }

        //Metodo para formato de estados
        public string toString()
        {
            return this.estado + " - " + this.descripcion;
        }
    }
}
