using dominio;
using negocio;
using System;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class AgregarArticulo : Form
    {
        //Creamos un articulo que sera el que estaba seleccionado cuando apretamos modificar
        //Como vamos a usar el mismo formulario para crear un nuevo pokemon esta variable la pondremos en NULL
        //En el caso que el usuario presione "Modificar" en el main cargaremos el seleccionado en esta variable a traves del constructor(*)
        private Articulo articulo = null;
        private Articulo articuloaux = null;
        public int artagreg = 0;

        //esta banderita es para ver si se apreto cancelar
        public bool cancelar = false;


        //Creamos una bandera que vamos a utilizar para identificar si se esta agregando un nuevo articulo o si se esta modificando uno existente
        private bool banderamodificar = false;
        //Creamos una variable de OpenfileDialog en null para el boton examinar
        private OpenFileDialog archivo = null;

        //Este es el constructor principal (no recibe articulo ya que es el form para agregar nuevos articulos)
        public AgregarArticulo()
        {
            InitializeComponent();
            Text = "Agregar Articulo";
            txtTitulo.Text = "Agregar Articulo";
        }

        //(*)Este otro constructor recibe un articulo y un booleano (Lo usaremos cuando se haga click en Modificar o en ver articulo)
        public AgregarArticulo(Articulo articulo, bool modi)
        {
            if(modi)
            { 
            InitializeComponent();
            banderamodificar = true;
            this.articulo = articulo;
            Text = "Modificar Articulo";
            txtTitulo.Text = "Modificar Articulo";
            }
            else
            {
                InitializeComponent();
                this.articulo = articulo;
                Text = "Ver Articulo";
                txtTitulo.Text = "Ver articulo";
                txtCodigo.ReadOnly = true;
                txtNombre.ReadOnly = true;
                txtDescripcion.ReadOnly = true; 
                txtImagen.ReadOnly = true;
                cboMarca.Enabled = false;
                cboCategoria.Enabled = false;
                txtPrecio.ReadOnly = true;
                BotonExaminar.Visible = false;
                btnCancelar.Visible = false;
                btnAceptar.Visible = false;
                btnCerrar.Visible = true;
            }
        }

        private void AgregarArticulo_Load(object sender, EventArgs e)
        {
            //(1) Creamos las variables para cargar las listas desplegables 
            MarcaNegocio Mnegocio = new MarcaNegocio();
            CategoriaNegocio Cnegocio = new CategoriaNegocio();
            cargarImagen(txtImagen.Text);
            try
            {
                //(2) Cargamos los datos en las listas desplegables (llamamos al metodo listar que nos devuelve la info)
                // Con Value member le decimos el valor q va a manejar , en este caso en numeros (id)
                // Con display memeber le vamos a decir el texto que queremos que muestre el combobox
                cboMarca.DataSource = Mnegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                // Hacemos lo mismo con el combobox de la Categoria
                cboCategoria.DataSource = Cnegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                // Cargamos los datos en los textos en el caso que estemos modificando un articulo, identificamos a traves del articulo != a null
               if (articulo != null)
               {
                    txtCodigo.Text = articulo.artcodigo.ToString();
                    txtNombre.Text = articulo.artnombre.ToString();
                    txtDescripcion.Text = articulo.artdescripcion.ToString();
                    txtImagen.Text = articulo.artimagen.ToString();
                    cargarImagen(articulo.artimagen);
                    cboCategoria.SelectedValue = articulo.artcategoria.Id;
                    cboMarca.SelectedValue = articulo.artmarca.Id;
                    txtPrecio.Text = articulo.artprecio.ToString();

                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        //Este metodo lo usamos para la carga de la imagen de los articulos
        private void cargarImagen(string imagen)
        {
            //Creamos una variable de config
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                pbxArticulo.Load(config.AppSettings.Settings["image-error"].Value);

            }
        }

        //Boton examinar para cargar la imagen
        private void BotonExaminar_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            //Filtro de tipos de archivo:
            archivo.Filter = "Imagenes jpg |*.jpg|Imagenes png |*.png| Imagenes bmp |*.bmp|Imagenes gif |*.gif| Todos los Archivos |*jpg;*.png;*.bmp;*.gif";

            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
            }
            else
            {
                cargarImagen(archivo.FileName);
            }
        }

        //El siguiente metodo se ejecuta cuando salimos del cuadro de texto imagen. Al seleccionar en otro lado intentara cargar la imagen
        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }

        //Boton Aceptar
        private void btnAceptar_Click(object sender, EventArgs e)
        {   
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (articulo == null)
                //Creamos la instancia si no existe
                {
                    articulo = new Articulo();
                }
                else
                {
                    //Creamos un auxiliar que nos va a servir para ver si se edito o no se edito nungun campo
                    articuloaux = (Articulo)articulo.Clone();
                }
                
                //Cargamos los datos escritos en el form dentro del articulo
                articulo.artcodigo = txtCodigo.Text;
                articulo.artnombre = txtNombre.Text;
                articulo.artdescripcion = txtDescripcion.Text;
                articulo.artimagen = txtImagen.Text;
                articulo.artcategoria = (Categoria)cboCategoria.SelectedItem;
                articulo.artmarca = (Marca)cboMarca.SelectedItem;


                if (!validarcaracteres(txtCodigo.Text))
                {
                    //Antes de ejecutar, validamos el campo codigo llamando al metodo validarcaracteres
                    if (!validarcaracteres(txtNombre.Text))
                    {
                    //Antes de ejecutar, validamos el campo del precio llamando al metodo Validarplata
                        if (Validarplata(txtPrecio.Text))
                        {
                        //Convertimos a decimal
                        articulo.artprecio = (Convert.ToDecimal(txtPrecio.Text));
    
                            if (banderamodificar)
                            {
                            //LLamamos en un condicional, al metodo que compara articulos y devuelve un bool 
                            //Este metodo recibe el auxiliar que fue clonado del articulo, y el articulo que se intenta guardar
                            //Esto es para ver si se modifico o no alguna celda
                            //En caso que no se haya modificado ningun campo no haremos nada
                                if (CompararArticulos(articulo, articuloaux))
                                {
                               negocio.modificar(articulo);
                               //llamamos al metodo que creamos para verificar si la imagen con el mismo nombre existe
                               Copiarimagen(articulo.artcodigo);
                               MessageBox.Show("Articulo Modificado exitosamente");
                               Close();
                                }
                                else
                                {
                                Close();
                                }
                            }
                            else
                            {
                            //ejecutamos agregar y mandamos a
                            artagreg = negocio.agregar(articulo);
                            MessageBox.Show("Articulo Agregado exitosamente");
                            //llamamos al metodo que creamos para copiar la imagen
                            Copiarimagen(articulo.artcodigo);
                            Close();
                            }
                        }
                        else
                        {
                        //En caso que el campo precio no este expresado como money nos dara el siguiente error
                        MessageBox.Show("Error en el campo obligatorio precio");
                        }
                    }
                    else
                    {
                    //En caso que el nombre este mal nos da este mensaje
                    MessageBox.Show("Error en el campo obligatorio Nombre. Campo vacio o caracteres inválidos");
                    }
                }
                else
                {
                //En caso que el codigo este mal nos da este mensaje
                MessageBox.Show("Error en el campo obligatorio Código. Campo vacio o caracteres inválidos");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Boton cancelar que cierra la ventana principal
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //cerramos pero ponemos la bandera en true
            cancelar = true;
            Close();
        }

        //Metodo para validar los datos ingresados en el precio
        private bool Validarplata(string numero)
        {
            if (decimal.TryParse(numero, out decimal num))
                return true;
            else
                return false;
        }

        //Metodo para copiar la imagen en directorio local creado
        private void Copiarimagen(string idart)
        {
            //Creamos una variable de config
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            
            if (Directory.Exists(config.AppSettings.Settings["images-folder"].Value))
            {
                //verificamos primero que la imagen no sea de la web
                if (archivo != null && !(txtImagen.Text.ToUpper().Contains("HTTP")))
                {
                    //Para copiar el archivo de imagen a la carpeta primero revisamos que el archivo exista
                    if (File.Exists(config.AppSettings.Settings["images-folder"].Value + idart + "-" + archivo.SafeFileName))
                    {
                    //En caso que exista un archivo con el mismo damos la opcion de elegir si reemplazarla o no
                    DialogResult dialogResult = MessageBox.Show("Ya existe una imagen con el mismo nombre, desea reemplazarla?", "Error al copiar imagen", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                        File.Delete(config.AppSettings.Settings["images-folder"].Value + idart + "-" + archivo.SafeFileName);
                        File.Copy(archivo.FileName, config.AppSettings.Settings["images-folder"].Value + idart + "-" + archivo.SafeFileName);
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                        }
                    }
                    else
                    {
                    //Verificamos que lo que este escrito en el box de imagen sea un archivo que exista
                    //Esto lo hacemos por si el usuario eligio un archivo de imagen desde el boton + pero despues cambio la ruta
                        if (File.Exists(articulo.artimagen))
                        {
                        //Si no hay un archivo con el mismo nombre lo copiamos
                        File.Copy(archivo.FileName, config.AppSettings.Settings["images-folder"].Value + idart + "-" + archivo.SafeFileName);
                        }
                        else
                        {
                        }
                    }
                }
                else
                {
                //En el caso que sea una imagen web llamamos a un metodo que va a intentar descargarla y guardarla en la carpeta backup
                ImagenWEB(txtImagen.Text, idart);
                }
            }
            else
            //En caso que el directorio no exista lo creamos
            {
                Directory.CreateDirectory(config.AppSettings.Settings["images-folder"].Value);
                ConfigurationManager.RefreshSection("images-folder");
                //volvemos a llamar al metodo
                Copiarimagen(idart);
            }
        }

        //Este metodo recibe la direccion web de la pagina, descarga la imagen y la copia a la carpeta backuo de imagenes
        static void ImagenWEB(string imagen, string idart)
        {
                string url = imagen;
                WebClient client = new WebClient();
            try
            {
                Stream stream = client.OpenRead(url);
                Bitmap bitmap = new Bitmap(stream);
  
                Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);

                if (bitmap != null)
                {
                    int x = 1;
                
                    //Este ciclo es para agregarle un numero al final del nombre de la imagen en caso que el archivo ya exista.
                    while (File.Exists(config.AppSettings.Settings["images-folder"].Value + idart + "-" + "Web - " + x + ".png"))
                    {
                        x = x + 1;
                    }
                    bitmap.Save(config.AppSettings.Settings["images-folder"].Value + idart + "-" + "Web - " + x + ".png", ImageFormat.Png);
                }
                stream.Flush();
                stream.Close();
                client.Dispose();
            }
            catch (Exception ex)
            {
                //En caso q la web no exista le damos return para que siga funcionando el programa
                return;
            }
        }

        //Comparador que usamos para ver si se edito o no un campo en un articulo
        private bool CompararArticulos(Articulo art1, Articulo art2)
        {
        PropertyInfo[] lst1 = typeof(Articulo).GetProperties();
        bool banderacomparar = false;

                foreach (PropertyInfo oProperty in lst1)
                {
                    string NombreAtributo = oProperty.Name;
                    string Valor = oProperty.GetValue(art1).ToString();
                    string Valor2 = oProperty.GetValue(art2).ToString();

                    if (Valor != Valor2)
                    {
                    banderacomparar = true;
                    }
                    else
                    {
                    }
            }
            return banderacomparar;
        }

        //Boton cerrar
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            //cerramos pero ponemos la bandera en true
            cancelar = true;
            Close();
        }

        //El siguiente metodo es para verificar los datos ingresados en las cajas de texto
        private bool validarcaracteres (string texto)
        {
            bool error = false;

            //PRUEBA 1 - buscamos si el campo esta vacio
            if (texto == "")
            {
                error = true;
            }
            else
            {
            }

            //PRUEBA 2 - Buscamos caracteres raros
            foreach (char c in texto)
                {
                    //Estamos mandando error cuando se usen caracteres que nos den error al crear el archivo de backup
                    if (c == '/' | c == '\\' | c == ':' | c == '*' | c == '?' | c == '"' | c == '|' )
                    {
                    error = true;
                    }
                    else
                    {
                    }
                }

            //PRUEBA 3
            //Esto es por si algun tester quiere poner en el campo varios espacios en blanco
            int cantcar = 0;
            int cantchar = 0;
            foreach (char c in texto)
            {
                cantchar++;
                if (c == ' ')
                {
                   cantcar++;
                }
                else
                {
                }
            }
            if (cantcar == cantchar)
            {
                error = true;
            }
            else
            {
            }
            //////////////////////////////

        return error;
        }

        private void AgregarArticulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true; //Esta linea elimina el sonido que hace al apretarse el enter
                cancelar = true;
                Close();
            }

            //TECLA ENTER
            if (e.KeyCode == Keys.Enter)

                //Con la caja de texto en readonly sabemos que estamos en la opcion de "ver" un articulo, por lo tanto
                //no tiene que poder modificarlo 
                if (txtNombre.ReadOnly)
                    {
                    cancelar = true;
                    Close();
                }
                else
                {
                    btnAceptar_Click(null, null);
                }
                }
        }

    
}


