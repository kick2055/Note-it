using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.ModelsNoMigration
{
    public class Counter : ICounter
    {
        int counter = 0;
        public void IncCounter()
        {
            counter++;
        }
        public int GetCounter()
        {
            return counter;
        }
    }
}
