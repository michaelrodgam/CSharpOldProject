using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPrograAvanzada
{
    public partial class MenuPrincipal : Form
    {
        

        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Servidor ser = new Servidor();
            ser.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cliente cli = new Cliente();
            cli.Show();
 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

    }
}
