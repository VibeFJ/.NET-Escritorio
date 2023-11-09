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
    public class ctrTRProductoCategoria
    {
        private string administradorBD = ConfigurationManager.ConnectionStrings["NombreConexionBD"].ConnectionString;
        public List<TCProducto> Obtener(int Id)
        {
            var respuesta = new List<TCProducto>();
            try
            {
                using (var conexion = new SqlConnection(administradorBD))
                {
                    conexion.Open();

                    using (var comando = new SqlCommand("PRRTRProductoCategoria", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@CategoriaId", Id);

                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var producto = new TCProducto
                                {
                                    ProductoId = reader.GetInt32(reader.GetOrdinal("ProductoId")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("Precio"))
                                };
                                respuesta.Add(producto);
                            }
                        }
                    }
                }
            }
            finally
            {

            }
            return respuesta;
        }
    }
}
