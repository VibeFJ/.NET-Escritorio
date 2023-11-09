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
    public class ctrTCCategoria
    {
        private string administradorBD = ConfigurationManager.ConnectionStrings["NombreConexionBD"].ConnectionString;
        public List<TCCategoria> Obtener()
        {
            var respuesta = new List<TCCategoria>();
            try
            {
                using (var conexion = new SqlConnection(administradorBD))
                {
                    conexion.Open();

                    using (var comando = new SqlCommand("PRRTCCategoria", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        using (var atributo = comando.ExecuteReader())
                        {
                            while (atributo.Read())
                            {
                                var categoria = new TCCategoria()
                                {
                                    CategoriaId = Convert.ToInt32(atributo["CategoriaId"]),
                                    Descripcion = atributo["Descripcion"].ToString(),
                                };

                                respuesta.Add(categoria);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Add(new TCCategoria { Descripcion = "Error: " + ex.ToString() });
            }
            return respuesta;
        }
    }
}
