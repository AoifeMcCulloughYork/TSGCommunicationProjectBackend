using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TSGCommunicationProjectBackend.Common.Interfaces;
using TSGCommunicationProjectBackend.Data.Contexts;

namespace TSGCommunicationProjectBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(
        IEventPublisher eventPublisher,
        ILogger<EventsController> logger,
        ApplicationDbContext context) : ControllerBase
    {
        private readonly IEventPublisher _eventPublisher = eventPublisher;
        private readonly ILogger<EventsController> _logger = logger;
        private readonly ApplicationDbContext _context = context;
    }
    //add a post route that validates that the status is applicable to the type
    //then publishes rabbitMQ event
}
