using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        

        private List<RightsLineItem> lineItems;
        public List<RightsLineItem> RightsLineItem
        {
            get
            {
                return this.lineItems;
            }
        }
        public void AddLineItems(RightsLineItem rli)
        {
            if (this.lineItems == null) lineItems = new List<RightsLineItem>();
            lineItems.Add(rli);
        }
        public void RemoveLineItems(RightsLineItem rli)
        {

            this.lineItems.Remove(rli);
        }
    }
}
