using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace SistemaVentas.Clases
{
    //Clase para los metodos de Proveedores
    public class MetodosProveedores
    {
        //Conexion con la base de datos
        MySqlDataReader resultado;
        DataTable tabla = new DataTable();
        MySqlConnection sqlConexion = new MySqlConnection();

        /*Metodos*/

        //Obtener todos los proveedores registrados
        public DataTable ObtenerProveedores()
        {
            try
            {
                //Conexion con la base de datos para cargar los datos
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("ObtenerProveedores", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                sqlConexion.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Finalizar la conexion
                if (sqlConexion.State == ConnectionState.Open)
                {
                    sqlConexion.Close();
                }
            }
        }

        //Agregar un nuevo proveedor del negocio
        public string AgregarProveedor(Proveedor proveedor)
        {
            string respuesta = "";

            try 
            { 
                //Conexion con la base de datos a la tabla proveedores
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("InsertarProveedor", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nomProveedor", MySqlDbType.VarChar).Value = proveedor.nomProveedor;
                comando.Parameters.Add("@numContacto", MySqlDbType.VarChar).Value = proveedor.numContacto;
                comando.Parameters.Add("@direccion", MySqlDbType.VarChar).Value = proveedor.direccion;
                comando.Parameters.Add("@email", MySqlDbType.VarChar).Value = proveedor.email;
                sqlConexion.Open();
                //Validacion de ejecucion
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
                //Finalizar la conexion
                if (sqlConexion.State == ConnectionState.Open)
                {
                    sqlConexion.Close();
                }
            }
        }

        //Buscar datos de un proveedor
        public DataTable BuscarProveedor(string nombre)
        {
            try
            {
                //Conexion con la base de datos a la tabla proveedores
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("BuscarProveedor", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@proveedor", MySqlDbType.VarChar).Value = nombre.Trim();
                sqlConexion.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Finalizar la conexion
                if (sqlConexion.State == ConnectionState.Open)
                {
                    sqlConexion.Close();
                }
            }
        }

        //Actualizar datos de un proveedor
        public string ActualizarProveedor(int idProveedor, Proveedor proveedor)
        {
            string respuesta = "";

            try
            {
                //Conexion con la base de datos a la tabla proveedores
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("ActualizarProveedor", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@id", MySqlDbType.Int32).Value = idProveedor;
                comando.Parameters.Add("@nomProveedor", MySqlDbType.VarChar).Value = proveedor.nomProveedor;
                comando.Parameters.Add("@numContacto", MySqlDbType.VarChar).Value = proveedor.numContacto;
                comando.Parameters.Add("@direccion", MySqlDbType.VarChar).Value = proveedor.direccion;
                comando.Parameters.Add("@email", MySqlDbType.VarChar).Value = proveedor.email;
                sqlConexion.Open();
                //Validacion de ejecucion
                if (comando.ExecuteNonQuery() == 1)
                {
                    respuesta = "SE ACTUALIZO CORRECTAMENTE";
                }
                else
                {
                    respuesta = "NO SE PUDO ACTUALIZAR EL REGISTRO";
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Finalizar la conexion
                if (sqlConexion.State == ConnectionState.Open)
                {
                    sqlConexion.Close();
                }
            }
        }

        //Validacion de campos vacios a la hora del ingreso de datos del producto
        public string ValidarProveedor(Proveedor proveedor)
        {
            string resultado = null;
            //Validadcion de la ejecucion
            if (proveedor.nomProveedor.Equals("") || proveedor.numContacto.Equals("") || proveedor.direccion.Equals("") || proveedor.email.Equals(""))
            {
                resultado = "TE FALTA LLENAR Y/O SELECCIONAR UN CAMPO";
            }
            else
            {
                resultado = "OK";
            }

            return resultado;
        }
    }
}
