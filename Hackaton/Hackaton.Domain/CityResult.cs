using System;
using System.Collections.Generic;

namespace Hackaton.Domain
{
    public class CityResult
    {
        public string City { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<String> Attendants { get; set; }
        public int Total { get { return this.Attendants?.Count ?? 0; } }
    }
}
