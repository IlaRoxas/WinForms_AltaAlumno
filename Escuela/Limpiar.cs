using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Escuela
{
    class Limpiar
    {
        public void BorrarCampos(Control control)
        {
            if (control is SplitContainer splitContainer)
            {
                foreach (var upperControl in splitContainer.Panel1.Controls)
                {
                    if (upperControl is TextBox upperTextBox)
                    {
                        upperTextBox.Clear();
                    }
                }

            }
        }
    }
}
