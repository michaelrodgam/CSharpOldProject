using System;
using System.Collections.Generic;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;

namespace ProyectoPrograAvanzada
{
    public partial class Cliente : Form
    {

        private Thread proceso;
        private NetworkStream salida;
        private BinaryWriter escritor;
        private BinaryReader lector;
        private string mensaje;
        internal bool suspendido = false;
        private bool conectado = false;
        private Jugador jugador;
        private TcpClient cliente;
        private int numeroCartas = 0;

        public Cliente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!suspendido)
            {
                if (!conectado)
                {
                    proceso = new Thread(new ThreadStart(conectarServidor));
                    proceso.Start();
                    botonDesconectar(0);
                    conectado = true;
                }
                else
                {
                    try
                    {
                        escritor.Write("desconectar");
                        conectado = false;
                    }
                    catch(Exception) { Close(); }
                }
            }
        }

        private void Cli_FormClosing(object sender, FormClosingEventArgs e)
        {

            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void conectarServidor() 
        {
            estadoConexion(3);
                try
                {
                    cliente = new TcpClient();
                    cliente.Connect("127.0.0.1", 6000);
                    salida = cliente.GetStream();
                    escritor = new BinaryWriter(salida);
                    lector = new BinaryReader(salida);
                    mensaje = lector.ReadString();
                    numeroJugador(mensaje);
                    jugador = new Jugador(int.Parse(mensaje));
                    if (int.Parse(mensaje) > 2)
                    {
                        MessageBox.Show("El servidor esta lleno!");
                        escritor.Write("cerradoCliente");
                        suspendido = true;
                    }
                    while (conectado)
                    {
                        
                        ejecutar(lector.ReadString());
                    }
                }
                catch (Exception)
                {
                    
                    MessageBox.Show("Se perdió la conexion con el servidor");
                    conectado = false;
                    suspendido = true;
                    estadoConexion(2);
                    numeroJugador("-");
            }
        }

        private void cerrarConexion() 
        {
            cliente.Close();
            salida.Close();
            lector.Close();
            escritor.Close();
            estadoConexion(2);
            numeroJugador("-");
            suspendido = true;
            Close();
        }

        private void ejecutar(string mensaje) 
        { 
            switch(mensaje) 
            {
                case "cerrar": //cierra el formulario.
                {
                    cerrarConexion();
                    break; 
                }
                case "conectado": 
                {
                        MessageBox.Show("Conexion satisfactoria");
                        estadoConexion(1);
                        break; 
                }
                case "terminado": 
                { 
                    jugador.setTerminado(true); 
                    jugador.calcularPuntaje();
                    mostrarPuntaje(jugador.getValue(1));
                    estadoConexion(4);
                    break; 
                }
                case "win":
                    {
                        jugador.setTerminado(true);
                        jugador.calcularPuntaje();
                        mostrarPuntaje(jugador.getValue(1));
                        estadoConexion(5);
                        conectado = false;
                        suspendido = true;
                        break;
                    }
                case "lose":
                    {
                        jugador.setTerminado(true);
                        jugador.calcularPuntaje();
                        mostrarPuntaje(jugador.getValue(1));
                        estadoConexion(6);
                        conectado = false;
                        suspendido = true;
                        break;
                    }
                case "draw":
                    {
                        jugador.setTerminado(true);
                        jugador.calcularPuntaje();
                        mostrarPuntaje(jugador.getValue(1));
                        estadoConexion(7);
                        conectado = false;
                        suspendido = true;
                        break;
                    }
                case "espera": 
                { 
                    suspendido = true;
                    estadoConexion(4);
                    if (jugador.getValue(0) == 1) 
                    { 
                        estadoConexion(9);
                    }  
                    break; 
                }
                case "turno":
                    {
                        suspendido = false;
                        estadoConexion(8);
                        break;
                    }

                default: //recibe la carta y la agrega.
                {
                    Cartas nueva = Cartas.generarCarta(mensaje);
                    numeroCartas++;
                    jugador.agregarCarta(nueva);
                    MessageBox.Show("Recibió la carta: "+nueva.getNombre());
                    jugador.calcularPuntaje(); 
                    mostrarPuntaje(jugador.getValue(1));
                    setPictureBox(numeroCartas,nueva.path());
                    break; 
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!suspendido)
            {
                if (conectado)
                {
                    if (conectado == true && !jugador.estadoJ())
                    {
                        escritor.Write("carta");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!suspendido)
            {
                if (conectado)
                {
                    escritor.Write("terminado");
                }
            }
        }
    }
}
