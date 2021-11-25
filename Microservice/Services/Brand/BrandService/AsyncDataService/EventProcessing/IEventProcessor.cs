using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandService.AsyncDataService.EventProcessing
{
    public interface IEventProcessor
    {
        bool IsProcessEventSuccess(string message);
    }
}
