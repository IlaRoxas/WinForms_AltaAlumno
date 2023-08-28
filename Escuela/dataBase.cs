using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
namespace Escuela
{
    class dataBase
    {
        //Se crea una instancia de conexión a una base de datos MySQL,
        //intento abrir la conexión y muestra un mensaje si la conexión es exitosa o si ocurre un error.
        public static MySqlConnection conexionBD = new MySqlConnection("server = 127.0.0.1 ; database = escuela ; Uid = root ; pwd = basededatos2020;"); 
        public static void Conexion()
        {
            try { 
                conexionBD.Open();
                MessageBox.Show("Conectado");
                conexionBD.Close();
            
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conexionBD.Close(); }
        }
    }
}
