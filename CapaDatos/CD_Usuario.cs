using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.IdUsuario,u.Documento,u.NombreCompleto,u.NombreUsuario,u.Correo,u.Clave,u.Estado,r.IdRol,r.Descripcion");
                    query.AppendLine("from Usuario u inner join ROL r on r.IdRol = u.IdRol;");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Documento = reader["Documento"].ToString(),
                                NombreCompleto = reader["NombreCompleto"].ToString(),
                                NombreUsuario = reader["NombreUsuario"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Clave = reader["Clave"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"]),
                                objRol = new Rol() { IdRol = Convert.ToInt32(reader["IdRol"]),Descripcion = reader["Descripcion"].ToString(), }
                            }); ;
                        }
                    }
                }
                catch(Exception ex)
                {
                    lista = new List<Usuario>();
                }
                
            }
            return lista;
        }
        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idUsuarioGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {

                    //Create Proc SP_Registrar_Usuario(
                    //@Documento varchar(50),
                    //@NombreCompleto varchar(100),
                    //@NombreUsuario varchar(100),
                    //@Correo varchar(100),
                    //@Clave varchar(100),
                    //@IdRol int,
                    //@Estado bit,
                    //@IdUsuarioResultado int output,
                    //@Mensaje varchar(500) output
                    //)


                    SqlCommand command = new SqlCommand("SP_Registrar_Usuario", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@Documento", obj.Documento);
                    command.Parameters.AddWithValue("@NombreCompleto", obj.NombreCompleto);
                    command.Parameters.AddWithValue("@NombreUsuario", obj.NombreUsuario);
                    command.Parameters.AddWithValue("@Correo", obj.Correo);
                    command.Parameters.AddWithValue("@Clave", obj.Clave);
                    command.Parameters.AddWithValue("@IdRol", obj.objRol.IdRol);
                    command.Parameters.AddWithValue("Estado", obj.Estado);

                    //Parametros de Salida
                    command.Parameters.Add("@IdUsuarioResultado",SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    command.ExecuteNonQuery();
                    idUsuarioGenerado = Convert.ToInt32(command.Parameters["@IdUsuarioResultado"].Value);
                    Mensaje = command.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idUsuarioGenerado = 0;
                Mensaje = ex.Message;
            }



            return idUsuarioGenerado;
        }
        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {

                    //Create Proc SP_Registrar_Usuario(
                    //@Documento varchar(50),
                    //@NombreCompleto varchar(100),
                    //@NombreUsuario varchar(100),
                    //@Correo varchar(100),
                    //@Clave varchar(100),
                    //@IdRol int,
                    //@Estado bit,
                    //@IdUsuarioResultado int output,
                    //@Mensaje varchar(500) output
                    //)


                    SqlCommand command = new SqlCommand("SP_Editar_Usuario", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    command.Parameters.AddWithValue("@Documento", obj.Documento);
                    command.Parameters.AddWithValue("@NombreCompleto", obj.NombreCompleto);
                    command.Parameters.AddWithValue("@NombreUsuario", obj.NombreUsuario);
                    command.Parameters.AddWithValue("@Correo", obj.Correo);
                    command.Parameters.AddWithValue("@Clave", obj.Clave);
                    command.Parameters.AddWithValue("@IdRol", obj.objRol.IdRol);
                    command.Parameters.AddWithValue("Estado", obj.Estado);

                    //Parametros de Salida
                    command.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    command.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(command.Parameters["@Respuesta"].Value);
                    Mensaje = command.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }



            return respuesta;
        }
        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand command = new SqlCommand("SP_Eliminar_Usuario", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);           
                    //Parametros de Salida
                    command.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    command.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(command.Parameters["@Respuesta"].Value);
                    Mensaje = command.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }



            return respuesta;
        }
    }
}
