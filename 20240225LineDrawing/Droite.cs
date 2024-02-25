using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20240225LineDrawing
{
    // j'ai créé une simple class pour représenter une Droite.
    // pour la simplicité, j'ai juste tout rendu public, ici.
    // pas de getter et setter...
    public class Droite
    {
        public int x1,y1, x2, y2;

        public Droite() // 1er constructeur
        {
            x1 = 0;
            y1 = 0;
            x2 = 0;
            y2 = 0;
        }
        public Droite(int xx1, int yy1, int xx2, int yy2) // 2ème constructeur
        {
            x1 = xx1;
            y1 = yy1;
            x2 = xx2;
            y2 = yy2;
        }

        public void reinitialiser()
        {
            x1 = 0;
            y1 = 0;
            x2 = 0;
            y2 = 0;
        }
    }
}
