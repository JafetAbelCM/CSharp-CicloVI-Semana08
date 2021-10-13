using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Semana08
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();

        static void Main(string[] args)
        {
            IntroToLinq();

            DataSource();

            Filtering();

            Ordering();

            Grouping();

            Grouping2();

            Joining();

            Console.Read();
        }

        static void IntroToLinq()
        {
            Console.WriteLine("IntroToLinq");
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = numbers.Where(n => n % 2 == 0).Select(n => n);

            foreach (int num in numQuery)
            {
                Console.Write($"{num}");
            }
            Console.WriteLine("");
        }

        static void DataSource()
        {
            Console.WriteLine("DataSource");
            var queryAllCustomers = context.clientes.Select(c => c);

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
            Console.WriteLine("");
        }

        static void Filtering()
        {
            Console.WriteLine("Filtering");
            var queryLondonCustomers = context.clientes.Where(c => c.Ciudad == "Londres").Select(c => c);

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
            Console.WriteLine("");
        }

        static void Ordering()
        {
            Console.WriteLine("Ordering");
            var queryLondonCustomers = context.clientes.Where(c => c.Ciudad == "Londres").OrderBy(c => c.NombreCompañia).Select(c => c);

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
            Console.WriteLine("");
        }

        static void Grouping()
        {
            Console.WriteLine("Grouping");
            IQueryable<IGrouping<string, clientes>> queryCustomersByCity = context.clientes.GroupBy(c => c.Ciudad).Select(c => c);

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine($"{customer.NombreCompañia}");
                }
            }
            Console.WriteLine("");
        }

        static void Grouping2()
        {
            Console.WriteLine("Grouping2");
            var custQuery = context.clientes.GroupBy(c => c.Ciudad).Where(c => c.Count() > 2).OrderBy(c => c.Key).Select(c => c);

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
                foreach (clientes customer in item)
                {
                    Console.WriteLine($"{customer.NombreCompañia}");
                }
            }
            Console.WriteLine("");
        }

        static void Joining()
        {
            Console.WriteLine("Joining");
            var innerJoinQuery = context.clientes.Join(
                context.Pedidos, c => c.idCliente, p => p.IdCliente,
                (c, p) => new { CustomerName = c.NombreCompañia, DistributorName = p.PaisDestinatario });

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine($"{item.CustomerName} - {item.DistributorName}");
            }
            Console.WriteLine("");
        }
    }
}