using Escritorio.ControladoresNegocio;
using Escritorio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Escritorio
{
    public partial class Compra : Form
    {
        private List<TCProducto> productos = new List<TCProducto>();

        private List<TAPedido> pedidos = new List<TAPedido>();
        private int idCliente = new int();
        private bool confirmmacion = new bool();
        public Compra(List<TCProducto> objeto, int datos)
        {
            InitializeComponent();
            productos = objeto;
            idCliente = datos;
        }

        private void Compra_Load(object sender, EventArgs e)
        {
            var ctrPedidoCliente = new ctrPedidoCliente();
            pedidos = ctrPedidoCliente.Obtener(idCliente);
            foreach(var pedido in pedidos)
            {
                lblFecha.Text = "Fecha: " + pedido.FechaPedido.ToString();
                lblTotal.Text = "Total: " + pedido.Total.ToString();
            }

            lbxProductos.Items.Clear();
            foreach(var producto in productos)
            {
                lbxProductos.Items.Add(producto.Nombre);
                confirmmacion = true;
            }
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            if(confirmmacion)
            {
                MessageBox.Show("Exito", "Gracias por su compra");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var limite = pedidos.Count();
            var contador = 0;
            foreach(var pedido in pedidos)
            {
                contador++;
                var ctrPedidoDetalle = new ctrTAPedidoDetalle();
                var respuesta = ctrPedidoDetalle.Eliminar(pedido);

                if(respuesta)
                {
                    var ctrPedido = new ctrTAPedido();
                    respuesta = ctrPedido.Eliminar(pedido);

                    if(respuesta)
                    {
                        if (contador == limite)
                        {
                            var ctrCliente = new ctrTACliente();
                            respuesta = ctrCliente.Eliminar(idCliente);
                            if(respuesta)
                            {
                                MessageBox.Show("Eliminado correctamente", "Exito");
                            }
                            else
                            {
                                MessageBox.Show("Cliente no fue eliminado", "Error");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Pedido no fue eliminado", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Pedido no fue eliminado", "Error");
                }
            }
        }
    }
}
