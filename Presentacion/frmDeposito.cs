using Datos;
using DocumentFormat.OpenXml.Office2010.Excel;
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
    public partial class frmDeposito : Form
    {
        private CNDeposito depositoNegocio;
        public frmDeposito()
        {
            InitializeComponent();
            CargarAgencias();
            depositoNegocio = new CNDeposito();
            gridDeposito.CellEndEdit += gridDeposito_CellEndEdit;
        }

        private void frmDeposito_Load(object sender, EventArgs e)
        {
            CargarDatos();
            txtID.Text = depositoNegocio.ObtenerNuevoCodigoID().ToString();
        }



        private void CargarAgencias()
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var agencias = context.Agencia.ToList();

                    // Asignar la lista de agencias al ComboBox
                    comboBoxAgencias.DataSource = agencias;
                    comboBoxAgencias.DisplayMember = "Nombre"; // Nombre de la propiedad a mostrar en el ComboBox
                    comboBoxAgencias.ValueMember = "Id"; // Nombre de la propiedad que representa el valor del item seleccionado
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las agencias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (comboBoxAgencias.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una agencia válida.");
                return;
            }

            if (!int.TryParse(comboBoxAgencias.SelectedValue.ToString(), out int idAgencia))
            {
                MessageBox.Show("Ingrese un valor válido para Id de Agencia.");
                return;
            }

            if (!int.TryParse(txtID.Text, out int idDeposito))
            {
                MessageBox.Show("Error al obtener el Id del depósito.");
                return;
            }

            Deposito nuevoDeposito = new Deposito()
            {
                Id = idDeposito,
                FechaDeposito = Convert.ToDateTime(dataDeposito.Text),
                ImporteDeposito = Convert.ToDouble(txtImporteDeposito.Text),
                BancoDeposito = txtBanco.Text,
                ObservacionesDepositos = txtObs.Text,
                idAgencia = idAgencia,
            };

            depositoNegocio.AgregarDeposito(nuevoDeposito);

            MessageBox.Show("Depósito Agregado Exitosamente (CAPA PRESENTACION)");

            txtImporteDeposito.Clear();
            txtBanco.Clear();
            txtObs.Clear();

            CargarDatos();
            txtID.Text = depositoNegocio.ObtenerNuevoCodigoID().ToString();
        }









        private void CargarDatos()
        {
            try
            {
                List<Deposito> listaDeposito = depositoNegocio.ObtenerDepositos();

                if (listaDeposito == null || listaDeposito.Count == 0)
                {
                    MessageBox.Show("No se encontraron depósitos en la lista.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Configurar DataSource del DataGridView
                    gridDeposito.DataSource = listaDeposito;

                    // Configurar headers de columnas
                    gridDeposito.Columns["FechaDeposito"].HeaderText = "Fecha de Depósito";
                    gridDeposito.Columns["ImporteDeposito"].HeaderText = "Importe de Depósito";
                    gridDeposito.Columns["BancoDeposito"].HeaderText = "Banco de Depósito";
                    gridDeposito.Columns["ObservacionesDepositos"].HeaderText = "Observaciones";
                    gridDeposito.Columns["IdAgencia"].HeaderText = "ID Agencia"; // Asegúrate de que la propiedad sea IdAgencia

                    // Ocultar otras columnas si es necesario
                    gridDeposito.Columns["Agencia"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los depósitos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void gridDeposito_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int depositoId = Convert.ToInt32(gridDeposito.Rows[e.RowIndex].Cells["Id"].Value);
                DateTime fechaDeposito = Convert.ToDateTime(gridDeposito.Rows[e.RowIndex].Cells["FechaDeposito"].Value);
                double importeDeposito = Convert.ToDouble(gridDeposito.Rows[e.RowIndex].Cells["ImporteDeposito"].Value);
                string bancoDeposito = gridDeposito.Rows[e.RowIndex].Cells["BancoDeposito"].Value.ToString();
                string observaciones = gridDeposito.Rows[e.RowIndex].Cells["ObservacionesDepositos"].Value.ToString();

                // Obtener la agencia seleccionada desde el ComboBox
                int idAgencia = Convert.ToInt32(comboBoxAgencias.SelectedValue);

                // Crear objeto Deposito con los cambios
                Deposito depositoModificado = new Deposito
                {
                    Id = depositoId,
                    FechaDeposito = fechaDeposito,
                    ImporteDeposito = importeDeposito,
                    BancoDeposito = bancoDeposito,
                    ObservacionesDepositos = observaciones,
                    idAgencia = idAgencia // Asignar el valor de idAgencia desde el ComboBox
                };

                // Llamar al método de negocio para modificar el depósito
                depositoNegocio.ModificarDeposito(depositoModificado);

                MessageBox.Show("Depósito modificado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar los datos en el DataGridView después de la modificación
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar depósito: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btnEliminarDepositos_Click(object sender, EventArgs e)
        {
            try
            {
                CNDeposito negocioDeposito = new CNDeposito();
                negocioDeposito.EliminarTodosLosDepositos();

                MessageBox.Show("Se eliminaron todos los depósitos correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Luego puedes recargar los datos o realizar alguna otra acción necesaria
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar los depósitos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (gridDeposito.SelectedRows.Count > 0)
            {
                int depositoId = Convert.ToInt32(gridDeposito.SelectedRows[0].Cells["Id"].Value);

                try
                {
                    CNDeposito negocioDeposito = new CNDeposito();
                    negocioDeposito.EliminarDeposito(depositoId);
                    MessageBox.Show("Deposito eliminada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos(); // Actualiza el DataGridView después de la eliminación
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar El deposito: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione la fila para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
