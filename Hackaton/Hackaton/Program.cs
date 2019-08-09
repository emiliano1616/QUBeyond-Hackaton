using Hackaton.Business;
using Hackaton.Domain;
using Hackaton.Persistance;
using System;
using System.Configuration;

namespace Hackaton
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = args.Length > 0 ? args[0] : ConfigurationManager.AppSettings["DefaultInput"];
            string output = args.Length > 1 ? args[1] : ConfigurationManager.AppSettings["DefaultOutput"];

            if (!FilePersistance.FileExists(input))
            {
                Console.WriteLine($"Cannot find file {input}");
                Environment.Exit(1);
            }
            if (!FilePersistance.CanWrite(output))
            {
                Console.WriteLine($"Cannot write on path {output}");
                Environment.Exit(1);
            }

            var attendes = FilePersistance.LoadJson<Attende>(input);

            var business = new AttendeBusiness();
            var result = business.ProcessAttendants(attendes);

            FilePersistance.WriteJsonToFile(result, output);

        }
    }
}
