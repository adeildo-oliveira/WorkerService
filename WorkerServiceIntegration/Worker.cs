using BaseIntegrationCli360;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceIntegrationCli360
{
    public class Worker : BackgroundService
    {
        private readonly IBaseFinal _baseFinal;
        private readonly IBase1Integracao _base1Integracao;

        public Worker(IBaseFinal baseFinal, IBase1Integracao base1Integracao)
        {
            _baseFinal = baseFinal;
            _base1Integracao = base1Integracao;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IEnumerable<string> textos = new List<string>
            {
                "Execuntando tarefas!!!"
            };
            var contador = 1;

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine($"\nINICIANDO O SERVIÇO: {DateTimeOffset.Now}");
                    Console.WriteLine($"Inicio CurrentThread: {Thread.CurrentThread.ManagedThreadId}");

                    //await UseDataBase();
                    await CriandoEscrevendoArquivo(textos, stoppingToken);

                    Console.WriteLine($"Fim CurrentThread: {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"FIM DA EXECUÇÃO DO SERVIÇO: {DateTimeOffset.Now}");
                    await Task.Delay(3000, stoppingToken);
                    contador++;
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Erro: {erro}");
            }
        }

        private static async Task CriandoEscrevendoArquivo(IEnumerable<string> textos, CancellationToken stoppingToken)
        {
            //diretório ubuntu
            const string diretorio = "/home/ubuntu/Documentos/FileTeste";

            Console.WriteLine($"Tarefa 01");
            await File.AppendAllLinesAsync($"{diretorio}/texto01.txt", textos, stoppingToken);

            Console.WriteLine($"Tarefa 02");
            await File.AppendAllLinesAsync($"{diretorio}/texto02.txt", textos, stoppingToken);
        }

        private async Task UseDataBase()
        {
            var resultado1 = await _base1Integracao.ObterTodosOsSalariosAsync();
            Console.WriteLine($"Tarefa 01");

            var resultado2 = await _base1Integracao.ObterTodosOsSalariosAsync();
            Console.WriteLine($"Tarefa 02");

            var resultado3 = await _base1Integracao.ObterTodosOsSalariosAsync();
            Console.WriteLine($"Tarefa 03");

            var resultado4 = await _base1Integracao.ObterTodosOsSalariosAsync();
            Console.WriteLine($"Tarefa 04");

            var resultado5 = await _base1Integracao.ObterTodosOsSalariosAsync();
            Console.WriteLine($"Tarefa 05");

            var resultado = resultado1.ToList();
            resultado.AddRange(resultado2);
            resultado.AddRange(resultado3);
            resultado.AddRange(resultado4);
            resultado.AddRange(resultado5);

            await _baseFinal.InserirTodosOsSalariosAsync(resultado);
            Console.WriteLine($"TAREFA FINAL");
        }
    }
}
