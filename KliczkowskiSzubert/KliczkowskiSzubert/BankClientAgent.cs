using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KliczkowskiSzubert
{
    class BankClientAgent : Agent
    {
        private int stan_konta = 0;
        private int zmiana_stanu;
        private Thread t;
        private BankAgent bank;
        private int i = 0;
        Object myLock = new object();
    

        // System.Threading.Mutex semafor;

        /*    public void Myprint()
            {
                while (!this.t_finished)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Agent o id: " + Id + " Stan konta :" + bank.stan_konta);
                }
            } */
        public BankClientAgent(int id, ref BankAgent bank, int zmiana_stanu, ref Object myLock) : base(id)
        {
            this.zmiana_stanu = zmiana_stanu;
            this.bank = bank;
            this.myLock = myLock;
            base.timeStep = 0.5f;
         

            //  this.t = new Thread(this.Myprint);
            // this.t.Start();
        }

        public override void Update()
        {
 
      
            bool lockTaken = false;
            try
            {
                bank.mySpinLock.Enter(ref lockTaken);
                int a = 0;
                do
                {
                    a = this.stan_konta;
                } while (a != Interlocked.CompareExchange(ref this.stan_konta, bank.Change(zmiana_stanu), a));
                //this.stan_konta = bank.Change(zmiana_stanu);

                Console.WriteLine("Agent o id: " + Id + " Stan konta :" + this.stan_konta);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.StackTrace);
            }
            finally
            {
                if (lockTaken)
                {
                    bank.mySpinLock.Exit();
                }
            }
         

            if (bank.HasFinished) { Finish(); }
            
        }
    }
}
