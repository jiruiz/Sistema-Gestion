using Datos;
using Negocio;
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
    public partial class frmAgencia : Form
    {
        private CNAgencia agenciaNegocio;
        public frmAgencia()
        {
            InitializeComponent();
            agenciaNegocio = new CNAgencia();
            gridAgencia.CellEndEdit += gridAgencia_CellEndEdit;

        }

        private void frmAgencia_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'administracionDataSet.Agencia'
            this.agenciaTableAdapter.Fill(this.administracionDataSet.Agencia);
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                List<Agencia> listaAgencias = agenciaNegocio.ObtenerAgencias();

                if (listaAgencias == null || listaAgencias.Count == 0)
                {
                    MessageBox.Show("No se encontraron agencias.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Configurar DataSource del DataGridView
                    gridAgencia.DataSource = listaAgencias;

                    // Configurar headers de columnas
                    gridAgencia.Columns["Id"].HeaderText = "Id";
                    gridAgencia.Columns["Nombre"].HeaderText = "Nombre";

                   
                    gridAgencia.Columns["Deposito"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las agencias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Agencia nuevaAgencia = new Agencia()
                {
                    Nombre = txtNombre.Text,
                };
                
                agenciaNegocio.AgregarAgencia(nuevaAgencia);

                MessageBox.Show("Cliente Argegado Exitosamente (CAPA PRESENTACION)");

                txtNombre.Clear();
                CargarDatos();


            }
            catch (Exception ex)
            {

                MessageBox.Show(" (CAPA PRESENTACION)  Error al agregar el Agencia: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (gridAgencia.SelectedRows.Count > 0)
            {
                int agenciaId = Convert.ToInt32(gridAgencia.SelectedRows[0].Cells["Id"].Value);

                try
                {
                    agenciaNegocio.EliminarAgencia(agenciaId);
                    MessageBox.Show("Agencia eliminada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos(); // Actualiza el DataGridView después de la eliminación
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la agencia: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione la fila para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridAgencia_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Obtener el ID de la agencia modificada
                int agenciaId = Convert.ToInt32(gridAgencia.Rows[e.RowIndex].Cells["Id"].Value);

                // Obtener el nuevo nombre de la agencia desde la celda editada
                string nuevoNombre = gridAgencia.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

                // Crear objeto Agencia con los cambios
                Agencia agenciaModificada = new Agencia
                {
                    Id = agenciaId,
                    Nombre = nuevoNombre
                };

                // Llamar al método de negocio para modificar la agencia
                agenciaNegocio.ModificarAgencia(agenciaModificada);

                MessageBox.Show("Agencia modificada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar los datos en el DataGridView después de la modificación
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la agencia: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
