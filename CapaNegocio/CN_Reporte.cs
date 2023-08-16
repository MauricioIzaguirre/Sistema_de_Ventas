using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objCD_Reporte = new CD_Reporte();
        public List<ReporteCompra> ReporteCompra(string fechaInicio, string fechaFin, int idProveedor)
        {
            return objCD_Reporte.ReporteCompra(fechaInicio,fechaFin,idProveedor);
        }
        public List<ReporteVentas> ReporteVenta(string fechaInicio, string fechaFin)
        {
            return objCD_Reporte.ReporteVenta(fechaInicio, fechaFin);
        }
    }
}
