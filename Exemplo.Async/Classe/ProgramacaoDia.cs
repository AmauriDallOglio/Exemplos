using Exemplo.Async.Contexto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Exemplo.Async
{
    public class ProgramacaoDia
    {
        /// <summary>
        //  O método MaquinasTrabalhando é chamado.
        //  Um objeto Stopwatch é criado e a contagem de tempo é iniciada.
        //  As funções MaquinaUm, MaquinaDois e MaquinaTres são chamadas sequencialmente.
        //  Cada função imprime uma mensagem indicando que a máquina começou a trabalhar, aguarda 5 segundos usando Thread.Sleep(5000) e, em seguida, imprime uma mensagem indicando que o produto foi finalizado.
        //  Após a execução das três funções, a contagem de tempo é interrompida e o tempo decorrido é impresso no console.
        /// </summary>
        public void MaquinasTrabalhando()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Como as chamadas síncrono para MaquinaUm, MaquinaDois e MaquinaTres são feitas de forma sequencial e cada uma delas bloqueia a execução
            //por 5 segundos usando Thread.Sleep(5000), o tempo total de execução será aproximadamente 15 segundos.

            MaquinaDto maquina1 = MaquinaUm();
            MaquinaDto maquina2 = MaquinaDois();
            MaquinaDto maquina3 = MaquinaTres();


            MaquinaDto resultado1 = maquina1;
            Console.WriteLine($"{DateTime.Now} Máquina 1 despachado, {resultado1.Contador} itens");

            MaquinaDto resultado2 = maquina2;
            Console.WriteLine($"{DateTime.Now} Máquina 2 despachado, {resultado2.Contador} itens");

            MaquinaDto resultado3 = maquina3;
            Console.WriteLine($"{DateTime.Now} Máquina 3 despachado, {resultado3.Contador} itens");

            stopwatch.Stop();
            long tempoDecorridoMs = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"{DateTime.Now} Tempo total: {tempoDecorridoMs} ms");
        }


        private MaquinaDto MaquinaUm()
        {
            Console.WriteLine($"{DateTime.Now} Máquina 1 começou a trabalhar. Deve terminar em 5s!");
            Thread.Sleep(5000);
            MaquinaDto retorno = new MaquinaDto();
            for (int i = 0; i < 1000; i++)
            {
                retorno.Incrementar();
            }
            Console.WriteLine($"{DateTime.Now} Máquina 1 finalizado, {retorno.Contador} itens");
            return retorno;
        }

        private MaquinaDto MaquinaDois()
        {
            Console.WriteLine($"{DateTime.Now} Máquina 2 começou a trabalhar. Deve terminar em 5s!");
            Thread.Sleep(5000);
            MaquinaDto retorno = new MaquinaDto();
            for (int i = 0; i < 2000; i++)
            {
                retorno.Incrementar();
            }
            Console.WriteLine($"{DateTime.Now} Máquina 2 finalizado, {retorno.Contador} itens");
            return retorno;
        }

        private MaquinaDto MaquinaTres()
        {
            Console.WriteLine($"{DateTime.Now} Máquina 3 começou a trabalhar. Deve terminar em 5s");
            Thread.Sleep(5000);
            MaquinaDto retorno = new MaquinaDto();
            for (int i = 0; i < 3000; i++)
            {
                retorno.Incrementar();
            }
            Console.WriteLine($"{DateTime.Now} Máquina 3 finalizado, {retorno.Contador} itens");
            return retorno;
        }


        public async Task MaquinasTrabalhandoAsync(CancellationToken cancellationToken)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Tornando o código assíncrono para melhorar a eficiência e permitir que as máquinas trabalhem em paralelo,
            //você pode usar tarefas assíncronas e esperar que elas terminem. Isso permitirá que o tempo total de execução seja reduzido,
            //pois as máquinas podem trabalhar simultaneamente em vez de uma após a outra.
 

            Task<MaquinaDto> maquina1 = MaquinaUmAsync(cancellationToken);
            Task<MaquinaDto> maquina2 = MaquinaDoisAsync(cancellationToken);
            Task<MaquinaDto> maquina3 = MaquinaTresAsync(cancellationToken);
            await Task.WhenAll(maquina1,
                               maquina2,
                               maquina3);


            Console.WriteLine($"{DateTime.Now} Máquina 1 despachado, {maquina1.Result.Contador} itens");
            Console.WriteLine($"{DateTime.Now} Máquina 2 despachado, {maquina2.Result.Contador} itens");
            Console.WriteLine($"{DateTime.Now} Máquina 3 despachado, {maquina3.Result.Contador} itens");


            stopwatch.Stop();
            long tempoDecorridoMs = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"{DateTime.Now} Tempo total: {tempoDecorridoMs} ms");
        }

        public async Task MaquinasTrabalhandoAsyncCancellationToken()
        {
            try
            {
  
                var cancellation = new CancellationTokenSource();
                cancellation.CancelAfter(4000);
                await MaquinasTrabalhandoAsync(cancellation.Token);


            }
            catch (Exception)
            {

                await Console.Out.WriteLineAsync($"{DateTime.Now} Erro: Operação cancelada devido a demora <---");
            }

        }



        private async Task<MaquinaDto> MaquinaUmAsync(CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now} Máquina 1 começou a trabalhar. Deve terminar em 5s");
            await Task.Delay(5000, cancellationToken);
            //Thread.Sleep(5000);
            MaquinaDto retorno = new MaquinaDto();
            for (int i = 0; i < 1000; i++)
            {
                retorno.Incrementar();
            }
            await Console.Out.WriteLineAsync($"{DateTime.Now} Máquina 1 finalizado, {retorno.Contador} itens");
        
            return retorno;


        }


        private async Task<MaquinaDto> MaquinaDoisAsync(CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now} Máquina 2 começou a trabalhar. Deve terminar em 5s");
            await Task.Delay(5000, cancellationToken);
            //Thread.Sleep(5000);
            MaquinaDto retorno = new MaquinaDto();
            for (int i = 0; i < 2000; i++)
            {
                retorno.Incrementar();
            }
            await Console.Out.WriteLineAsync($"{DateTime.Now} Máquina 2 finalizado, {retorno.Contador} itens");

            return retorno;
        }


        private async Task<MaquinaDto> MaquinaTresAsync(CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now} Máquina 3 começou a trabalhar. Deve terminar em 5s");
            await Task.Delay(5000, cancellationToken);
            //Thread.Sleep(5000);
            MaquinaDto retorno = new MaquinaDto();
            for (int i = 0; i < 3000; i++)
            {
                retorno.Incrementar();
            }
            await Console.Out.WriteLineAsync($"{DateTime.Now} Máquina 3 finalizado, {retorno.Contador} itens");
            return retorno;
        }


        /// <summary>
        // O método MaquinasTrabalhandoAsyncNoAwait é chamado.
        // Um objeto Stopwatch é criado e a contagem de tempo é iniciada.
        // A função Task.WhenAll é chamada, passando três tarefas assíncronas como parâmetros. Essas tarefas correspondem às funções MaquinaUmAsyncNoAwait, MaquinaDoisAsyncNoAwait e MaquinaTresAsyncNoAwait.
        // Cada função imprime uma mensagem indicando que a máquina começou a trabalhar e, em seguida, bloqueia a execução por 5 segundos usando Thread.Sleep(5000). No entanto, como não há um operador await, essas chamadas não são realmente assíncronas e bloqueiam a execução da tarefa principal.
        // Após a execução das três tarefas, a contagem de tempo é interrompida e o tempo decorrido é impresso no console.
        /// </summary>
        /// <returns></returns>
        public async Task MaquinasTrabalhandoAsyncNoAwait()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //O código é assíncrona, onde cada função MaquinaUmAsyncNoAwait, MaquinaDoisAsyncNoAwait e MaquinaTresAsyncNoAwait
            //é marcada como assíncrona (async) e retorna uma tarefa (Task). No entanto, não há uso do operador await dentro dessas funções,
            //o que significa que elas não esperam por operações assíncronas dentro delas.


            Task<MaquinaDto> maquina1 = MaquinaUmAsyncNoAwait();
            Task<MaquinaDto> maquina2 = MaquinaDoisAsyncNoAwait();
            Task<MaquinaDto> maquina3 = MaquinaTresAsyncNoAwait();


             Task.WhenAll(maquina1, maquina2, maquina3);


            Console.WriteLine($"{DateTime.Now} Máquina 1 despachado, {maquina1.Result.Contador} itens");
            Console.WriteLine($"{DateTime.Now} Máquina 2 despachado, {maquina2.Result.Contador} itens");
            Console.WriteLine($"{DateTime.Now} Máquina 3 despachado, {maquina3.Result.Contador} itens");


            //Task<MaquinaDto> maquina1 = MaquinaUmAsyncNoAwait();
            //Task<MaquinaDto> maquina2 = MaquinaDoisAsyncNoAwait();
            //Task<MaquinaDto> maquina3 = MaquinaTresAsyncNoAwait();

            //await Task.WhenAll(maquina1, maquina2, maquina3);

            //MaquinaDto resultado1 = maquina1.Result;
            //Console.WriteLine($"Máquina 1 despachado, {resultado1.Contador} itens");


            //MaquinaDto resultado2 = maquina2.Result;
            //Console.WriteLine($"Máquina 2 despachado, {resultado2.Contador} itens");

            //MaquinaDto resultado3 = maquina3.Result;
            //Console.WriteLine($"Máquina 3 despachado, {resultado3.Contador} itens");


            stopwatch.Stop();
            long tempoDecorridoMs = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"{DateTime.Now} Tempo total: {tempoDecorridoMs} ms");
        }

        private async Task<MaquinaDto> MaquinaUmAsyncNoAwait()
        {
            Console.WriteLine($"{DateTime.Now} Máquina 1 começou a trabalhar. Deve terminar em 5s");
            //Task.Delay(1000);

            MaquinaDto retorno = new MaquinaDto();
    
            for (int i = 0; i < 1000; i++)
            {
                retorno.Incrementar();
                if (retorno.Contador >= 800)
                {
                    await Task.Delay(100);
                }
            }

            Console.WriteLine($"{DateTime.Now} Máquina 1 finalizado, {retorno.Contador} itens");
            return retorno;

        }


        private async Task<MaquinaDto> MaquinaDoisAsyncNoAwait()
        {
            Console.WriteLine($"{DateTime.Now} Máquina 2 começou a trabalhar. Deve terminar em 5s");

            //Task.Delay(2000);
            MaquinaDto retorno = new MaquinaDto();
            for (int i = 0; i < 1000; i++)
            {
                retorno.Incrementar();
                if (retorno.Contador >= 850)
                {
                    await Task.Delay(100);
                }
            }
            Console.WriteLine($"{DateTime.Now} Máquina 2 finalizado, {retorno.Contador} itens");
            return retorno;
        }

        private async Task<MaquinaDto> MaquinaTresAsyncNoAwait()
        {
            Console.WriteLine($"{DateTime.Now} Máquina 3 começou a trabalhar. Deve terminar em 5s");
            //Task.Delay(3000);

            MaquinaDto retorno = new MaquinaDto();
            for (int i = 0; i < 1000; i++)
            {
                retorno.Incrementar();
                if (retorno.Contador >= 900)
                {
                    await Task.Delay(100);
                }
            }
            Console.WriteLine($"{DateTime.Now} Máquina 3 finalizado, {retorno.Contador} itens");

            return retorno;
        }


        

       

    }
}
