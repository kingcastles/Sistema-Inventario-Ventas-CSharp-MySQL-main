using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace SistemaVentas.Clases
{
    //Clase para los metodos de usuario
    public class MetodosUsuarios
    {
        //Conexion con la base de datos
        MySqlDataReader resultado;
        DataTable tabla = new DataTable();
        MySqlConnection sqlConexion = new MySqlConnection();

        //Metodo para obtener a los usuarios registrados
        public DataTable ObtenerUsuarios()
        {
            try
            {
                //Conexion con la tabla de usuarios
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("ObtenerUsuarios", sqlConexion);
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
                //Finalizanbdo la conexion con la base de datos
                if (sqlConexion.State == ConnectionState.Open)
                {
                    sqlConexion.Close();
                }
            }
        }

        //Obtener los tipos de permisos registrados en la base de dtaos
        public List<Permiso> ObtenerPermisos()
        {
            List<Permiso> listaPermisos = new List<Permiso>();

            try
            {
                //Conexion con la tabla permisos
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("ObtenerPermisos", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                sqlConexion.Open();
                resultado = comando.ExecuteReader();
                //Ibteniendo los permisos
                while (resultado.Read())
                {
                    listaPermisos.Add(
                        new Permiso(
                            resultado.GetInt32(0),
                            resultado.GetString(1)
                            )
                        );
                }

                return listaPermisos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Finalizando la conexion
                if (sqlConexion.State == ConnectionState.Open)
                {
                    sqlConexion.Close();
                }
            }
        }

        //Obtener los tipos de estados registrados en la base de datos
        public List<Estado> ObtenerEstados()
        {
            List<Estado> listaEstados = new List<Estado>();

            try
            {
                //Conexion con la tabla estados
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("ObtenerEstados", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                sqlConexion.Open();
                resultado = comando.ExecuteReader();
                //Obteniendo los tipos de estados
                while (resultado.Read())
                {
                    listaEstados.Add(
                        new Estado(
                            resultado.GetInt32(0),
                            resultado.GetInt32(1),
                            resultado.GetString(2)
                            )
                        );
                }

                return listaEstados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Finalizando la conexion con la base de datos
                if (sqlConexion.State == ConnectionState.Open)
                {
                    sqlConexion.Close();
                }
            }
        }

        //Validacion de campos vacios al momento de registrar al usuario
        public string ValidarUsuario(Usuario usuario)
        {
            string resultado = null;

            if (usuario.nomUsuario.Equals("") || usuario.usuario.Equals("") || usuario.contrasena.Equals("") || usuario.idPermiso == 0 || usuario.idEstado == 0)
            {
                resultado = "TE FALTA LLENAR Y/O SELECCIONAR UN CAMPO";
            } else
            {
                resultado = "OK";
            }

            return resultado;
        }

        //Metodo para registrar un nuevo usuario
        public string AgregarUsuario(Usuario usuario)
        {
            string respuesta = "";

            try
            {
                //Conexion con la tabla usuarios
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("InsertarUsuario", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nomUsuario", MySqlDbType.VarChar).Value = usuario.nomUsuario;
                comando.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = usuario.usuario;
                comando.Parameters.Add("@contrasena", MySqlDbType.VarChar).Value = usuario.contrasena;
                comando.Parameters.Add("@idPermiso", MySqlDbType.Int32).Value = usuario.idPermiso;
                comando.Parameters.Add("@idEstado", MySqlDbType.Int32).Value = usuario.idEstado;
                sqlConexion.Open();
                //Validando la ejecucion
                if(comando.ExecuteNonQuery() == 1)
                {
                    respuesta = "SE INSERTO CORRECTAMENTE";
                } else
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

        //Metodo´para actualizar los datos de un usuario
        public string ActualizarUsuario(int idUsuario, Usuario usuario)
        {
            string respuesta = "";

            try
            {
                //Conexion con la tabla usuarios
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("ActualizarUsuario", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@id", MySqlDbType.Int32).Value = idUsuario;
                comando.Parameters.Add("@nomUsuario", MySqlDbType.VarChar).Value = usuario.nomUsuario;
                comando.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = usuario.usuario;
                comando.Parameters.Add("@contrasena", MySqlDbType.VarChar).Value = usuario.contrasena;
                comando.Parameters.Add("@idPermiso", MySqlDbType.Int32).Value = usuario.idPermiso;
                comando.Parameters.Add("@idEstado", MySqlDbType.Int32).Value = usuario.idEstado;
                sqlConexion.Open();
                //Validando la ejecucion
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
                //Fin de la conexion
                if (sqlConexion.State == ConnectionState.Open)
                {
                    sqlConexion.Close();
                }
            }
        }

        //Metodo para buscar un usario ya registrado
        public DataTable BuscarUsuario(string nombre)
        {
            try
            {
                //Conexion con la tabla usuarios
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("BuscarUsuario", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = nombre.Trim();
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
                //Fin de la conexion
                if (sqlConexion.State == ConnectionState.Open)
                {
                    sqlConexion.Close();
                }
            }
        }

        //Metodo para restringir el acceso del sistema
        public Usuario LoginUsuario(string usuario, string contrasena)
        {
            Usuario user = new Usuario();

            try
            {
                //Conectando con la tabla usuarios
                sqlConexion = new Conexion().obtenerConexion();
                MySqlCommand comando = new MySqlCommand("LoginUsuario", sqlConexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@user", MySqlDbType.VarChar).Value = usuario.Trim();
                comando.Parameters.Add("@pass", MySqlDbType.VarChar).Value = contrasena.Trim();
                sqlConexion.Open();
                resultado = comando.ExecuteReader();
                //Valindacion de contraseña y usuraio
                while (resultado.Read())
                {
                    if (resultado.Equals(""))
                    {
                        user.idUsuario = 0;
                    }
                    else
                    {

                        user = new Usuario(
                            resultado.GetInt32(0),
                            resultado.GetString(1),
                            resultado.GetString(2),
                            resultado.GetString(3),
                            resultado.GetInt32(4),
                            resultado.GetInt32(5)
                            );
                    }
                }
                return user;
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
}
