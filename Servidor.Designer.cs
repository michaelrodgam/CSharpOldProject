using System;
using System.Windows.Forms;

namespace ProyectoPrograAvanzada
{
    partial class Servidor
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Jugadores Conectados: 0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cartas restantes en la Baraja: 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Puntaje J1: 0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Puntaje J2: 0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 36);
            this.button1.TabIndex = 5;
            this.button1.Text = "Activar servidor";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Estado del servidor: Inactivo.";
            // 
            // Servidor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 257);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Servidor";
            this.Text = "Servidor";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.FormClosing += new FormClosingEventHandler(Servidor_FormClosing);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        //this.FormClosing += new FormClosingEventHandler(Servidor_FormClosing);

        public void estadoConexion(int valor)
        {
            try
            {
                
                
            
            switch (valor) {
                case 1:
                    this.Invoke((Action)(() => label6.Text="Estado del servidor: En linea."));
                    break;
                case 2:
                    this.Invoke((Action)(() => label6.Text = "Estado del servidor: Desconectado."));
                    break;
                case 3:
                    this.Invoke((Action)(() => label6.Text = "Estado del servidor: Esperando conexion."));
                    break;
                default:
                    this.Invoke((Action)(() => label6.Text = "Estado del servidor: error!.")); 
                    break;
            }
            }
            catch (System.InvalidOperationException) { MessageBox.Show("Error al modifical label6"); }
        }

        public void jugadoresConectados() 
        {
            this.Invoke((Action)(() => label1.Text = "Conexiones establecidas: "+numJugador));
        }

        public void cartasRestantes()
        {
            this.Invoke((Action)(() => label2.Text = "Cartas restantes en la baraja: " + baraja.Count));
        }

        public void puntajeJugador(Jugador jugador)
        {
            if (jugador.getValue(0) == 1)
            {
                this.Invoke((Action)(() => label4.Text = "Puntaje J" + jugador.getValue(0) + " = " + jugador.getValue(1)));
            }
            else
                this.Invoke((Action)(() => label5.Text = "Puntaje J" + jugador.getValue(0) + " = " + jugador.getValue(1)));
        }

        public void ocultarBoton() 
        {
            this.Invoke((Action)(() => button1.Text = "Desactivar"));
        }
    }
}

