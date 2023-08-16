using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objCD_Usuario = new CD_Usuario();
        public List<Usuario> Listar()
        {
            return objCD_Usuario.Listar();
        }
        public int Registrar(Usuario obj,out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del usuario\n";
            }
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }
            if (obj.Clave == "")
            {
                Mensaje += "Es necesaria la clave del usuario\n";
            }
            if(Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCD_Usuario.Registrar(obj, out Mensaje);
            }
        }
        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del usuario\n";
            }
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }
            if (obj.Clave == "")
            {
                Mensaje += "Es necesaria la clave del usuario\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCD_Usuario.Editar(obj, out Mensaje);
            }
            
        }
        public bool Eliminar(Usuario obj, out string Mensaje)
        {
                return objCD_Usuario.Eliminar(obj, out Mensaje);     
        }
    }
}
