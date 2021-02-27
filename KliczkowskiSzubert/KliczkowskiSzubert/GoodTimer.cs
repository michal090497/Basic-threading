using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace KliczkowskiSzubert
{
    class GoodTimer
    {
        private int goStart = 0;
        private int dueTime = 0;
        private int periodTime; // [ms]
        private string sValue;
        private int state = 0;
        ura uklad;
        int a = 0;
        public bool can_start = true;
        
        
        //List<Thread> threads = new List<Thread>(5);
        public GoodTimer(int periodTime, int a)
        {
            this.a = a;
            this.periodTime = periodTime;
            uklad = new ura(state, periodTime);
            var t = new Thread(uklad.oblicz_wspolczynniki);
            //t_ob = new Thread(() => cos = uklad.obiekt(state, uklad.regulator(state)) );
            //double cos = t_ob.;
            //var t_jakosc = new Thread(uklad.oblicz_jakosc(0.5, state));
            //threads.Add(t);
            t.Start();

            //t_ob.Start();
            //uklad.oblicz_wspolczynniki();
        }

        public void StartGoodTimer()
        {
            goStart = Environment.TickCount;
            //Console.WriteLine (" Podaj   wartosc   wzmocnienia  Kp i potwierdz Enter ");
            //sValue = Console.ReadLine();
            Timer gTimer = new Timer(new TimerCallback(CallbackGoodMethod), uklad, dueTime, periodTime);
            Console.WriteLine(" Nacisnij   dowolny   klawisz , aby zakonczyc program.");
            Console.Read();
            gTimer.Dispose();
        }

        private void CallbackGoodMethod(object gStateObject)
        {
            //Console.WriteLine (" Potrzebowałes  {0}  ms , aby  wprowadzic  wartosc   {1}. ",Environment . TickCount - goStart ,gStateObject.ToString () );
            //Console.WriteLine(" Potrzebowałes  {0}  ms , aby  wprowadzic  wartosc   {1}. ", Environment.TickCount - goStart, (Environment.TickCount - goStart)/this.periodTime);
            //Console.WriteLine(" Potrzebowałes  {0}  ms , aby  wprowadzic  wartosc   {1}. ", Environment.TickCount - goStart, state);
            //state++;
            /*if(state==2)
            {
                uklad.wypisz_wspolczynniki();
            }*/



            uklad.oblicz();

            /*Thread t_ob;

            t_ob = new Thread(uklad.obiekt);
            while (uklad.start == false)
            { };
            t_ob.Start();*/




            //double cos=1;
            //t_ob = new Thread(() => uklad.obiekt(state, uklad.regulator(state)));
            //if(uklad.start==true)
            //while(t_ob.ThreadState==ThreadState.Running)
            //{
            //    Thread.Sleep(500);
            //}

            //double y=uklad.obiekt(state, uklad.regulator(state));

            //t_ob.Join();
            //Console.WriteLine(periodTime + "wartosc cos: "+cos);

            //uklad.wypisz_wspolczynniki();
            //uklad.oblicz_jakosc(y, state);
        }
    }
}
