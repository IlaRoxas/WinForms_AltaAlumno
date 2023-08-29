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

            // Validar que el nombre y el apellido contenga solo letras y tenga más de 3 caracteres
            if (System.Text.RegularExpressions.Regex.IsMatch(nombre, "^[a-zA-Z]{3,}$")&& 
                System.Text.RegularExpressions.Regex.IsMatch(apellido, "^[a-zA-Z]{3,}$")&&
                System.Text.RegularExpressions.Regex.IsMatch(dniString, "^[0-9]{8}$"))// Validar que el dni tenga 8 caracteres
            {

                int dni;
                if (int.TryParse(dniString, out dni))//Se convierte el texto a un valor entero
                                                    //utilizando int.TryParse().
                                                   //Si la conversión es exitosa, el valor se almacena en 'dni'.
                {
                    // Calcular la edad
                    int edad = DateTime.Now.Year - fechaNacimiento.Year;
                    if (DateTime.Now.Month < fechaNacimiento.Month || (DateTime.Now.Month == fechaNacimiento.Month && DateTime.Now.Day < fechaNacimiento.Day))
                    {
                        edad--;
                    }
                    // Validar la edad entre 8 y 60 años
                    if (edad >= 16 && edad <= 60)
                    {
                        // Los campos nombre, apellido, DNI y edad cumplen con los requisitos
                        // Agrego los datos al DataGridView
                        dataGridView1.Rows.Add(new object[] { nombre, apellido, fechaNacimientoFormateada, dni, "Eliminar" });
                        
                        Limpiar limpiador = new Limpiar();
                        limpiador.BorrarCampos(splitContainer1);

                    }
                    else
                    {
                        MessageBox.Show("La edad debe estar entre 16 y 60 años.");
                    }
                }
                else
                {
                    MessageBox.Show("El DNI debe ser un número válido.");
                }
            }
            else
            {
                MessageBox.Show("El nombre, el apellido y el DNI deben cumplir con los requisitos.");
            }

            dataBase.Conexion();
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
