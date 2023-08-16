using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Negocio
    {
        private CD_Negocio objCD_Negocio = new CD_Negocio();
        public Negocio ObtenerDatos()
        {
            return objCD_Negocio.ObtenerDatos();
        }
        public bool GuardarDatos(Negocio obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del Negocio\n";
            }
            if (obj.RUC == "")
            {
                Mensaje += "Es necesario el RUC del Negocio\n";
            }
            if (obj.Direccion == "")
            {
                Mensaje += "Es necesaria la direccion del Negocio\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCD_Negocio.GuardarDatos(obj, out Mensaje);
            }
        }
        public byte[] ObtenerLogo(out bool obtenido)
        {
            return objCD_Negocio.ObtenerLogo(out obtenido);
        }
        public bool ActualizarLogo(byte[] image,out string mensaje)
        {
            return objCD_Negocio.ActualizarLogo(image ,out mensaje);
        }
    }
}
