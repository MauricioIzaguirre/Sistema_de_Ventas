using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System;
using System.Net.NetworkInformation;

namespace CapaDatos
{
    public class CD_Licencia
    {
        private static string ObtenerMAC()
        {
            string DireccionMAC = string.Empty;
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet || networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    DireccionMAC = networkInterface.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return DireccionMAC;
        }
        public bool Registrar_Licencia(string Codigo, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand command = new SqlCommand("SP_RegistrarLicencia", objConexion);
                    //Parametros de Entrada
                    command.Parameters.AddWithValue("@MAC", ObtenerMAC());
                    command.Parameters.AddWithValue("@CODIGO", Codigo);
                    //Parametros de Salida
                    command.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    command.CommandType = CommandType.StoredProcedure;
                    objConexion.Open();
                    command.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(command.Parameters["@Resultado"].Value);
                    Mensaje = command.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
        public int ComprobarLicencia()
        {
            int CantDias = 0;
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" select CantDias from LICENCIAS_ADQUIRIDAS ");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    CantDias = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    CantDias = 0;
                }
            }
            return CantDias;
        }
        public string ComprobarFechaCompra()
        {
            string FechaCompra = string.Empty;
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" select FechaCompra from LICENCIAS_ADQUIRIDAS");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    FechaCompra = Convert.ToDateTime(command.ExecuteScalar()).ToString("yyyyMMdd");
                }
                catch (Exception ex)
                {
                    FechaCompra = string.Empty;
                }
            }
            return FechaCompra;
        }
        public bool Actualizar(string fecha)
        {
            bool actualizar = false;
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand command = new SqlCommand("SP_Actualizar", objConexion);
                //Parametros de Entrada
                command.Parameters.AddWithValue("@Fecha", fecha);
                command.Parameters.AddWithValue("@MAC", ObtenerMAC());
                //Parametros de Salida
                command.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                objConexion.Open();
                command.ExecuteNonQuery();
                actualizar = Convert.ToBoolean(command.Parameters["@Resultado"].Value);
            }

            return actualizar;
        }
        
    }
}
