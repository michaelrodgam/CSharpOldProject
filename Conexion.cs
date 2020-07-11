using System;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace ProyectoPrograAvanzada
{
    public class Conexion
    {
        internal Socket conexion;
        private NetworkStream socketStream;
        private BinaryWriter escritor;
        private BinaryReader lector;
        internal bool suspendido = false;
        private static Servidor serverRef;
        private Jugador jugador;
        private Cartas nuevaCarta;

    public Conexion(Socket direccion, int numero, Servidor ser) 
    {
            jugador = new Jugador(numero);
            conexion = direccion;
            socketStream = new NetworkStream(conexion);
            lector = new BinaryReader(socketStream);
            escritor = new BinaryWriter(socketStream);
            serverRef = ser;
            escritor.Write(jugador.getValue(0).ToString());
            escritor.Write("conectado");
            escritor.Write("espera");
            if (numero == 2) 
            {
                serverRef.verificarTurno();
            }
        }

    public void ejecutar()
        {
            while(!suspendido) 
            {
                string mensaje = lector.ReadString();
                switch (mensaje)
                {
                    case "cerradoCliente": 
                    {  
                        escritor.Write("cerrar"); 
                        serverRef.setNumJugadores(-1);
                        serverRef.jugadoresConectados();
                        suspendido=true;
                        break; 
                    }
                    case "carta": 
                    {
                        nuevaCarta = serverRef.obtenerCarta();
                        escritor.Write(enviarCarta(nuevaCarta)); 
                        agregarCarta(this, nuevaCarta);
                        jugador.calcularPuntaje();
                        serverRef.cartasRestantes();
                        serverRef.puntajeJugador(jugador);
                        estadoPartida();
                        break; 
                    }
                    case "terminado": 
                    { 
                        this.jugador.setTerminado(true); 
                        escritor.Write("terminado"); 
                        serverRef.setTerminado();
                        serverRef.declararGanador();
                        serverRef.setTurno(serverRef.getTurno()+1);
                        serverRef.verificarTurno();
                        break; 
                    }
                    case "desconectar": 
                    {
                            MessageBox.Show("Se ha desconectado del servidor.");
                            serverRef.cerrar();
                            break; 
                    }
                    default: break;
                }
            }   
              
        }

    public void cerrar() 
    {
            lector.Close();
            escritor.Close();
            conexion.Close();
            socketStream.Close();
     }

     public string enviarCarta(Cartas carta) 
     {
            string tipo=carta.getValue(1).ToString();
            string posicion= carta.getValue(2).ToString();
            return tipo +","+ posicion;
     }

     public void agregarCarta(Conexion conex, Cartas carta) 
     {
            conex.jugador.agregarCarta(carta);
     }

     public void estadoPartida() 
     {
            jugador.calcularPuntaje();
            int puntaje = jugador.getValue(1);
            if(puntaje>=21) 
            {
                this.jugador.setTerminado(true);
                escritor.Write("terminado");
                serverRef.setTerminado();
                serverRef.setTurno(serverRef.getTurno() + 1);
                serverRef.verificarTurno();
            } 
            if(jugador.ganadorAutoma()) 
            {
                serverRef.setTerminado();
                serverRef.setTerminado();
            }
            if(serverRef.getTerminado()>=2) 
            {
                serverRef.declararGanador(); 
            }
            
        }

     public void enviarMensaje(string x) 
     {
            escritor.Write(x);
     }

     public int getPuntaje() 
     {
            return jugador.getValue(1); 
     }

    }
}
