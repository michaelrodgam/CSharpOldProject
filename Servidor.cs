using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace ProyectoPrograAvanzada
{
    public partial class Servidor : Form
    {
        private Thread proceso;
        private static int numJugador= 0;
        private Thread[] procesoJugador;
        private Conexion[] conexiones;
        private Socket rechazar;
        private NetworkStream salida;
        private BinaryWriter escritor;
        private BinaryReader lector;
        private string mensaje;
        private Cartas[] naipe = new Cartas[52];
        private Cartas[] naipe2 = new Cartas[52];
        private Stack <Cartas> baraja;
        private bool activo=false;
        private int terminado=0;
        private int turno = 0;


        public Servidor()
        {
            InitializeComponent();
        }

        public void ejecutarServidor() 
        {
            TcpListener oyente; //declara tcplistener
            procesoJugador = new Thread[3];
            conexiones = new Conexion[3];
            try
            {
                IPAddress local = IPAddress.Parse("127.0.0.1"); //crea tcplistener
                oyente = new TcpListener(local, 6000);
                oyente.Start(); //tcplistener esperando conexion
                estadoConexion(3);

                while (terminado < 2)
                {

                    if (numJugador == 0)
                {
                    jugadoresConectados();
                    numJugador++;
                    conexiones[numJugador - 1] = new Conexion(oyente.AcceptSocket(), numJugador, this);
                    estadoConexion(1);
                    jugadoresConectados();
                    procesoJugador[numJugador - 1] = new Thread(new ThreadStart(conexiones[numJugador - 1].ejecutar));
                    procesoJugador[numJugador - 1].Start();
                    jugadoresConectados();
                    estadoConexion(1);
                }

                if (numJugador == 1)
                {
                    jugadoresConectados();
                    numJugador++;
                    conexiones[numJugador - 1] = new Conexion(oyente.AcceptSocket(), numJugador, this);
                    estadoConexion(1);
                    jugadoresConectados();
                    procesoJugador[numJugador - 1] = new Thread(new ThreadStart(conexiones[numJugador - 1].ejecutar));
                    procesoJugador[numJugador - 1].Start();
                    jugadoresConectados();
                    estadoConexion(1);
                }

               
                    if (numJugador == 2)
                    {
                        rechazar = oyente.AcceptSocket();
                        salida = new NetworkStream(rechazar);
                        escritor = new BinaryWriter(salida);
                        lector = new BinaryReader(salida);
                        escritor.Write("3");
                        escritor.Write("cerrar");
                        mensaje = lector.ReadString();
                        if (mensaje == "cerradoCliente")
                        {
                            escritor.Close();
                            lector.Close();
                            salida.Close();
                            rechazar.Close();
                        }
                    }
                }



            }
            catch (Exception e) 
            {
                MessageBox.Show("error en el servidor!"+e.Message);
            }
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (activo == false)
            {
                crearBaraja();
                barajarCartas();
                cartasRestantes();
                proceso = new Thread(new ThreadStart(ejecutarServidor));
                proceso.Start();
                ocultarBoton();
                activo = true;
            }
            else
            {
                this.Close();
            }
        }

        private void Servidor_FormClosing(object sender, FormClosingEventArgs e) 
        {
            
            System.Environment.Exit(System.Environment.ExitCode);
        }

        public void barajarCartas()
        {
            Random rnd = new Random();
            int i = 0;
            int val = 0;
            while (i < 52)
            {
                val = rnd.Next(0, 52);
                if (naipe2[val] == null) 
                { 
                    naipe2[val] = naipe[i];
                    i++;
                }
            }

            baraja = new Stack<Cartas>();
            for(i=0; i<=51; i++) 
            {
                baraja.Push(naipe2[i]);
            }

        }

        public void crearBaraja() 
        {
            int i;
            int n;
            int m = 0;
            for (n = 0; n <= 3; n++)
            {
                for (i = 2; i <= 14; i++)
                {
                    naipe[m] = new Cartas(n, i);
                    m++;
                }
            }
        }

        public Cartas obtenerCarta() 
        {
            return baraja.Pop();
        }

        public void setNumJugadores(int valor) 
        {
            numJugador += valor;
            if(numJugador < 0) { numJugador = 0; }
        }

        public int getTerminado() 
        {
            return terminado;
        }

        public void setTerminado() 
        { 
            terminado += 1; 
        }

        public int getTurno()
        {
            return turno;
        }

        public void setTurno( int a)
        {
            turno = a;
        }

        public void verificarTurno() 
        {
           if(turno == 0) { conexiones[0].enviarMensaje("turno"); }
           if(turno == 1) { conexiones[1].enviarMensaje("turno"); }
        }

        public void declararGanador() 
        { 
        if(terminado==2) 
        {
                int p1 = conexiones[0].getPuntaje();
                int p2 = conexiones[1].getPuntaje();
                if(p1>p2 && p1 <= 21) 
                { 
                    conexiones[0].enviarMensaje("win");
                    conexiones[1].enviarMensaje("lose");
                }
                else if(p1<p2 && p2 <= 21) 
                {
                    conexiones[1].enviarMensaje("win");
                    conexiones[0].enviarMensaje("lose");
                }
                else if(p2 < p1 && p2 > 21) 
                {
                    conexiones[1].enviarMensaje("win");
                    conexiones[0].enviarMensaje("lose");
                }
                else if (p1 < p2 && p1 > 21)
                {
                    conexiones[0].enviarMensaje("win");
                    conexiones[1].enviarMensaje("lose");
                }
                else if (p1 > 21 && p2 < 21) 
                {
                    conexiones[1].enviarMensaje("win");
                    conexiones[0].enviarMensaje("lose");
                }
                else if (p2 > 21 && p1 < 21)
                {
                    conexiones[0].enviarMensaje("win");
                    conexiones[1].enviarMensaje("lose");
                }
                else 
                {
                    conexiones[0].enviarMensaje("draw");
                    conexiones[1].enviarMensaje("draw");
                }
        }
        }

        public void cerrar() 
        {
            this.Invoke((Action)(() => Close()));
        }




    }
}
