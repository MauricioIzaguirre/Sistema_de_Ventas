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
    public class CD_Producto
    {

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.IdProducto,p.Codigo,p.Nombre,p.Descripcion,c.IdCategoria,");
                    query.AppendLine("c.Descripcion[DescripcionCategoria],p.Stock,p.PrecioCompra,p.PrecioVenta,p.Estado from Producto p");
                    query.AppendLine("inner join CATEGORIA c on p.IdCategoria = c.IdCategoria");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                Codigo = reader["Codigo"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                objCategoria = new Categoria() { IdCategoria = Convert.ToInt32(reader["IdCategoria"]), Descripcion = reader["DescripcionCategoria"].ToString()},
                                Stock = Convert.ToInt32(reader["Stock"]),
                                PrecioCompra = Convert.ToDecimal(reader["PrecioCompra"]),
                                PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]),
                                Estado = Convert.ToBoolean(reader["Estado"]),
                                
                            }); ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Producto>();
                }

            }
            return lista;
        }
        public int Registrar(Producto obj, out string Mensaje)
        {
            int idProductoGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {

                   // @Codigo varchar(20),
                   // @Nombre varchar(30),
                   // @Descripcion varchar(30),
                   // @IdCategoria int,
                   // @Estado bit,
                   // @Mensaje varchar(500) output,
                   // @Resultado int output


                    SqlCommand command = new SqlCommand("SP_Registrar_Producto", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@Codigo", obj.Codigo);
                    command.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    command.Parameters.AddWithValue("@IdCategoria", obj.objCategoria.IdCategoria);
                    command.Parameters.AddWithValue("@Estado", obj.Estado);

                    //Parametros de Salida
                    command.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    command.ExecuteNonQuery();
                    idProductoGenerado = Convert.ToInt32(command.Parameters["@Resultado"].Value);
                    Mensaje = command.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idProductoGenerado = 0;
                Mensaje = ex.Message;
            }



            return idProductoGenerado;
        }
        public bool Editar(Producto obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {


                        //@IdProducto int,
                        //@Codigo varchar(20),
                        //@Nombre varchar(30),
                        //@Descripcion varchar(30),
                        //@IdCategoria int,
                        //@Estado bit,
                        //@Mensaje varchar(500) output,
                        //@Resultado bit output


                    SqlCommand command = new SqlCommand("SP_Editar_Producto", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@IdProducto", obj.IdProducto);
                    command.Parameters.AddWithValue("@Codigo", obj.Codigo);
                    command.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    command.Parameters.AddWithValue("@IdCategoria", obj.objCategoria.IdCategoria);
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
        public bool Eliminar(Producto obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand command = new SqlCommand("SP_Eliminar_Producto", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@IdProducto", obj.IdProducto);
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
