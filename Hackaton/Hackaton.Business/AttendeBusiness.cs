using Hackaton.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Business
{
    public class AttendeBusiness
    {

        //private Action<IGrouping<string, Attende>> _groupedFunction = (city) =>
        //{
        //    var cityResult = new List<CityResult>();
        //    var dates = city.SelectMany(t => t.Dates).Distinct().ToList();
        //    foreach (var date in dates)
        //    {
        //        var temp = new CityResult()
        //        {
        //            City = city.Key,
        //            StartDate = date,
        //            EndDate = date.AddDays(1),
        //            Attendants = new List<string>()
        //        };
        //        temp.Attendants.AddRange(
        //            city.Where(t => t.Dates.Contains(temp.StartDate) || t.Dates.Contains(temp.EndDate))
        //            .Select(t => t.Email));
        //        cityResult.Add(temp);
        //    }

        //    result.Add(cityResult.OrderByDescending(t => t.Attendants.Count).FirstOrDefault());

        //};

        public List<CityResult> ProcessAttendants(List<Attende> attendes)
        {
            var result = new List<CityResult>();
            var grouped = attendes.GroupBy(t => t.City);


            Action<IGrouping<string, Attende>> _groupedFunction = (city) =>
            {
                var cityResult = new List<CityResult>();
                var dates = city.SelectMany(t => t.Dates).Distinct().ToList();
                foreach (var date in dates)
                {
                    var temp = new CityResult()
                    {
                        City = city.Key,
                        StartDate = date,
                        EndDate = date.AddDays(1),
                        Attendants = new List<string>()
                    };
                    temp.Attendants.AddRange(
                        city.Where(t => t.Dates.Contains(temp.StartDate) || t.Dates.Contains(temp.EndDate))
                        .Select(t => t.Email));
                    cityResult.Add(temp);
                }

                result.Add(cityResult.OrderByDescending(t => t.Attendants.Count).FirstOrDefault());
            };



            //If the list is small, it will perjudicate the performance. with 900 records, parallel reduces the process time to half
            //(actually, that depends on the number of cores you CPU has)
            if (attendes.Count > 500)
            {
                Parallel.ForEach(grouped, _groupedFunction);
            } else
            {
                foreach(var city in grouped)
                {
                    _groupedFunction(city);
                }
            }



            return result.OrderByDescending(t=>t.Attendants.Count).Take(5).ToList();
        }
    }
}
