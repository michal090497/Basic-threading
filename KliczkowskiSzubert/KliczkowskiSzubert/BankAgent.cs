using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace KliczkowskiSzubert
{
    class BankAgent : Agent
    {
       private int stan_konta = 0;
        // private int update = 0;
        public Mutex semafor;
       
        private int counter = 0;
        Object myLock = new object();
        public SpinLock mySpinLock;

        public BankAgent(int id, ref Object myLock) : base(id)
        {
            this.semafor = new Mutex();
            base.timeStep= 2.0f;
            this.myLock = myLock;
            this.mySpinLock = new SpinLock();
        }

        public int Change(int i)
        {

            
        
                stan_konta = stan_konta + i;

                return stan_konta;
          
        }
            

        public override void Update()
        {

          
            bool lockTaken = false;
            try
            {
                this.mySpinLock.Enter(ref lockTaken);
                Console.WriteLine("-----------------------------------------------------------Stan konta: " + stan_konta);
                counter++;

            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.StackTrace);
            }
            finally
            {
                if (lockTaken)
                {
                    this.mySpinLock.Exit();
                }
            }
            

            if (counter >= 3) this.Finish();

        }
    }
}
