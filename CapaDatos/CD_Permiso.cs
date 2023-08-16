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
    public class CD_Permiso
    {
        public List<Permiso> Listar(int id_usuario)
        {
            List<Permiso> lista = new List<Permiso>();
            using (SqlConnection objConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.IdRol,p.NombreMenu from PERMISO p");
                    query.AppendLine("inner join Rol r on r.IdRol = p.IdRol inner join USUARIO u");
                    query.AppendLine("on u.IdRol = r.IdRol where u.IdUsuario = @id_usuario;");
                    SqlCommand command = new SqlCommand(query.ToString(), objConexion);
                    command.Parameters.AddWithValue("@id_usuario", id_usuario);
                    command.CommandType = CommandType.Text;
                    objConexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Permiso()
                            {
                             objRol = new Rol() { IdRol = Convert.ToInt32(reader["IdRol"])},
                             NombreMenu = reader["NombreMenu"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Permiso>();
                }

            }
            return lista;
        }
    }
}
