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
using ClosedXML.Excel;
using System.IO;



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

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (gridDeposito.DataSource is List<Deposito> listaDeposito && listaDeposito.Count > 0)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Depositos");

                        // Agregar encabezados
                        worksheet.Cell(1, 1).Value = "ID Deposito";
                        worksheet.Cell(1, 2).Value = "Fecha";
                        worksheet.Cell(1, 3).Value = "Importe";
                        worksheet.Cell(1, 4).Value = "Banco";
                        worksheet.Cell(1, 5).Value = "Observaciones";
                        worksheet.Cell(1, 6).Value = "ID Agencia";

                        // Agregar datos
                        int fila = 2;
                        foreach (var deposito in listaDeposito)
                        {
                            worksheet.Cell(fila, 1).Value = deposito.Id;
                            worksheet.Cell(fila, 2).Value = deposito.FechaDeposito;
                            worksheet.Cell(fila, 3).Value = deposito.ImporteDeposito;
                            worksheet.Cell(fila, 4).Value = deposito.BancoDeposito;
                            worksheet.Cell(fila, 5).Value = deposito.ObservacionesDepositos;
                            worksheet.Cell(fila, 6).Value = deposito.idAgencia;
                            fila++;
                        }

                        // Guardar el archivo
                        using (var saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                            saveFileDialog.Title = "Guardar como Excel";

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                workbook.SaveAs(saveFileDialog.FileName);
                                MessageBox.Show("Datos exportados exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar a Excel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            if (gridDeposito.DataSource is List<Deposito> listaDeposito && listaDeposito.Count > 0)
            {
                StringBuilder csvContent = new StringBuilder();
                csvContent.AppendLine("Id,FechaDeposito,ImporteDeposito,ObservacionesDepositos,idAgencia");

                foreach (var deposito in listaDeposito)
                {
                    csvContent.AppendLine($"{deposito.Id},{Convert.ToDateTime(deposito.FechaDeposito)},{deposito.ImporteDeposito},{deposito.BancoDeposito},{deposito.ObservacionesDepositos},{deposito.idAgencia}");
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Guardar como CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, csvContent.ToString());
                    MessageBox.Show("Datos exportados exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
