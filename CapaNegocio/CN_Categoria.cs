using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objCD_Categoria = new CD_Categoria();
        public List<Categoria> Listar()
        {
            return objCD_Categoria.Listar();
        }
        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripcion de la Categoria\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCD_Categoria.Registrar(obj, out Mensaje);
            }
        }
        public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripcion de la Categoria\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCD_Categoria.Editar(obj, out Mensaje);
            }

        }
        public bool Eliminar(Categoria obj, out string Mensaje)
        {
                return objCD_Categoria.Eliminar(obj, out Mensaje);
        }
    }
}
