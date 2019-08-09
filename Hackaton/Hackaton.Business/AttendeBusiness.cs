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
        private readonly DateTime _startDate = new DateTime(2019, 7, 2);
        private readonly DateTime _endDate = new DateTime(2019, 9, 9);


        public HashSet<CityResult> ProcessAttendants(HashSet<Attende> attendes)
        {
            //the date provided by the attende is in the valid range. otherwise, ignore
            Parallel.ForEach(attendes, (attende) => {
                attende.Dates.RemoveAll(t => !(t.CompareTo(_startDate) >= 0 && t.CompareTo(_endDate) <= 0));
            });
            var result = new HashSet<CityResult>();
            var grouped = attendes.GroupBy(t => t.City);


            Action<IGrouping<string, Attende>> _groupedFunction = (city) =>
            {
                var cityResult = new HashSet<CityResult>();

                var dates = city.SelectMany(t => t.Dates).Distinct().ToHashSet();
                foreach (var date in dates)
                {
                    //I need to assess every possible day.
                    var temp = new CityResult()
                    {
                        City = city.Key,
                        StartDate = date,
                        EndDate = date.AddDays(1),
                        Attendants = new List<string>()
                    };

                    // the possible attendants for every consecutive day
                    temp.Attendants.AddRange(
                        city.Where(t => t.Dates.Contains(temp.StartDate) || t.Dates.Contains(temp.EndDate))
                        .Select(t => t.Email));
                    //if there are attendants that can come to BOTH days, they should count twice in the final count.
                    temp.Score = temp.Attendants.Count + city.Where(t => t.Dates.Contains(temp.StartDate) && t.Dates.Contains(temp.EndDate)).Count();

                    cityResult.Add(temp);
                }

                result.Add(cityResult.OrderByDescending(t => t.Attendants.Count).ThenByDescending(t=>t.Score).FirstOrDefault());
            };

            Parallel.ForEach(grouped, _groupedFunction);

            return result.OrderByDescending(t => t.Attendants.Count).ThenByDescending(t=>t.Score).Take(5).ToHashSet();
        }
    }
}
