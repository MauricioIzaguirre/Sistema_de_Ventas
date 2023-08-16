using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objCD_Cliente = new CD_Cliente();
        public List<Cliente> Listar()
        {
            return objCD_Cliente.Listar();
        }
        public int Registrar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del Cliente\n";
            }
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Cliente\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCD_Cliente.Registrar(obj, out Mensaje);
            }
        }
        public bool Editar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del Cliente\n";
            }
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Cliente\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCD_Cliente.Editar(obj, out Mensaje);
            }

        }
        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            return objCD_Cliente.Eliminar(obj, out Mensaje);
        }
    }
}
