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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Escritorio
{
    public partial class Form1 : Form
    {
        private List<TCProducto> productos = new List<TCProducto>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var ctrCategoria = new ctrTCCategoria();
            var categorias = ctrCategoria.Obtener();

            cbxCategoria.Items.Clear();
            foreach (var categoria in categorias)
            {
                cbxCategoria.Items.Add(new TCCategoria { Descripcion = categoria.Descripcion, CategoriaId = categoria.CategoriaId });
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cbxProductos.SelectedItem is TCProducto productos)
            {
                for(int i = 0;i < int.Parse(cbxCantidad.Text); i++)
                {
                    lbxProductos.Items.Add(productos);
                }
            }
        }

        private void btnRealizarPedido_Click(object sender, EventArgs e)
        {
            foreach (TCProducto producto in lbxProductos.Items)
            {
                productos.Add(producto);
            }

            var formulario = new Formulario_Clientes(productos);
            formulario.Show();
            Hide();
        }

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCategoria.SelectedItem is TCCategoria categorias)
            {
                var ctrProductoCategoria = new ctrTRProductoCategoria();
                var productos = ctrProductoCategoria.Obtener(categorias.CategoriaId);

                cbxProductos.Items.Clear();
                foreach (var producto in productos)
                {
                    cbxProductos.Items.Add(new TCProducto { Nombre = producto.Nombre, ProductoId = producto.ProductoId });
                }
            }
        }
    }
}
