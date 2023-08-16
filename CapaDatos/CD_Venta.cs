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
    public class CD_Venta
    {
        public int ObtenerCorrelativo()
        {
            int idCorrelativo = 0;

            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from VENTA");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    idCorrelativo = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    idCorrelativo = 0;
                }
            }
            return idCorrelativo;
        }
        public bool RestarStock(int idProducto,int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PRODUCTO set Stock = Stock - @cantidad where IdProducto = @IdProducto");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("@cantidad",cantidad);
                    command.Parameters.AddWithValue("@IdProducto", idProducto);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    respuesta = command.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
        public bool SumarStock(int idProducto, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PRODUCTO set Stock = Stock + @cantidad where IdProducto = @IdProducto");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("@cantidad", cantidad);
                    command.Parameters.AddWithValue("@IdProducto", idProducto);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    respuesta = command.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
        public bool RegistrarVenta(Venta objVenta, DataTable detalleVenta, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand command = new SqlCommand("SP_Registrar_Venta", objConexion);
                    command.Parameters.AddWithValue("@IdUsuario", objVenta.objUsuario.IdUsuario);
                    command.Parameters.AddWithValue("@TipoDocumento", objVenta.TipoDocumento);
                    command.Parameters.AddWithValue("@NumeroDocumento", objVenta.NumeroDocumento);
                    command.Parameters.AddWithValue("@DocumentoCliente", objVenta.DocumentoCliente);
                    command.Parameters.AddWithValue("@NombreCliente", objVenta.NombreCliente);
                    command.Parameters.AddWithValue("@MontoPago", objVenta.MontoPago);
                    command.Parameters.AddWithValue("@MontoCambio", objVenta.MontoCambio);
                    command.Parameters.AddWithValue("@MontoTotal", objVenta.MontoTotal);
                    command.Parameters.AddWithValue("@DetalleVenta", detalleVenta);
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
        public Venta ObtenerVenta(string numero)
        {
            Venta obj = new Venta();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.IdVenta,u.NombreCompleto,v.DocumentoCliente,v.NombreCliente,v.TipoDocumento,");
                    query.AppendLine("v.NumeroDocumento,v.MontoPago,v.MontoCambio,v.MontoTotal,convert(char(10),v.FechaRegistro,103)[FechaRegistro]");
                    query.AppendLine("from VENTA v inner join USUARIO u on v.IdUsuario = u.IdUsuario where v.NumeroDocumento = @numero");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("@numero", numero);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj = new Venta()
                            {
                                IdVenta = Convert.ToInt32(reader["IdVenta"]),
                                objUsuario = new Usuario() { NombreCompleto = reader["NombreCompleto"].ToString() },
                                DocumentoCliente = reader["DocumentoCliente"].ToString(),
                                NombreCliente = reader["NombreCliente"].ToString(),
                                TipoDocumento = reader["TipoDocumento"].ToString(),
                                NumeroDocumento = reader["NumeroDocumento"].ToString(),
                                MontoPago = Convert.ToDecimal(reader["MontoPago"].ToString()),
                                MontoCambio = Convert.ToDecimal(reader["MontoCambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(reader["MontoTotal"].ToString()),
                                FechaRegistro = reader["FechaRegistro"].ToString(),
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new Venta();
                }

            }
            return obj;
        }
        public List<Detalle_Venta> ObtenerDetalleVenta(int IdVenta)
        {
            List<Detalle_Venta> oList = new List<Detalle_Venta>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Select p.Nombre,p.PrecioVenta,dv.Cantidad,dv.SubTotal");
                    query.AppendLine("from DETALLE_VENTA dv inner join PRODUCTO p on ");
                    query.AppendLine("p.IdProducto = dv.IdProducto where dv.IdVenta = @IdVenta ");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("@IdVenta", IdVenta);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            oList.Add(new Detalle_Venta()
                            {
                                objProducto = new Producto() { Nombre = reader["Nombre"].ToString() },
                                PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"].ToString()),
                                Cantidad = Convert.ToInt32(reader["Cantidad"].ToString()),
                                SubTotal = Convert.ToDecimal(reader["SubTotal"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    oList = new List<Detalle_Venta>();
                }
                return oList;
            }
        }
    }
}
