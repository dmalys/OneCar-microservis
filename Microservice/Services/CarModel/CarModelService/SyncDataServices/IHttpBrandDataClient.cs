using CarModelService.SyncDataServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarModelService.SyncDataServices
{
    public interface IHttpBrandDataClient
    {
        Task SendCheckForAddCarModel(SendAddCarModelRequest request);
    }
}
