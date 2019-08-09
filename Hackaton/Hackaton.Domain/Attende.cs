using System;
using System.Collections.Generic;

namespace Hackaton.Domain
{
    /// <summary>
    /// I am not using the name property because I dont really need it at all to process the attendes and is
    /// not required in the output file
    /// </summary>
    public class Attende
    {
        public string City { get; set; }
        public string Email { get; set; }
        public List<DateTime> Dates { get; set; }
    }
}
