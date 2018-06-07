using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApi.Models
{
    public class SectionItem
    {
        public long Id { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Location { get; set; }

        public string Serialize()
        {
            return "{ \"Section\":\"" + SectionName + "\","
                + "\"info\":{ \"name\":\"" + Name + "\",\"city\":\"" + City + "\",\"location\":\"" + Location + "\"}}";
        }
    }
}