using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class WorkerLineItem
    {

        private WorkerDealing workerdealing;

        public WorkerDealing WorkerDealing
        {
            get { return this.workerdealing; }
            set { this.workerdealing = value; }
        }

       


        public WorkerLineItem() { }


      
    }
}