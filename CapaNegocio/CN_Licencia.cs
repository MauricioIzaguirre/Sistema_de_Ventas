using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Licencia
    {
        private CD_Licencia objCD_Licencia = new CD_Licencia();
        public bool Registrar_Licencia(string Codigo, out string Mensaje)
        {
            return objCD_Licencia.Registrar_Licencia( Codigo, out Mensaje);
        }
        public int ComprobarLicencia()
        {
            return objCD_Licencia.ComprobarLicencia();
        }
        public string ComprobarFechaCompra()
        {
            return objCD_Licencia.ComprobarFechaCompra();
        }
        public bool Actualizar(string fecha)
        {
            return objCD_Licencia.Actualizar(fecha);
        }
    }
}
