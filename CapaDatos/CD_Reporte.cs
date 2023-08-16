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
    public class CD_Reporte
    {
        public List<ReporteCompra> ReporteCompra(string fechaInicio,string fechaFin, int idProveedor)
        {
            List<ReporteCompra> lista = new List<ReporteCompra>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                   
                    SqlCommand command = new SqlCommand("SP_ReporteCompra", objConexion);
                    command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", fechaFin);
                    command.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ReporteCompra()
                            {
                                FechaRegistro = reader["FechaRegistro"].ToString(),
                                TipoDocumento = reader["TipoDocumento"].ToString(),
                                NumeroDocumento = reader["NumeroDocumento"].ToString(),
                                MontoTotal = reader["MontoTotal"].ToString(),
                                UsuarioRegistrado = reader["UsuarioRegistro"].ToString(),
                                DocumentoProveedor = reader["DocumentoProveedor"].ToString(),
                                RazonSocial = reader["RazonSocial"].ToString(),
                                CodigoProducto = reader["CodigoProducto"].ToString(),
                                NombreProducto = reader["NombreProducto"].ToString(),
                                Categoria = reader["Categoria"].ToString(),
                                PrecioCompra = reader["PrecioCompra"].ToString(),
                                PrecioVenta = reader["PrecioVenta"].ToString(),
                                Cantidad = reader["Cantidad"].ToString(),
                                SubTotal = reader["SubTotal"].ToString(),

                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ReporteCompra>();
                }

            }
            return lista;
        }
        public List<ReporteVentas> ReporteVenta(string fechaInicio, string fechaFin)
        {
            List<ReporteVentas> lista = new List<ReporteVentas>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    SqlCommand command = new SqlCommand("SP_ReporteVenta", objConexion);
                    command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", fechaFin);
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ReporteVentas()
                            {
                                FechaRegistro = reader["FechaRegistro"].ToString(),
                                TipoDocumento = reader["TipoDocumento"].ToString(),
                                NumeroDocumento = reader["NumeroDocumento"].ToString(),
                                MontoTotal = reader["MontoTotal"].ToString(),
                                UsuarioRegistrado = reader["UsuarioRegistro"].ToString(),
                                DocumentoCliente = reader["DocumentoCliente"].ToString(),
                                NombreCliente = reader["NombreCliente"].ToString(),
                                CodigoProducto = reader["CodigoProducto"].ToString(),
                                NombreProducto = reader["NombreProducto"].ToString(),
                                Categoria = reader["Categoria"].ToString(),
                                PrecioVenta = reader["PrecioVenta"].ToString(),
                                Cantidad = reader["Cantidad"].ToString(),
                                SubTotal = reader["SubTotal"].ToString(),

                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ReporteVentas>();
                }

            }
            return lista;
        }
    }
}
