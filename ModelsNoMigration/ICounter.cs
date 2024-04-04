using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.ModelsNoMigration
{
    public interface ICounter
    {
        void IncCounter();
        int GetCounter();
    }
}
