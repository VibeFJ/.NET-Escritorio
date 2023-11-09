using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.Entidades
{
    public class TCProducto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set;}
        public decimal Precio { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
