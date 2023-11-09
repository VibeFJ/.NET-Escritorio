using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.Entidades
{
    public class TAPedido : TAPedidoDetalle
    {
        public int ClienteId { get; set; }
        public DateTime FechaPedido { get; set; }
    }
}
