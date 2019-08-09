using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Hackaton.Domain
{
    [DataContract]
    public class CityResult
    {
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public List<String> Attendants { get; set; }
        [DataMember]
        public int Total { get { return this.Attendants?.Count ?? 0; } }
        /// <summary>
        /// This field mussnt necessary be equals to Total because maybe a attende
        /// can come to both dates and that attendant should have priviledge above another
        /// </summary>
        public int Score { get; set; }
    }
}
