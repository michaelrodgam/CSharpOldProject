using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProyectoPrograAvanzada
{
    public class Cartas
    {
        int tipos; //0 corazones, 1 espadas, 2 diamantes, 3 treboles;
        int posiciones; //2,3...J=11,Q=12,K=13,A=14.
        int valores; //1,2,3...10,J,Q,K = 10, As=11;
        string nombre;
        string tipoString;
        string posString;

        public Cartas(int tipo, int posicion) 
        {
            tipos = tipo;
            posiciones = posicion;
            nombrarCarta(tipos, posiciones);
            if (posiciones > 10)
            {
                valores = 10;
                if(posiciones==14) 
                {
                    valores = 11;
                }
            }
            else
            {
                valores = posiciones;
            }
        }

        private void nombrarCarta(int tip, int poc) 
        {
            string nam1, nam2;
            switch(tip) 
            {
                case 0: nam1 = "Corazones."; break;
                case 1: nam1 = "Espadas."; break;
                case 2: nam1 = "Diamantes."; break;
                case 3: nam1 = "Treboles."; break;
                default: nam1="Errores."; break; 
            }
            switch (poc) 
            {
                case 2: nam2 = "Dos"; break;
                case 3: nam2 = "Tres"; break;
                case 4: nam2 = "Cuatro"; break;
                case 5: nam2 = "Cinco"; break;
                case 6: nam2 = "Seis"; break;
                case 7: nam2 = "Siete"; break;
                case 8: nam2 = "Ocho"; break;
                case 9: nam2 = "Nueve"; break;
                case 10: nam2 = "Diez"; break;
                case 11: nam2 = "Jota"; break;
                case 12: nam2 = "Reina"; break;
                case 13: nam2 = "Rey"; break;
                case 14: nam2 = "As"; break;
                default: nam2 = "Error"; break;
            }
            tipoString = nam2;
            posString = nam1;
            nombre = nam2 + " de " + nam1;
        }

        public string getNombre() { return nombre;}

        public int getValue(int valor) 
        { 
        switch(valor) 
        {
                case 1: return tipos;
                case 2: return posiciones;
                case 3: return valores;
                default: return -1;
        }
        }

        public string path() 
        {
            
            string pathD = Directory.GetCurrentDirectory(); 
            string path = pathD + "\\naipe\\" + getValue(1) + "p" + getValue(2) + ".png";
            return path;
        }

        public static Cartas generarCarta(string nombre) 
        {
            string[] val;
            val = nombre.Split(',');//primero el tipo y luego la posicion.
            Cartas nueva = new Cartas(int.Parse(val[0]), int.Parse(val[1]));
            return nueva;
        }
    }
}
