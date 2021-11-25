using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketService.BusinessLayer.Ticket.Interfaces;
using TicketService.BusinessLayer.Ticket.Models;
using Swashbuckle.Swagger.Annotations;

namespace TicketService.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketHandler _ticketHandler;

        public TicketController(ILogger<TicketController> logger,
             ITicketHandler ticketHandler)
        {
            _logger = logger;
            _ticketHandler = ticketHandler;
        }

        [HttpPost]
        [Route("GetTickets")]
        [SwaggerOperation(OperationId = "GetTickets")]
        [ProducesResponseType(typeof(GetTicketsResponse), StatusCodes.Status200OK)]
        public async Task<GetTicketsResponse> GetTickets()
        {
            return await _ticketHandler.GetTickets();
        }

        [HttpPost]
        [Route("GetDetails")]
        [SwaggerOperation(OperationId = "GetDetails")]
        [ProducesResponseType(typeof(GetTicketResponse), StatusCodes.Status200OK)]
        public async Task<GetTicketResponse> GetDetails([FromBody] GetTicketRequest request)
        {
            return await _ticketHandler.GetTicket(request);
        }

        [HttpPost]
        [Route("AddTicket")]
        [SwaggerOperation(OperationId = "AddTicket")]
        public async Task AddCar([FromBody]AddTicketRequest request)
        {
            await _ticketHandler.AddTicket(request);
        }

        [HttpPost]
        [Route("DeleteTicket")]
        [SwaggerOperation(OperationId = "DeleteTicket")]
        public async Task DeleteTicket([FromBody]DeleteTicketRequest request)
        {
            await _ticketHandler.DeleteTicket(request);
        }
    }
}
