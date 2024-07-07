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
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        private void btnIrAgencias_Click(object sender, EventArgs e)
        {

            
            frmAgencia ventanaAgencia = new frmAgencia();
            ventanaAgencia.ShowDialog(); 

            /*
            Program.frmAgenci.Hide();
            frmAgencia ventanaAgencias = new frmAgencia();
            ventanaAgencias.Show();
            */
        }

        private void btnIrDepositos_Click(object sender, EventArgs e)
        {
            frmDeposito ventanaDeposito = new frmDeposito();
            ventanaDeposito.ShowDialog();
        }
    }
}
