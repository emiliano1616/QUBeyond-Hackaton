using Hackaton.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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

            //Object to be retrieved
            var result = new HashSet<CityResult>();
            var grouped = attendes.GroupBy(t => t.City);

            //I have grouped cities. Every loop contains the info of that city summarized
            Parallel.ForEach(grouped, (city) =>
            {
                var cityResult = new HashSet<CityResult>();

                //Select all the possible dates for that city
                var dates = city.SelectMany(t => t.Dates).Distinct().ToHashSet();
                foreach (var date in dates)
                {
                    //I need to assess every possible day.
                    var record = new CityResult()
                    {
                        City = city.Key,
                        StartDate = date,
                        EndDate = date.AddDays(1),
                        Attendants = new List<string>()
                    };

                    // the possible attendants for every consecutive day
                    record.Attendants.AddRange(
                        city.Where(t => t.Dates.Contains(record.StartDate) || t.Dates.Contains(record.EndDate))
                        .Select(t => t.Email));
                    //if there are attendants that can come to BOTH days, they should count twice in the final count.
                    record.Score = record.Attendants.Count + city.Where(t => t.Dates.Contains(record.StartDate) && t.Dates.Contains(record.EndDate)).Count();

                    //At this point, I have summarized all the possible attendes for a specific city in a specific day
                    cityResult.Add(record);
                }

                //At this point, I have summarized all the possible attendes for a specific city in every possible date.
                //So I will choose the date range with the biggest amount of attendants/score
                //NOTE: I am considering that only one event per city is allowed. If more than one event per city is required, the following line should be removed.
                result.Add(cityResult.OrderByDescending(t => t.Attendants.Count).ThenByDescending(t => t.Score).FirstOrDefault());
            });

            //At this point, I have choosen the best date for the event in every city. Now I need to select the top 5 cities.
            return result.OrderByDescending(t => t.Attendants.Count).ThenByDescending(t=>t.Score).Take(5).ToHashSet();
        }
    }
}
