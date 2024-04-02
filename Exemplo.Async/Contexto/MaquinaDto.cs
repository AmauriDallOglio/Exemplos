using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemplo.Async.Contexto
{
    public class MaquinaDto
    {
        public int Contador { get; set; }
        public void Incrementar()
        {
            Contador++;
        }

  


        public void IncrementarLock()
        {
            lock (this)
            {
                Contador++;
            }
        }
    }
}
