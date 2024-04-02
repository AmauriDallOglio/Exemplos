using Exemplo.Async.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemplo.Async.Classe
{
    public class GeradorContador
    {
        public static void ContadorProdutos()
        {
            MaquinaDto contator = new MaquinaDto();   
            var tasks = new List<Task>();

            for (int i = 0; i < 1000; i++)
            {
                tasks.Add(Task.Run(() => contator.Incrementar()));
            }

            Task.WhenAll(tasks).Wait();

            Console.WriteLine("Quantidde: " + contator.Contador);
        }

        public static void ContadorProdutosLock()
        {
            MaquinaDto contator = new MaquinaDto();
            var tasks = new List<Task>();

            for (int i = 0; i < 1000; i++)
            {
                tasks.Add(Task.Run(() => contator.IncrementarLock()));
            }

            Task.WhenAll(tasks).Wait();

            Console.WriteLine("Quantidde: " + contator.Contador);
        }
    }
}
