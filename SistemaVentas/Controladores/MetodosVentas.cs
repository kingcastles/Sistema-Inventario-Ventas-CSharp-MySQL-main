using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace SistemaVentas.Clases
{
    //Clase para los metodos de ventas
    public class MetodosVentas
    {
        //Conexion con la base de datos
        MySqlConnection sqlConexion = new MySqlConnection();
       
        //Agregar una venta realizada
        public string AgregarVenta(DateTime idVenta, DateTime fechaVenta, int idusuario, List<Venta> listaVenta, double totalVenta)
        {
            string respuesta = "";
            string idProductos = obtenerIdListaProductos(listaVenta);
            try
            {
                //Conexion con la tabla ventas
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("InsertarVenta", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idVenta", MySqlDbType.DateTime).Value = idVenta;
                comando.Parameters.Add("@fechaVenta", MySqlDbType.DateTime).Value = fechaVenta;
                comando.Parameters.Add("@idUsuario", MySqlDbType.Int32).Value = idusuario;
                comando.Parameters.Add("@idProductos", MySqlDbType.MediumText).Value = idProductos;
                comando.Parameters.Add("@total", MySqlDbType.Double).Value = totalVenta;
                sqlConexion.Open();
                //Validando ejecucion
                if (comando.ExecuteNonQuery() == 1)
                {
                    respuesta = "SE INSERTO CORRECTAMENTE";
                }
                else
                {
                    respuesta = "NO SE PUDO INSERTAR EL REGISTRO";
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Fin de la conexion
                if (sqlConexion.State == ConnectionState.Open)
                {
                    sqlConexion.Close();
                }
            }
        }
        
        //Actualizar la cantidad de un productos en el inventario
        public void ActualizaStock(List<Venta> listaVenta)
        {
            for (int i = 0; i < listaVenta.Count; i++)
            {
                try
                {
                    //Conexion con la tabla productos
                    sqlConexion = new Conexion().obtenerConexion();
                    MySqlCommand comando = new MySqlCommand("ActualizarStock", sqlConexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@codigo", MySqlDbType.VarChar).Value = listaVenta.ElementAt(i).producto.idProducto.Trim();
                    comando.Parameters.Add("@cantidad", MySqlDbType.Int32).Value = listaVenta.ElementAt(i).cantidad;

                    sqlConexion.Open();
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //Fin de la conexion
                    if (sqlConexion.State == ConnectionState.Open)
                    {
                        sqlConexion.Close();
                    }
                }
            }
        }

        //Obtener la lista de productos existente en el almacen
        public string obtenerIdListaProductos(List<Venta> listaVenta)
        {
            string identificadores = "";

            for (int i = 0; i < listaVenta.Count; i++)
            {
                if (i == 0)
                {
                    identificadores = listaVenta.ElementAt(i).cantidad + "-" + listaVenta.ElementAt(i).producto.idProducto;
                } 
                else
                {
                    identificadores = identificadores + "," + listaVenta.ElementAt(i).cantidad + "-" + listaVenta.ElementAt(i).producto.idProducto;
                }
            }

            return identificadores;
        }
    }
}
