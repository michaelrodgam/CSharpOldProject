using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoPrograAvanzada
{
    public class Jugador
    {
        int numJugador;
        int puntaje;
        List <Cartas> mano;
        bool terminado = false;

    public Jugador(int num) 
    {
            numJugador = num;
            puntaje = 0;
            mano = new List<Cartas>();
        }

    public void agregarCarta(Cartas nuevaCarta)
    {
            mano.Add(nuevaCarta);
    }

    public void calcularPuntaje() 
    {
            int cantidadActual;
            int i;
            puntaje = 0;

            cantidadActual = mano.Count();
            for (i=0;i<cantidadActual;i++) 
            {
                puntaje += mano[i].getValue(3);
            }

            if(puntaje>21) 
            { 
                for(i=0;i<cantidadActual;i++) 
                {
                 if(mano[i].getValue(2)==14) 
                 {
                        puntaje -= 10; 
                 }
                }
            }
    }

    public int getValue(int tipo) 
    { 
        if(tipo ==0) 
        {
                return numJugador; 
        }else 
        {
                return puntaje;
        }
    }

    public void setValue(int valor) 
    {
            numJugador = valor;
    }

    public bool estadoJ() 
    {
            return terminado;
    }

    public void setTerminado(bool estado) 
    {
            terminado = estado;
    }

        public bool ganadorAutoma()
        {
            bool resultado = false;
            if (mano.Count() > 1)
            {
                bool condicion1 = mano[0].getValue(3) == 11 || mano[1].getValue(3) == 11;
                bool condicion2 = mano[0].getValue(3) == 10 || mano[1].getValue(3) == 10;
                

                if (condicion1 && condicion2)
                {
                    resultado = true;
                }
            }

            return resultado;
        }


    }
}
