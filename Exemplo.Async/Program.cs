 
using Exemplo.Async;
using Exemplo.Async.Classe;

string opcao;
do
{
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Inicializa as máquinas (síncrono) ");
    Console.WriteLine("2 - Inicializa as máquinas (assíncrono) Await");
    Console.WriteLine("3 - Inicializa as máquinas (assíncrono) Await cancellationToken");
    Console.WriteLine("4 - Inicializa as máquinas (assíncrono) No Await");

    Console.WriteLine("5 - Contador");
    Console.WriteLine("6 - Contador lock");

    Console.WriteLine("7 - Listar cliente");

    Console.WriteLine("0 - Sair");
    Console.Write("Opção: ");

    opcao = Console.ReadLine();

    CancellationToken cancellationToken = new CancellationToken();
    ProgramacaoDia programacaoDia = new ProgramacaoDia();
    Cliente cliente = new Cliente();

    switch (opcao)
    {
        case "1":
            programacaoDia.MaquinasTrabalhando();
            break;

        case "2":
            await programacaoDia.MaquinasTrabalhandoAsync(cancellationToken);
            break;

        case "3":
            await programacaoDia.MaquinasTrabalhandoAsyncCancellationToken();
            break;

        case "4":
            await programacaoDia.MaquinasTrabalhandoAsyncNoAwait();
            break;

        case "5":
            GeradorContador.ContadorProdutos();
            break;

        case "6":
            GeradorContador.ContadorProdutosLock();
            break;

        case "7":
            await cliente.ListarClientesAsync();
            break;

        case "0":
            break;

        default:
            Console.WriteLine("Opção inválida.");
            break;
    }

    Console.WriteLine();
} while (opcao != "0");

