using System;
using System.Collections.Generic;

namespace Hackaton.Domain
{
    public class Attende
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public List<DateTime> Dates { get; set; }
    }
}
