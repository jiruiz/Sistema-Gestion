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
using ClosedXML.Excel;
using System.IO;



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

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (gridAgencia.DataSource is List<Agencia> listaAgencias && listaAgencias.Count > 0)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Agencias");

                        // Agregar encabezados
                        worksheet.Cell(1, 1).Value = "Id";
                        worksheet.Cell(1, 2).Value = "Nombre";

                        // Agregar datos
                        int fila = 2;
                        foreach (var agencia in listaAgencias)
                        {
                            worksheet.Cell(fila, 1).Value = agencia.Id;
                            worksheet.Cell(fila, 2).Value = agencia.Nombre;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (gridAgencia.DataSource is List<Agencia> listaAgencias && listaAgencias.Count > 0)
            {
                StringBuilder csvContent = new StringBuilder();
                csvContent.AppendLine("Id,Nombre");

                foreach (var agencia in listaAgencias)
                {
                    csvContent.AppendLine($"{agencia.Id},{agencia.Nombre}");
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

        private void btnVolverInicio_Click(object sender, EventArgs e)
        {
            
            //Program.frmAgenci.Show();
            frmInicio ventanaInicio = new frmInicio();
            ventanaInicio.ShowDialog();  
        }

       
    }
    
    
}
