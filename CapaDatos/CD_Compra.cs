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
    public class CD_Compra
    {
        public int ObtenerCorrelativo()
        {
            int idCorrelativo = 0;

            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from COMPRA");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    idCorrelativo =  Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    idCorrelativo = 0;
                }
            }
            return idCorrelativo;
        }
        public bool RegistrarCompra(Compra objCompra, DataTable detalleCompra, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand command = new SqlCommand("SP_Registrar_Compra", objConexion);
                    command.Parameters.AddWithValue("@IdUsuario", objCompra.objUsuario.IdUsuario);
                    command.Parameters.AddWithValue("@IdProveedor", objCompra.objProveedor.IdProveedor);
                    command.Parameters.AddWithValue("@TipoDocumento", objCompra.TipoDocumento);
                    command.Parameters.AddWithValue("@NumeroDocumento", objCompra.NumeroDocumento);
                    command.Parameters.AddWithValue("@MontoTotal", objCompra.MontoTotal);
                    command.Parameters.AddWithValue("@DetalleCompra", detalleCompra);
                    command.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    command.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(command.Parameters["@Resultado"].Value);
                    mensaje = command.Parameters["@Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    mensaje = ex.Message;
                }
            }
            return respuesta;
        }
        public Compra ObtenerCompra(string numero)
        {
            Compra obj = new Compra();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select c.IdCompra,u.NombreCompleto,p.Documento,p.RazonSocial,c.TipoDocumento,");
                    query.AppendLine("c.NumeroDocumento,c.MontoTotal,convert(char(10),c.FechaRegistro,103)[FechaRegistro]");
                    query.AppendLine("from COMPRA c inner join USUARIO u on c.IdUsuario = u.IdUsuario inner join ");
                    query.AppendLine("PROVEEDOR p on p.IdProveedor = c.IdProveedor where c.NumeroDocumento = @numero");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("@numero", numero);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj = new Compra()
                            {
                                IdCompra = Convert.ToInt32(reader["IdCompra"]),
                                objUsuario = new Usuario() { NombreCompleto = reader["NombreCompleto"].ToString()},
                                objProveedor = new Proveedor() { Documento = reader["Documento"].ToString(), RazonSocial = reader["RazonSocial"].ToString() },
                                TipoDocumento = reader["TipoDocumento"].ToString(),
                                NumeroDocumento = reader["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(reader["MontoTotal"]),
                                FechaRegistro = reader["FechaRegistro"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new Compra();
                }

            }
            return obj;
        }
        public List<Detalle_Compra> ObtenerDetalleCompra(int IdCompra)
        {
            List<Detalle_Compra> oList = new List<Detalle_Compra>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.Nombre,d.PrecioCompra,d.Cantidad,d.MontoTotal");
                    query.AppendLine("from DETALLE_COMPRA d inner join PRODUCTO p on ");
                    query.AppendLine("p.IdProducto = d.IdProducto where d.IdCompra = @numero ");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("@numero", IdCompra);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            oList.Add(new Detalle_Compra()
                            {
                                objProducto = new Producto() { Nombre = reader["Nombre"].ToString() },
                                PrecioCompra = Convert.ToDecimal(reader["PrecioCompra"].ToString()),
                                Cantidad = Convert.ToInt32(reader["Cantidad"].ToString()),
                                MontoTotal = Convert.ToDecimal(reader["MontoTotal"].ToString()),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    oList = new List<Detalle_Compra>();
                }
                return oList;
            }
        }
    }
}
