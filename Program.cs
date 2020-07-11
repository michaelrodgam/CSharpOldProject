using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPrograAvanzada
{
    static class Program
    {
        static MenuPrincipal ventana;
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ventana = new MenuPrincipal();
            Application.EnableVisualStyles();
            Application.Run(ventana);
        }

        public static void showMenu() 
        {
            ventana.Visible = true;
        }
    }
}
