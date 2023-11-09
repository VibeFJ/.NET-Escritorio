using Escritorio.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.ControladoresNegocio
{
    public class ctrTCProducto
    {
        private string administradorBD = ConfigurationManager.ConnectionStrings["NombreConexionBD"].ConnectionString;
        public List<TCProducto> Obtener()
        {
            var respuesta = new List<TCProducto>();
            try
            {
                using (var conexion = new SqlConnection(administradorBD))
                {
                    conexion.Open();

                    using (var comando = new SqlCommand("PRRTCProducto", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (var atributo = comando.ExecuteReader())
                        {
                            while (atributo.Read())
                            {
                                var producto = new TCProducto()
                                {
                                    ProductoId = Convert.ToInt32(atributo["ProductoId"]),
                                    Nombre = atributo["Nombre"].ToString(),
                                };

                                respuesta.Add(producto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Add(new TCProducto { Nombre = "Error: " + ex.ToString() });
            }
            return respuesta;
        }
    }
}
