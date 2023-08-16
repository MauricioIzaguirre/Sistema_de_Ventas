using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;



namespace CapaDatos
{
    public class CD_Negocio
    {
        public Negocio ObtenerDatos()
        {
            Negocio obj = new Negocio();
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "select IdNegocio,Nombre,RUC,Direccion from  NEGOCIO where IdNegocio = 1";
                    SqlCommand comand = new SqlCommand(query, conexion);
                    comand.CommandType = CommandType.Text;
                    using (SqlDataReader reader = comand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            obj = new Negocio()
                            {
                                IdNegocio = int.Parse(reader["IdNegocio"].ToString()),
                                Nombre = reader["Nombre"].ToString(),
                                RUC = reader["RUC"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                            };
                        }
                    }
                }
            }
            catch
            {
                obj = new Negocio();
            }
            return obj;
        }
        public bool GuardarDatos(Negocio obj, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Nombre = @nombre,");
                    query.AppendLine("RUC = @ruc,");
                    query.AppendLine("Direccion = @direccion");
                    query.AppendLine("where IdNegocio = 1;");
                    SqlCommand comand = new SqlCommand(query.ToString(), conexion);
                    comand.Parameters.AddWithValue("@nombre", obj.Nombre);
                    comand.Parameters.AddWithValue("@ruc", obj.RUC);
                    comand.Parameters.AddWithValue("@direccion", obj.Direccion);
                    comand.CommandType = CommandType.Text;
                    
                    if(comand.ExecuteNonQuery() < 1 )
                    {
                        mensaje = "No se pudieron actualizar los datos \n";
                        respuesta = false;
                    }
                }
            }
            catch(Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;
            }
            return respuesta;
        }
        public byte[] ObtenerLogo(out bool obtenido)
        {
            obtenido = true;
            byte[] LogoBytes = new byte[0];
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "select Logo from  NEGOCIO where IdNegocio = 1;";
                    SqlCommand comand = new SqlCommand(query, conexion);
                    comand.CommandType = CommandType.Text;
                    using (SqlDataReader reader = comand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LogoBytes = (byte[])reader["Logo"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obtenido = false;
                LogoBytes = new byte[0];
            }
            return LogoBytes;
        }
        public bool ActualizarLogo(byte[] image,out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Logo = @image");
                    query.AppendLine("where IdNegocio = 1;");
                    SqlCommand comand = new SqlCommand(query.ToString(), conexion);
                    comand.Parameters.AddWithValue("@image", image);
                    comand.CommandType = CommandType.Text;

                    if (comand.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo actualizar el logo \n";
                        respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;
            }
            return respuesta;
        }
    }
}
