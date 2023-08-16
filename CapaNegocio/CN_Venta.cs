using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objCD_Venta = new CD_Venta();
        public int ObtenerCorrelativo()
        {
            return objCD_Venta.ObtenerCorrelativo();
        }
        public bool RestarStock(int idProducto, int cantidad)
        {
            return objCD_Venta.RestarStock(idProducto, cantidad);
        }
        public bool SumarStock(int idProducto, int cantidad)
        {
            return objCD_Venta.SumarStock(idProducto, cantidad);
        }
        public bool RegistrarVenta(Venta objVenta, DataTable detalleVenta, out string mensaje)
        {
            return objCD_Venta.RegistrarVenta(objVenta, detalleVenta, out mensaje);
        }
        public Venta ObtenerVenta(string numero)
        {
            Venta objVenta = objCD_Venta.ObtenerVenta(numero);
            if (objVenta.IdVenta != 0)
            {
                List<Detalle_Venta> detalle_Ventas = objCD_Venta.ObtenerDetalleVenta(objVenta.IdVenta);
                objVenta.objListDetalleVenta = detalle_Ventas;
            }
            return objVenta;
        }
    }
}
