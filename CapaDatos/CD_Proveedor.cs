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
    public class CD_Proveedor
    {
        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdProveedor,Documento,RazonSocial,Correo,Telefono,Estado from PROVEEDOR");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Proveedor()
                            {
                                IdProveedor = Convert.ToInt32(reader["IdProveedor"]),
                                Documento = reader["Documento"].ToString(),
                                RazonSocial = reader["RazonSocial"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"]),
                            }); ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Proveedor>();
                }

            }
            return lista;
        }
        public int Registrar(Proveedor obj, out string Mensaje)
        {
            int idProveedorGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {

                           //@Documento varchar(50),
                            //@RazonSocial varchar(50),
                            //@Correo varchar(50),
                            //@Telefono varchar(50),
                            //@Estado bit,
                            //@Respuesta int output,
                            //@Mensaje varchar(500) output


                    SqlCommand command = new SqlCommand("SP_Registrar_Proveedor", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@Documento", obj.Documento);
                    command.Parameters.AddWithValue("@RazonSocial", obj.RazonSocial);
                    command.Parameters.AddWithValue("@Correo", obj.Correo);
                    command.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    command.Parameters.AddWithValue("Estado", obj.Estado);

                    //Parametros de Salida
                    command.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    command.ExecuteNonQuery();
                    idProveedorGenerado = Convert.ToInt32(command.Parameters["@Respuesta"].Value);
                    Mensaje = command.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idProveedorGenerado = 0;
                Mensaje = ex.Message;
            }



            return idProveedorGenerado;
        }
        public bool Editar(Proveedor obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {

                    //@IdProveedor int,
                    //@Documento varchar(50),
                    //@RazonSocial varchar(50),
                    //@Correo varchar(50),
                    //@Telefono varchar(50),
                    //@Estado bit,
                    //@Respuesta bit output,
                    //@Mensaje varchar(500) output


                    SqlCommand command = new SqlCommand("SP_Editar_Proveedor", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@IdProveedor", obj.IdProveedor);
                    command.Parameters.AddWithValue("@Documento", obj.Documento);
                    command.Parameters.AddWithValue("@RazonSocial", obj.RazonSocial);
                    command.Parameters.AddWithValue("@Correo", obj.Correo);
                    command.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    command.Parameters.AddWithValue("Estado", obj.Estado);

                    //Parametros de Salida
                    command.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
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
        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {

                    //@IdProveedor int,
                    //@Respuesta bit output,
                    //@Mensaje varchar(500) output
                    SqlCommand command = new SqlCommand("SP_Eliminar_Proveedor", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@IdProveedor", obj.IdProveedor);
                    //Parametros de Salida
                    command.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
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
