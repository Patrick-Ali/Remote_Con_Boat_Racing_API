using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remote_Control_Boat_Racing_API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EventIn
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VideoURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] EventFile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TimeStart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TimeEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
}
