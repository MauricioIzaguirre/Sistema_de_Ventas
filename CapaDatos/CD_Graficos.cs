using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Globalization;

namespace CapaDatos
{
    public struct Ganancias
    {
        public string Fecha { get; set; }
        public decimal TotalGanancia { get; set; }
    }
    public class CD_Graficos
    {
        public List<Detalle_Venta> ListGanancias(DateTime fechaInicio,DateTime fechaFin, int numeroDias)
        {
            List<Detalle_Venta> list = new List<Detalle_Venta>();
            List<Ganancias> listGanancias = new List<Ganancias>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select CONVERT(date,FechaRegistro) as FechaRegistro,sum(SubTotal) as SubTotal from DETALLE_VENTA where");
                    query.AppendLine("FechaRegistro between @FechaInicio and @FechaFin group by CONVERT(date,FechaRegistro)");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("FechaInicio",fechaInicio);
                    command.Parameters.AddWithValue("FechaFin", fechaFin);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listGanancias.Add(new Ganancias()
                            {
                                Fecha = (Convert.ToDateTime(reader["FechaRegistro"])).ToString("ddMMM"),
                                TotalGanancia = Convert.ToDecimal(reader["SubTotal"])
                                //FechaRegistro = (Convert.ToDateTime(reader["FechaRegistro"])).ToString("ddMMM"),
                               // SubTotal = Convert.ToDecimal(reader["SubTotal"])
                            }) ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    list = new List<Detalle_Venta>();
                }
                if (numeroDias <= 1)
                {
                    list = (from orderList in listGanancias
                            group orderList by (Convert.ToDateTime(orderList.Fecha)).ToString("hh tt")
                            into order
                            select new Detalle_Venta
                            {
                                FechaRegistro = order.Key,
                                SubTotal = order.Sum(amount => amount.TotalGanancia)
                            }
                                        ).ToList();

                }
                else if (numeroDias <= 30)
                {
                    foreach (var item in listGanancias)
                    {
                        list.Add(new Detalle_Venta()
                        {
                            FechaRegistro = (Convert.ToDateTime(item.Fecha)).ToString("dd MMM"),
                            SubTotal = item.TotalGanancia
                        });
                    }
                }
                else if (numeroDias <= 92)
                {
                    list = (from orderList in listGanancias
                            group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                            Convert.ToDateTime(orderList.Fecha), CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                        into order
                            select new Detalle_Venta
                            {
                                FechaRegistro = "Week" + order.Key.ToString(),
                                SubTotal = order.Sum(amount => amount.TotalGanancia)
                            }
                                        ).ToList();
                }
                else if (numeroDias <= (365 * 2))
                {
                    bool isYear = numeroDias <= 365 ? true : false;
                    list = (from orderList in listGanancias
                            group orderList by (Convert.ToDateTime(orderList.Fecha)).ToString("MMM yyyy")
                                        into order
                            select new Detalle_Venta
                            {
                                FechaRegistro = isYear ? order.Key.Substring(0, order.Key.IndexOf(" ")) : order.Key,
                                SubTotal = order.Sum(amount => amount.TotalGanancia)
                            }
                                        ).ToList();
                }
                else
                {
                    list = (from orderList in listGanancias
                            group orderList by (Convert.ToDateTime(orderList.Fecha)).ToString("yyyy")
                                        into order
                            select new Detalle_Venta
                            {
                                FechaRegistro = order.Key,
                                SubTotal = order.Sum(amount => amount.TotalGanancia)
                            }
                                        ).ToList();
                }
            }
            return list;
        }
        public List<Producto> BajoStock(){
            List<Producto> lista = new List<Producto>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select top 10 Nombre, Stock from PRODUCTO order by Stock ASC");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Producto()
                            {
                                Nombre = reader["Nombre"].ToString(),
                                Stock = Convert.ToInt32(reader["Stock"])
                            });
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
        public List<Detalle_Venta> MasVendios(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Detalle_Venta> lista = new List<Detalle_Venta>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select top 5 p.Nombre, sum(dv.Cantidad) as Cantidad from PRODUCTO p ");
                    query.AppendLine("inner join DETALLE_VENTA dv on dv.IdProducto = p.IdProducto where dv.FechaRegistro ");
                    query.AppendLine("between @FechaInicio and @FechaFin  group by p.Nombre order by Cantidad desc");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("FechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("FechaFin", fechaFin);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Detalle_Venta()
                            {
                                objProducto = new Producto() { Nombre = reader["Nombre"].ToString() },
                                Cantidad = Convert.ToInt32(reader["Cantidad"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Detalle_Venta>();
                }
            }
            return lista;
        }
        public int CantidadVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            int cantVentas = 0;
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" select count(IdVenta) as Cantidad from VENTA where FechaRegistro between @FechaInicio and @FechaFin ");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", fechaFin);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    cantVentas = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    cantVentas = 0;
                }
            }
            return cantVentas;
        }
        public int CantidadClientes()
        {
            int cant = 0;
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" select count(IdCliente) as Cantidad from CLIENTE ");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    cant = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    cant = 0;
                }
            }
            return cant;
        }
        public int CantidadProveedores()
        {
            int cant = 0;
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" select count(IdProveedor) as Cantidad from PROVEEDOR");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    cant = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    cant = 0;
                }
            }
            return cant;
        }
        public int CantidadProductos()
        {
            int cant = 0;
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(IdProducto) as Cantidad from PRODUCTO");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    cant = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    cant = 0;
                }
            }
            return cant;
        }
        public decimal TotalIngresos(DateTime fechaInicio, DateTime fechaFin)
        {
            decimal ingresos = 0;
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select sum(SubTotal) from DETALLE_VENTA where FechaRegistro between @FechaInicio and @FechaFin");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", fechaFin);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    ingresos = Convert.ToDecimal(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    ingresos = 0;
                }
            }
            return ingresos;
        }
        public decimal TotalGanancias(DateTime fechaInicio, DateTime fechaFin)
        {
            decimal ganancias = 0;
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select sum(dv.SubTotal) - sum(p.PrecioCompra * dv.Cantidad) from DETALLE_VENTA dv");
                    query.AppendLine("inner join PRODUCTO p on dv.IdProducto = p.IdProducto where dv.FechaRegistro");
                    query.AppendLine("between @FechaInicio and @FechaFin");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", fechaFin);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    ganancias = Convert.ToDecimal(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    ganancias = 0;
                }
            }
            return ganancias;
        }
    }
}
