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
    public class CN_Compra
    {
        private CD_Compra objCD_Compra = new CD_Compra();
        public int ObtenerCorrelativo()
        {
            return objCD_Compra.ObtenerCorrelativo();
        }
        public bool RegistrarCompra(Compra obj,DataTable detalleCompra, out string Mensaje)
        {
                return objCD_Compra.RegistrarCompra(obj,detalleCompra, out Mensaje);
        }
        public Compra ObtenerCompra(string numero)
        {
            Compra objCompra = objCD_Compra.ObtenerCompra(numero);
            if(objCompra.IdCompra != 0)
            {
                List<Detalle_Compra> detalle_Compras = objCD_Compra.ObtenerDetalleCompra(objCompra.IdCompra);
                objCompra.objListaDetalleCompra = detalle_Compras;
            }
            return objCompra;
        }
    }
}
