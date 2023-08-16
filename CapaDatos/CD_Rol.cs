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
    public class CD_Rol
    {
        public List<Rol> Listar()
        {
            List<Rol> lista = new List<Rol>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdRol,Descripcion from ROL;");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Rol()
                            {
                                Descripcion = reader["Descripcion"].ToString(),
                                IdRol = Convert.ToInt32(reader["IdRol"])
                            }) ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Rol>();
                }

            }
            return lista;
        }
    }
}
