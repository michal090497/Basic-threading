using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace KliczkowskiSzubert
{
    class ura
    {
        public int state = 0;
        public double[] a = new double[3];
        public double[] b = new double[2];
        public double[] k = new double[3];
        public double Tp;
        public double wart_zadana = 1;
        public double u = 0;
        public double u1 = 0;
        public double u2 = 0;
        public double y = 0;
        public double y1 = 0;
        public double y2 = 0;
        public double e = 0;
        public double e1 = 0;
        public double e2 = 0;
        public double Ti = 100;
        public double Td = 0.05;
        public double kp = 0.001;
        public double jakosc;
        private int licznik = 0;
        public bool start = true;
        public ura(int state, double Tp)
        {
            this.state = state;
            Console.WriteLine("konstruktor ura: "+state);
            this.Tp = (Double)Tp/1000;
            jakosc = 0;
    }
        public void oblicz_wspolczynniki()
        {
            a[0] = (Tp * Tp) / (4 + 4 * Tp + 3 * Tp * Tp);
            a[1] = (2 * Tp * Tp) / (4 + 4 * Tp + 3 * Tp * Tp);
            a[2] = a[0];

            b[0] = (6 * Tp * Tp - 8) / (4 + 4 * Tp + 3 * Tp * Tp);
            b[1] = (4 - 4 * Tp + 3 * Tp * Tp) / (4 + 4 * Tp + 3 * Tp * Tp);

            k[0] = kp*(1+Tp/Ti+Tp/Td);
            k[1] = -kp*(1+2*Td/Tp);
            k[2] = kp*(Td/Tp);
            //Console.WriteLine("ura: "+state);
            Console.WriteLine(a[0] + " " + a[1] + " " + a[2] + " " + b[0] + " " + b[1]);
        }
        public void wypisz_wspolczynniki()
        {
            //Console.WriteLine(state + " " + a[0] + " " + a[1] + " " + a[2] + " " + b[0] + " " + b[1]);
            Console.WriteLine("wyjcie:" + y);
        }
        public void oblicz()
        {
            Thread t_ob= new Thread(obiekt);

            while (start == false)
            { };
            t_ob.Start();
        }

        public double regulator(int i)
        {
            //public double e = wart_zadana - y;
            e = wart_zadana - y;
            e1 = wart_zadana - y1;
            e2 = wart_zadana - y2;
            u = u1 + k[0] * e + k[1] * e1 + k[2] * e2;
            e2 = e1;
            e1 = e;
            y2 = y1;
            y1 = y;
            return u;
        }

        //public void obiekt(int i, double we)
        public void obiekt()
        {
            if (licznik > 2)
            {
                start = false;
                licznik++;
                double we = regulator(licznik);
                y = a[0] * we + a[1] * u1 + a[2] * u2 + b[0] * y1 + b[1] * y2;
                u2 = u1;
                u1 = we;
                //y2 = y1;
                //y1 = y;

                Console.WriteLine("wynik obliczen(ura " + Tp + "): " + y + " dla probki nr: " + licznik);
                //Thread t_jakosc;
                //t_jakosc = new Thread(() => oblicz_jakosc(y, i));
                //t_jakosc.Start();
                oblicz_jakosc(y, licznik);


            }
            else
            {
                licznik++;
            }

        }
        private void oblicz_jakosc(double y, int i)
        {
            jakosc += Math.Abs((wart_zadana-y)*i*i);
            Console.WriteLine("jakosc sterowania(ura " + Tp + "): " + jakosc + " dla probki nr: " + i);
            start = true;
        }


    }
}
