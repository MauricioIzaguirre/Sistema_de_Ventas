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
    public class CD_Cliente
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdCliente,Documento,NombreCompleto,Correo,Telefono,Estado from CLIENTE");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                Documento = reader["Documento"].ToString(),
                                NombreCompleto = reader["NombreCompleto"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"]),
                            }); ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Cliente>();
                }

            }
            return lista;
        }
        public int Registrar(Cliente obj, out string Mensaje)
        {
            int idClienteGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {

                        //@Documento varchar(50),
                        //@NombreCompleto varchar(50),
                        //@Correo varchar(50),
                        //@Telefono varchar(50),
                        //@Estado bit,
                        //@Respuesta int output,
                        //@Mensaje varchar(500) output


                    SqlCommand command = new SqlCommand("SP_Registrar_Cliente", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@Documento", obj.Documento);
                    command.Parameters.AddWithValue("@NombreCompleto", obj.NombreCompleto);
                    command.Parameters.AddWithValue("@Correo", obj.Correo);
                    command.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    command.Parameters.AddWithValue("Estado", obj.Estado);

                    //Parametros de Salida
                    command.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    command.ExecuteNonQuery();
                    idClienteGenerado = Convert.ToInt32(command.Parameters["@Respuesta"].Value);
                    Mensaje = command.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idClienteGenerado = 0;
                Mensaje = ex.Message;
            }



            return idClienteGenerado;
        }
        public bool Editar(Cliente obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {

                        //@IdCliente int,
                        //@Documento varchar(50),
                        //@NombreCompleto varchar(50),
                        //@Correo varchar(50),
                        //@Telefono varchar(50),
                        //@Estado bit,
                        //@Respuesta int output,
                        //@Mensaje varchar(500) output


                    SqlCommand command = new SqlCommand("SP_Editar_Cliente", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@IdCliente", obj.IdCliente);
                    command.Parameters.AddWithValue("@Documento", obj.Documento);
                    command.Parameters.AddWithValue("@NombreCompleto", obj.NombreCompleto);
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
        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("delete from CLIENTE where IdCliente = @IdCliente");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("@IdCliente", obj.IdCliente);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    respuesta = command.ExecuteNonQuery() > 0 ? true : false;
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
