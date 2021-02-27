using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KliczkowskiSzubert
{
    public abstract class Agent : IRunnable
    {
        protected float timeStep;
        public int Id;
        protected float timeTic = 0.0f;

        public Agent(int id, float time_step = 0.1f)
        {
            this.timeStep = time_step;
            this.Id = id;
        }

        public void Finish() { HasFinished = true; t_finished = true; }

        public bool HasFinished { get; private set; } = false;

       public bool t_finished = false;
        public void Run()
        {
            while (!HasFinished)
            {
                Update();
                timeTic += timeStep;
                Thread.Sleep((int)Math.Round(timeStep * 1000.0f));
            }
        }

        public IEnumerator<float> CoroutineUpdate()
        {
            while (!HasFinished)
            {
                Update();
                timeTic += timeStep;
                if (HasFinished)
                    yield break;
                else
                    yield return timeTic;
            }

        }

        public abstract void Update();
    }
}
