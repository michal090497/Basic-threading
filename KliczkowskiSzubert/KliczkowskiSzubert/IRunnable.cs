using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KliczkowskiSzubert
{

    interface IRunnable
    {
        void Run();

        bool HasFinished { get; }
        IEnumerator<float> CoroutineUpdate();
    }

}
