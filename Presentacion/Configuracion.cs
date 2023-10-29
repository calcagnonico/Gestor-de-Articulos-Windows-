using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Configuracion : Form
    {
        private OpenFileDialog archivo = null;
        bool botoncys = true;

        public Configuracion()
        {
            InitializeComponent();
            cargar();
        }

        //Creamos una variable tipo Configurator
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public void cargar()
        {
        //Cargamos la value de images-folder y server en las cajas de texto
        txtRutaImagenes.Text = config.AppSettings.Settings["images-folder"].Value;
        txtServer.Text = config.AppSettings.Settings["server"].Value;
        txtErrorimage.Text = config.AppSettings.Settings["image-error"].Value;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //cargamos en la config "images-folder" el texto que hay en la caja de texto
            config.AppSettings.Settings["images-folder"].Value = txtRutaImagenes.Text;
            config.AppSettings.Settings["server"].Value = txtServer.Text;
            config.AppSettings.Settings["image-error"].Value = txtErrorimage.Text;
            ConfigurationManager.RefreshSection("images-folder");
            ConfigurationManager.RefreshSection("server");
            ConfigurationManager.RefreshSection("image-error");
            //guardamos
            try
            {
                config.Save();
                Properties.Settings.Default.Reload();
                //Nulleamos la variable luego por si necesitamos recambiarla

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Close();
        }

        //Este metodo sirve para abrir el cuadro de dialogo que nos permite elegir la carpeta y la devuelve en la variable txt (le sumamos el signo \ necesario para que funcione el algoritmo
        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (var fd = new FolderBrowserDialog())
            {
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fd.SelectedPath))
                {
                    txtRutaImagenes.Text = fd.SelectedPath + "\\";
                }
            }
        }

        //Este metodo sirve para abrir el cuadro de dialogo que nos permite elegir un archivo que sera el que se muestre cuando un producto no tenga imagen cargada
        private void btnExaminarimg_Click(object sender, EventArgs e)
        {
                archivo = new OpenFileDialog();
                //Filtro de tipos de archivo:
                archivo.Filter = "Imagenes jpg |*.jpg|Imagenes png |*.png| Imagenes bmp |*.bmp|Imagenes gif |*.gif| Todos los Archivos |*jpg;*.png;*.bmp;*.gif";
                if (archivo.ShowDialog() == DialogResult.OK)
                {
                txtErrorimage.Text = archivo.FileName;
                }
                else
                {
                }
        }

        //Boton cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(botoncys)
            {

                //BOTON SALIR
                //enviroment cierra todos los forms abiertos pq este form al cerrarse continuaria la ejecucion desde donde vino (Main del metodo cargar)
                Environment.Exit(0);
                //cerramos la app
                Application.Exit();
            }
            else
            {
                //Boton CANCELAR
                Close();
            }
        }

        //Segun desde donde llamemos la ventana se configura de distintas maneras:
        public void desdeconfiguracion()
        {
            botoncys = false;
            btnCancelar.Text = "Cancelar";
        }

        public void desdeerror()
        {
            botoncys = true;
            btnCancelar.Text = "Salir";
        }



        //Este metodo controla cuando se apreta escape o enter
        //Para hacer esto primero el KeyPreview del formulario en True y en el evento KeyDown del Form 
        private void Configuracion_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Escape)
                {
                    e.Handled = true; //Esta linea elimina el sonido que hace al apretarse el enter
                    Close();
                }

                //TECLA ENTER
                if (e.KeyCode == Keys.Enter)
                {
                    btnGuardar_Click(null, null);
                }
            }
        }
    }
}