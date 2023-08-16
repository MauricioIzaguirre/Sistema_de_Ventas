using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Proveedor
    {
        private CD_Proveedor objCD_Proveedor = new CD_Proveedor();
        public List<Proveedor> Listar()
        {
            return objCD_Proveedor.Listar();
        }
        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Proveedor\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCD_Proveedor.Registrar(obj, out Mensaje);
            }
        }
        public bool Editar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Proveedor\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCD_Proveedor.Editar(obj, out Mensaje);
            }

        }
        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            return objCD_Proveedor.Eliminar(obj, out Mensaje);
        }
    }
}
