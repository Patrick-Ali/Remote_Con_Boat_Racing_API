using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remote_Control_Boat_Racing_API.Models
{
    public class EventIn
    {

        public string Id;


        public string VideoURL { get; set; }


        public byte[] EventFile { get; set; }


        public string Name { get; set; }


        public string Date { get; set; }


        public string Location { get; set; }


        public string TimeStart { get; set; }


        public string TimeEnd { get; set; }
    }
}
