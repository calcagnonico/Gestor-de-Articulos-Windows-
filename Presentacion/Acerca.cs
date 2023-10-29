using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Acerca : Form
    {
        public Acerca()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://maxiprograma.com/");
        }

        //Al apretar escape salimos del form
        //Para hacer esto pusimos la propiedad KeyPreview del formulario en True y este es el evento KeyDown
        private void Acerca_KeyDown(object sender, KeyEventArgs e)
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
