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
    public class CD_Categoria
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdCategoria,Estado,Descripcion from CATEGORIA");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                Descripcion = reader["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            }); ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Categoria>();
                }

            }
            return lista;
        }
        public int Registrar(Categoria obj, out string Mensaje)
        {
            int idCategoriaGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand command = new SqlCommand("SP_Registrar_Categoria", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    command.Parameters.AddWithValue("@Estado", obj.Estado);
                    //Parametros de Salida
                    command.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    command.ExecuteNonQuery();
                    idCategoriaGenerado = Convert.ToInt32(command.Parameters["@Resultado"].Value);
                    Mensaje = command.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idCategoriaGenerado = 0;
                Mensaje = ex.Message;
            }



            return idCategoriaGenerado;
        }
        public bool Editar(Categoria obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {
                   //@IdCategoria int,
                   //@Descripcion varchar(50),
                   //@Resultado bit output,
                   //@Mensaje varchar(500) output
                    SqlCommand command = new SqlCommand("SP_Editar_Categoria", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@IdCategoria", obj.IdCategoria);
                    command.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    command.Parameters.AddWithValue("@Estado", obj.Estado);

                    //Parametros de Salida
                    command.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    command.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(command.Parameters["@Resultado"].Value);
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
        public bool Eliminar(Categoria obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand command = new SqlCommand("SP_Eliminar_Categoria", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@IdCategoria", obj.IdCategoria);
                    //Parametros de Salida
                    command.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    command.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(command.Parameters["@Resultado"].Value);
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
