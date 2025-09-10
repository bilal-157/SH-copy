using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class City
    {

        /// <summary>
        /// Subversion testing ...!
        /// </summary>
        public int CityId { get; set; }
        public string CityName { get; set; }
        public Country CntId{get; set;}
        public City(int id, string name)
        { 
            this.CityId = id; 
            this.CityName = name;
        }
        public City() {
            this.CntId = new Country();
        }
    }
}
