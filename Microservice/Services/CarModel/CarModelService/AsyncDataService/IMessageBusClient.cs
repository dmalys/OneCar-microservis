using CarModelService.AsyncDataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarModelService.AsyncDataService
{
    public interface IMessageBusClient
    {
        Task PublishCheckBrandExists(BrandPublishDTO brandPublishDTO);
    }
}
