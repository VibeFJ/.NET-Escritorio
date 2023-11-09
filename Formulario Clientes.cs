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
    public partial class Formulario_Clientes : Form
    {
        private List<TCProducto> productos = new List<TCProducto>();

        public Formulario_Clientes(List<TCProducto> objeto)
        {
            InitializeComponent();
            productos = objeto;
        }

        private void btnContinuarPedido_Click(object sender, EventArgs e)
        {
            var cliente = new TACliente()
            {
                Nombre = txtNombre.Text,
                ApellidoPaterno = txtApellidoPaterno.Text,
                ApellidoMaterno = txtApellidoMaterno.Text,
            };

            var ctrCliente = new ctrTACliente();
            var idCliente = ctrCliente.Insertar(cliente);

            if (idCliente > 0)
            {
                var pedido = new TAPedido()
                {
                    ClienteId = idCliente,
                    FechaPedido = DateTime.Now
                };

                var ctrPedido = new ctrTAPedido();
                var idPedido = ctrPedido.Insertar(pedido);

                if (idPedido > 0)
                {
                    var idProducto = 0;
                    var contador = 0;
                    var totalProductos = productos.Count;
                    var total = 0;
                    foreach (var producto in productos)
                    {
                        total++;
                        if (producto.ProductoId == idProducto)
                        {
                            contador++;
                            idProducto = producto.ProductoId;
                        }
                        if (idProducto != 0 && producto.ProductoId != idProducto)
                        {
                            var pedidoDetalle = new TAPedidoDetalle()
                            {
                                PedidoId = idPedido,
                                ProductoId = idProducto,
                                Cantidad = contador
                            };
                            var ctrPedidoDetalle = new ctrTAPedidoDetalle();
                            var respuesta = ctrPedidoDetalle.Insertar(pedidoDetalle);

                            if(respuesta > 0)
                            {
                                pedidoDetalle = new TAPedidoDetalle()
                                {
                                    DetalleId = respuesta,
                                    PedidoId = idPedido,
                                    ProductoId = idProducto,
                                    Cantidad = contador
                                };
                                ctrPedidoDetalle.Actualizar(pedidoDetalle);
                            }
                            idProducto = 0;
                        }
                        if (idProducto == 0)
                        {
                            contador = 1;
                            idProducto = producto.ProductoId;
                        }
                        if (total == totalProductos)
                        {
                            var pedidoDetalle = new TAPedidoDetalle()
                            {
                                PedidoId = idPedido,
                                ProductoId = idProducto,
                                Cantidad = contador
                            };
                            var ctrPedidoDetalle = new ctrTAPedidoDetalle();
                            var respuesta = ctrPedidoDetalle.Insertar(pedidoDetalle);
                            if(respuesta>0)
                            {
                                pedidoDetalle = new TAPedidoDetalle()
                                {
                                    DetalleId = respuesta,
                                    PedidoId = idPedido,
                                    ProductoId = idProducto,
                                    Cantidad = contador
                                };

                                var confirmar = ctrPedidoDetalle.Actualizar(pedidoDetalle);
                                if (confirmar)
                                {
                                    pedido = new TAPedido()
                                    {
                                        PedidoId = idPedido,
                                        ClienteId = idCliente,
                                        FechaPedido = DateTime.Now
                                    };

                                    ctrPedido = new ctrTAPedido();
                                    confirmar = ctrPedido.Actualizar(pedido);
                                    if(confirmar)
                                    {
                                        var compra = new Compra(productos, idCliente);
                                        compra.Show();
                                        Hide();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error", "No se registro el pedido");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Error", "No se registro el pedido");
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error", "No se registro el pedido");
                }
            }
            else
            {
                MessageBox.Show("Error", "No se registro el cliente");
            }
        }
    }
}
