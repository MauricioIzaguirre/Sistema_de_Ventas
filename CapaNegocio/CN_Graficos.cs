using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Graficos
    {
        private CD_Graficos objCD_Graficos = new CD_Graficos();
        public List<Producto> BajoStock()
        {
            return objCD_Graficos.BajoStock();
        }
        public List<Detalle_Venta> MasVendidos(DateTime fechaInicio, DateTime fechaFin)
        {
            return objCD_Graficos.MasVendios(fechaInicio, fechaFin);
        }
        public List<Detalle_Venta> ListGanancias(DateTime fechaInicio, DateTime fechaFin,int numeroDias)
        {
            return objCD_Graficos.ListGanancias(fechaInicio, fechaFin, numeroDias);
        }
        public int CantidadVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            return objCD_Graficos.CantidadVentas(fechaInicio, fechaFin);
        }
        public int CantidadClientes()
        {
            return objCD_Graficos.CantidadClientes();
        }
        public int CantidadProveedores()
        {
            return objCD_Graficos.CantidadProveedores();
        }
        public int CantidadProductos()
        {
            return objCD_Graficos.CantidadProductos();
        }
        public decimal TotalIngresos(DateTime fechaInicio, DateTime fechaFin)
        {
            return objCD_Graficos.TotalIngresos(fechaInicio, fechaFin);
        }
        public decimal TotalGanancias(DateTime fechaInicio, DateTime fechaFin)
        {
            return objCD_Graficos.TotalGanancias(fechaInicio, fechaFin);
        }

    }
}
