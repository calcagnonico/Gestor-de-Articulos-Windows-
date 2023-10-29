using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Presentacion
{
    public partial class Borrados : Form
    {
        public Borrados()
        {
            InitializeComponent();
            //Al inicializar llamamos al metodo para cargar la lista
            cargar();
        }

        //Creamos la Lista de tipo Articulo
        private List<Articulo> listaarticulos = null;

        //Metodo para Cargar la lista
        private void cargar()
        {
            bool tipo = false;
            ArticuloNegocio negocio = new ArticuloNegocio();
                try
                {
                //Llamamos a listar pero le mandamos el bool "tipo" en false
                //El resultado del metodo lo cargamos en la variable listaarticulos
                //Tambien llamamos al metodo para ocultar columnas
                    listaarticulos = negocio.listar(tipo);
                    dgvArticulos.DataSource = listaarticulos;
                    ocultarColumnas();
                }
                catch (Exception ex)
                {
                MessageBox.Show(ex.ToString());
                }
        }

        //Metodo para Restablecer el Articulo
        private void btnrestablecer_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            //Comprobamos si la lista esta vacía
            if (listaarticulos.Count != 0)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                try
                    {
                    //Le mandamos el articulo al metodo restablecer dentro de tipo Articulo negocio
                       negocio.restablecer(seleccionado);
                       MessageBox.Show("Articulo Restablecido exitosamente");
                       cargar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                }
            }
            else
            {

            }
        }

        //Metodo para Borrar fisicamente el Articulo
        private void btnEliminarF_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar fisicamente el articulo " + dgvArticulos.CurrentRow.Cells[1].Value.ToString() + " - " + dgvArticulos.CurrentRow.Cells[2].Value.ToString(), "Eliminar Fisico", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    ArticuloNegocio artnegocio = new ArticuloNegocio();
                    //Llamamos al metodo eliminar fisico dentro del artnegocio de tipo ArticuloNegocio
                    artnegocio.eliminarFisico(seleccionado.artid);
                    MessageBox.Show("Eliminado Exitosamente");
                    cargar();
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
        }

        //Metodo para ocultar columnas de la tabla
        private void ocultarColumnas()
        {
            dgvArticulos.Columns["artimagen"].Visible = false;
            dgvArticulos.Columns["artid"].Visible = false;
        }

        private void btnEliminarF_Click_1(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar el articulo " + dgvArticulos.CurrentRow.Cells[1].Value.ToString() + " - " + dgvArticulos.CurrentRow.Cells[2].Value.ToString(), "Eliminar Fisico", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    ArticuloNegocio artnegocio = new ArticuloNegocio();
                    artnegocio.eliminarFisico(seleccionado.artid);
                    MessageBox.Show("Eliminado Exitosamente");
                    cargar();
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
        }


        //Al apretar escape salimos del form
        //Para hacer esto pusimos la propiedad KeyPreview del formulario en True y este es el evento KeyDown
        private void Borrados_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Escape)
                {
                    e.Handled = true; //Esta linea elimina el sonido que hace al apretarse el enter
                    Close();
                }
            }
        }
    }
}
