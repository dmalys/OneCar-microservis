using BrandService.BusinessLayer.ErrorHandling;
using BrandService.DataAccessLayer.Interfaces;
using CarModelService.AsyncDataService.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrandService.AsyncDataService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EventProcessor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public bool IsProcessEventSuccess(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.CheckBrand:
                    return CheckBrand(message);
                    break;
                case EventType.Undefined:
                    return false;
                    break;
                default:
                    throw new SystemBaseException("EventProcessor ProcessEvent failed.", SystemErrorCode.SystemError);
            }
        }

        private bool CheckBrand(string checkBrandPublishedMessage)
        {
            //TODO: check it better for other way
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IBrandRepository>();

                var brandDTO = JsonSerializer.Deserialize<BrandPublishedDTO>(checkBrandPublishedMessage);

                try
                {
                    var brandExists = repo.CheckBrandExists(brandDTO.BrandId);

                    //TODO:
                    if (brandExists.Result)
                    {
                        Console.WriteLine("CheckBrand - Brand exists");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("CheckBrand - Brand not exists");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new SystemBaseException("EventProcessor CheckBrand failed.", SystemErrorCode.SystemError, ex);
                }
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("Determining event");
            var eventType = JsonSerializer.Deserialize<GenericEventDTO>(notificationMessage);

            switch (eventType.Event)
            {
                case "CheckBrand":
                    Console.WriteLine("check brand - event type detected");
                    return EventType.CheckBrand;
                default:
                    Console.WriteLine("No - event type detected");
                    return EventType.Undefined;
            }
        }
    }

    enum EventType
    {
        CheckBrand,
        Undefined
    }
}
