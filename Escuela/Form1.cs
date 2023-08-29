using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Escuela
{
    public partial class Form1
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            string nombre = txtNombre.Text.Trim();//se utiliza para eliminar espacio en blanco al comienzo o final del texto.
            string apellido = txtApellido.Text.Trim();
            string dniString = txtDni.Text.Trim();
            DateTime fechaNacimiento = dtpFN.Value;
            string fechaNacimientoFormateada = fechaNacimiento.ToString("dd/MM/yyyy");

            
            if (ValidarNombreApellido(nombre, apellido) && ValidarDNI(dniString))
            {
                int dni;

                if (int.TryParse(dniString, out dni))
                {
                    AgregarPersona(nombre, apellido, fechaNacimiento, dni);
                }
                else
                {
                    MessageBox.Show("DNI inválido.");
                }
            }
            else
            {
                MessageBox.Show("Nombre, apellido o DNI inválido(s).");
            }

            dataBase.Conexion();
        }
        private bool ValidarNombreApellido(string nombre, string apellido)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(nombre, "^[a-zA-Z]{3,}$") &&
                   System.Text.RegularExpressions.Regex.IsMatch(apellido, "^[a-zA-Z]{3,}$");
        }

        private bool ValidarDNI(string dniString)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(dniString, "^[0-9]{8}$");
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (DateTime.Now.Month < fechaNacimiento.Month || (DateTime.Now.Month == fechaNacimiento.Month && DateTime.Now.Day < fechaNacimiento.Day))
            {
                edad--;
            }
            return edad;
        }
        private void AgregarPersona(string nombre, string apellido, DateTime fechaNacimiento, int dni)
        {
            int edad = CalcularEdad(fechaNacimiento);

            if (edad >= 16 && edad <= 60)
            {
                string fechaNacimientoFormateada = fechaNacimiento.ToString("dd/MM/yyyy");
                dataGridView1.Rows.Add(new object[] { nombre, apellido, fechaNacimientoFormateada, dni, "Eliminar" });

                BorrarCampos(splitContainer1);
            }
            else
            {
                MessageBox.Show("La edad no cumple con los requisitos.");
            }
        }

        public void BorrarCampos(SplitContainer splitContainer)
        {
            foreach (var upperControl in splitContainer.Panel1.Controls)
            {
                if (upperControl is TextBox upperTextBox)
                {
                    upperTextBox.Clear();
                }
            }
        }

        //Este método maneja el evento CellClick del DataGridView. 
        //Si se hace clic en una celda de la columna "Eliminar", elimina la fila correspondiente de la tabla, 
        //lo que permite al usuario eliminar registros haciendo clic en esa celda específica.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex < 0  || e.ColumnIndex != dataGridView1.Columns["Eliminar"].Index ) return;

            dataGridView1.Rows.RemoveAt(e.RowIndex);
        
        }
    }
}
