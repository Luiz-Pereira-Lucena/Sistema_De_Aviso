using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                args = new string[3];
                Console.WriteLine("Qual é o código do Ativo a ser monitorado ?");
                args[0] = Console.ReadLine();

                while(args[0] != "PETR4 22.59 22.67")
                {
                    Console.WriteLine("O codigo informado anteriormente está errado. Tente novamente por favor:");
                    Console.WriteLine("Qual é o código do Ativo a ser monitorado ?");
                    args[0] = Console.ReadLine();
                }

                Monitoramento cotacao = new Monitoramento();
                while (true)
                {
                    cotacao.Pesquisar();
                }
            }
            else
            {
                Console.WriteLine("Algo deu errado.");
            }
        }
    }
}
