using Hackaton.Business;
using Hackaton.Domain;
using Hackaton.Persistance;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hackaton
{
    public class Tuple<T1, T2>
    {
        public Tuple(T1 t1, T2 t2) { Item1 = t1; Item2 = t2; }
        public readonly T1 Item1;
        public readonly T2 Item2;
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Tuple<T1, T2>))
                return false;
            var Tobj = (obj as Tuple<T1, T2>);
            return Tobj != null && Tobj.Item1.Equals(Item1) && Tobj.Item2.Equals(Item2);
        }
        public override int GetHashCode()
        {
            return Item1.GetHashCode() * Item2.GetHashCode();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            var attendes = FilePersistance.LoadJson<Attende>("c:/inputs/default.json");

            //var result = new List<CityResult>();
            //var grouped = attendes.GroupBy(t => t.City);

            //Parallel.ForEach(grouped, (city) =>
            //    {
            //        var cityResult = new List<CityResult>();
            //        var dates = city.SelectMany(t => t.Dates).Distinct().ToList();
            //        foreach (var date in dates)
            //        {
            //            var temp = new CityResult()
            //            {
            //                City = city.Key,
            //                StartDate = date,
            //                EndDate = date.AddDays(1),
            //                Attendants = new List<string>()
            //            };
            //            temp.Attendants.AddRange(
            //                city.Where(t => t.Dates.Contains(temp.StartDate) || t.Dates.Contains(temp.EndDate))
            //                .Select(t => t.Email));
            //            cityResult.Add(temp);
            //        }
            //        result.Add(cityResult.OrderByDescending(t => t.Attendants.Count).FirstOrDefault());

            //    }
            //);


            //result = result.OrderByDescending(t => t.Attendants.Count).ToList();

            var business = new AttendeBusiness();
            var result = business.ProcessAttendants(attendes);


            FilePersistance.WriteJsonToFile(result, "c:/inputs/result.json");

            string a = "";

        }

        //private static List<Attende> LoadJson(string path)
        //{
        //    using (StreamReader r = new StreamReader(path))
        //    {
        //        var serializer = new JsonSerializer();

        //        using (var jsonTextReader = new JsonTextReader(r))
        //        {
        //            return serializer.Deserialize<List<Attende>>(jsonTextReader); //34.344 ms
        //        }
        //    }
        //}
    }
}
