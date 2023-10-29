using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Deployment.Internal;

namespace Presentacion
{
    public partial class Main : Form
    {
        //Creamos una lista para cargarla en pantalla
        private List<Articulo> listaarticulos;
        private bool modi;
        //Creamos instancia de config q usaremos para cambiar seteos de la app
        Configuracion configuracion = new Configuracion();

        //Inicializamos los componentes
        public Main()
        {
            InitializeComponent();
            //Llamamos al metodo cargar para inicializar la lista
            cargar();

            //Cargamos los combo box de la busqueda avanzada
            cboCriterio.Items.Add("Codigo");
            cboCriterio.Text = "Codigo";
            cboCriterio.Items.Add("Nombre");
            cboCriterio.Items.Add("Descripcion");
            cboCriterio.Items.Add("Marca");
            cboCriterio.Items.Add("Categoria");
            cboCondicion.Items.Add("Comienza con");
            cboCondicion.Text = "Comienza con";
            cboCondicion.Items.Add("Termina con");
            cboCondicion.Items.Add("Contiene");
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        //Metodo para cargar la lista en la data grid view
        public void cargar()
        {
            //Creamos la variable negocio 
            ArticuloNegocio negocio = new ArticuloNegocio();
            //Creamos una variable de tipo bool y la ponemos en true para mandarsela al metodo listar
            bool tipo = true;

            try
            {
                //llamamos al metodo que devuelve la lista de articulos de la base de datos y su resultado lo cargamos en la data grid view (tambien ocultamos columnas y cargamos la imagen)
                listaarticulos = negocio.listar(tipo);
                dgvArticulos.DataSource = listaarticulos;
                ocultarColumnas();
            }
            catch (Exception)
            {
                //En caso de error en  la conexion mostramos el error de conexion y mostramos el formulario de confiugracion
                DialogResult dialogResult = MessageBox.Show("Error en la conexion a la Base de datos. Especifique una direccion correcta", "Conexion a Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);    
                    if (dialogResult == DialogResult.OK)
                    {
                    //le indicamos que vamos a la ventana de configuracion desde un error, esto nos permite poder salir del programa desde la misma en caso que no podamos arreglarlo
                    configuracion.desdeerror();
                    configuracion.ShowDialog();
                    cargar();
                    }
            }
        }

        //Metodo que oculta columnas de la data grid view
        private void ocultarColumnas()
        {
            dgvArticulos.Columns["artimagen"].Visible = false;
            dgvArticulos.Columns["artid"].Visible = false;
        }

        //Metodo para cargar una imagen, recibe un string que intenta cargarla, si no puede devuelve una imagen generica que dice no disponible
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                //Si la carga da error llamamos al metodo para configurar la ruta de las imagenes de error
                cargarImagendesdecatch();
            }
        }

        //Este metodo intenta cargar la imagen "base" de los productos que no tienen
        //En caso que no funcione muestra un error y nos manda a la configuracion para que lo solucionemos
        private void cargarImagendesdecatch()
        {
            //Agregamos un try para que intente cargar la imagen de producto "no disponible" elegida
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                pbxArticulo.Load(config.AppSettings.Settings["image-error"].Value);
            }
            catch (Exception)
            {
                DialogResult dialogResultimg = MessageBox.Show("Error en la carga de la imagen por defecto de productos, Especifique otra imagen", "Error de configuracion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dialogResultimg == DialogResult.OK)
                {
                    configuracion.desdeerror();
                    configuracion.ShowDialog();
                    cargar();
                }
            }
        }

        //Boton agregar / Metodo que llama al dialog para agregar articulos
        private void BotonAgregar_Click(object sender, EventArgs e)
        {
            AgregarArticulo agregar = new AgregarArticulo();

            //GuardarAnchoColumnas();


                agregar.ShowDialog();

                if (!agregar.cancelar)
                {
                cargar();

                //Esta vez el seleccionado lo vamos a agregar en este momento, luego de cargar un articulo
                //Si lo pondiramos antes del if, en el hipotetico caso q no tengamos ningun articulo intentaria seleccionar una referencia nula
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                // Este ciclo es para encontrar el ultimo producto cargado y luego seleccionarlo en la lista
                //usamos un contador que arranca en -1 para encontrar el indice ya que el id solo nos sirve para identificarlo    
                int contador = -1;
                    foreach (Articulo a in listaarticulos)
                    {
                        contador++;
                        if (a.artid == agregar.artagreg)
                        {
                            a.artid = agregar.artagreg;
                            dgvArticulos.CurrentCell = dgvArticulos.Rows[contador].Cells[1];
                            seleccionado = a;
                            contador = -1;
                            break;
                        }
                    }
                    cargarImagen(seleccionado.artimagen);
                }
                else
                {
                    //Si se apreto el boton cancelar la lista queda seleccionada como estaba antes, no llamamos a cargar denuevo, tampoco volvemos a cargar la imagen
                }
        }

        //Al cambiar de articulo se intenta cargar la imagen del mismo a traves de este metodo
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.artimagen);
            }
            else
            {
            }
    }

        //Boton filtrar busqueda, envia los 3 criterios seleccionados, crea una instancia articulonegocio y ejecuta el metodo filtrar
        private void BotonFiltrar(object sender, EventArgs e)
        {
            ArticuloNegocio artnegocio = new ArticuloNegocio();
            try
            {
                //Toma las condiciones de los combobox
                string criterio = cboCriterio.SelectedItem.ToString();
                string condicion = cboCondicion.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                //Manda las condiciones al metodo filtrar
                listaarticulos = artnegocio.filtrar(criterio, condicion, filtro);
                dgvArticulos.DataSource = listaarticulos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Boton modificar , primero revisa que este seleccionado un articulo, y ejecuta el formularion enviandole el seleccionado
        private void Modificar_Click(object sender, EventArgs e)
        {
            if(dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado;
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                int indexseleccionado = dgvArticulos.CurrentRow.Index;
                //Llamamos al constructor y le dceimos con la bool modi, que es una modificacion de articulo
                AgregarArticulo modificar = new AgregarArticulo(seleccionado, modi = true);
                modificar.ShowDialog();
                if (!modificar.cancelar)
                {
                    cargar();
                    //Al salir volvemos a seleccionar la fila del articulo que modificamos
                    dgvArticulos.CurrentCell = dgvArticulos.Rows[indexseleccionado].Cells[1];
                    cargarImagen(seleccionado.artimagen);
                }
                else
                {
                    //Si se apreto cancelar no hacemos nada
                }
            }
        }

        //Boton que llama a la ventana de agregar pero con una bandera para que se desactive la edicion de los campos
        private void btnVerart_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado;
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                int indexseleccionado = dgvArticulos.CurrentRow.Index;
                //Llamamos al constructor y le dceimos con la bool modi, que es una modificacion de articulo
                AgregarArticulo verart = new AgregarArticulo(seleccionado, modi = false);
                verart.ShowDialog();
            }
        }

        //Filtro superior, cuando el texto cambia se va actualizando la lista (siempre q sean 3 o mas letra)
        private void txtFiltro_TextChanged_1(object sender, EventArgs e)
        {

            //Guardo todos los anchos de la columna
            int w0 = dgvArticulos.Columns[0].Width;
            int w1 = dgvArticulos.Columns[1].Width;
            int w2 = dgvArticulos.Columns[2].Width;
            int w3 = dgvArticulos.Columns[3].Width;
            int w4 = dgvArticulos.Columns[4].Width;
            int w5 = dgvArticulos.Columns[5].Width;
            int w6 = dgvArticulos.Columns[6].Width;

            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text;
            if (filtro.Length >= 3)
            {
                listaFiltrada = listaarticulos.FindAll(x => x.artnombre.ToUpper().Contains(filtro.ToUpper()) || 
                x.artdescripcion.ToUpper().Contains(filtro.ToUpper()) || x.artcodigo.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaarticulos;
            }
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();

            //Vuelvo a cargar los anchos de las columnas
            dgvArticulos.Columns[0].Width = w0;
            dgvArticulos.Columns[1].Width = w1;
            dgvArticulos.Columns[2].Width = w2;
            dgvArticulos.Columns[3].Width = w3;
            dgvArticulos.Columns[4].Width = w4;
            dgvArticulos.Columns[5].Width = w5;
            dgvArticulos.Columns[6].Width = w6;
        }

        //Menu Superior
        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Borrados configuracion = new Borrados();
            configuracion.ShowDialog();
            cargar();
        }

        //Boton eliminar fisico
        private void btneliminarfisico_Click(object sender, EventArgs e)
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

        //Boton eliminar Logico que llama al metodo eliminar logico dentro de articulo negocio
        private void btneliminarlogico_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar el articulo " + dgvArticulos.CurrentRow.Cells[1].Value.ToString() + " - " + dgvArticulos.CurrentRow.Cells[2].Value.ToString(), "Eliminar Logico", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    ArticuloNegocio artnegocio = new ArticuloNegocio();
                    artnegocio.eliminarLogico(seleccionado.artid);
                    MessageBox.Show("Eliminado Exitosamente");
                    cargar();
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
        }

        //Boton salir
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Boton Acerca de
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acerca acercade = new Acerca();
            acercade.ShowDialog();
        }

        //Boton Configuracion
        private void configuracionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Le indicamos a la ventana que estamos ingresando desde el programa a la configuracion
            configuracion.desdeconfiguracion();
            configuracion.ShowDialog();
            cargar();
        }

        //Esto es para q al apretar enter en la caja de texto del filtro busque
        private void txtFiltroAvanzado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true; //Esta linea elimina el sonido que hace al apretarse el enter
                BotonFiltrar(null, null);
            }
        }


    //Metodo que oculta columnas de la data grid view
    private void GuardarAnchoColumnas()
    {
            for (int i = 0; i <= dgvArticulos.Columns.Count - 1; i++)
            {
            // almacenar anchos auto-dimensionados
            int colunmw = dgvArticulos.Columns[i].Width;
                //eliminar autosizing
                dgvArticulos.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //establecer el ancho calculado por autosize
                dgvArticulos.Columns[i].Width = colunmw;
       }



        }


}
}
