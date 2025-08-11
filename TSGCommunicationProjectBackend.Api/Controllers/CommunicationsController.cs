using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSGCommunicationProjectBackend.Common.Dtos;
using TSGCommunicationProjectBackend.Common.Dtos.Requests;
using TSGCommunicationProjectBackend.Common.Interfaces;


namespace TSGCommunicationProjectBackend.Api.Controllers
{
    [Route("api/communications")]
    [ApiController]
    public class CommunicationsController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;
        private readonly ILogger<CommunicationsController> _logger;

        public CommunicationsController(
            ICommunicationService communicationService,
            ILogger<CommunicationsController> logger
        )
        {
            _communicationService = communicationService;
            _logger = logger;
        }

        [HttpGet("/all")]
        public async Task<ActionResult<IEnumerable<CommunicationDto>>> GetAllCommunications()
        {
            try
            {
                _logger.LogInformation("Getting all communications");
                var communications = await _communicationService.GetAllCommunicationsAsync();
                if (communications == null)
                {
                    _logger.LogError("Communications not found");
                    return NotFound();
                }
                return Ok(communications);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting communications");
                return StatusCode(500, new { message = $"Error getting communications" });
            }
        }

        [HttpGet("{communicationId:Guid}")]
        public async Task<ActionResult<CommunicationDto>> GetCommunicationById(Guid communicationId)
        {
            try
            {
                _logger.LogInformation("Getting communication {communicationId}", communicationId);
                var communication = await _communicationService.GetCommunicationByIdAsync(communicationId);
                if (communication == null)
                {
                    _logger.LogInformation("Communication {communicationId} not found", communicationId);
                    return NotFound();
                }
                return Ok(communication);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting communication {communicationId}", communicationId);
                return StatusCode(500, new { message = $"Error getting communication" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CommunicationDto>> CreateCommunication([FromBody] CreateCommunicationRequest request)
        {
            try
            {
                _logger.LogInformation("Creating new communication");
                var communication = await _communicationService.CreateCommunicationAsync(request);
                _logger.LogInformation("Created communication {communication.communicationId}", communication.Id);
                return CreatedAtAction(nameof(GetCommunicationById), new { communicationId = communication.Id }, communication);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating communication");
                return StatusCode(500, new { message = "Error creating communication" });
            }
        }

        [HttpPut("{communicationId}")]
        public async Task<ActionResult<CommunicationDto>> UpdateCommunication(Guid communicationId, [FromBody] UpdateCommunicationRequest request)
        {
            try
            {
                _logger.LogInformation("Updating communication {communicationId}", communicationId);
                var result = await _communicationService.UpdateCommunicationAsync(communicationId, request);
                if (result == null)
                {
                    _logger.LogInformation("Communication {communicationId} not found", communicationId);
                    return NotFound();
                }
                _logger.LogInformation("Updated communication {communicationId}", communicationId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating communication {communicationId}", communicationId);
                return StatusCode(500, new { message = "Error updating communication" });
            }
        }

        [HttpDelete("{communicationId}")]
        public async Task<IActionResult> DeleteCommunication(Guid communicationId)
        {
            try
            {
                _logger.LogInformation("Deleting communication {communicationId}", communicationId);
                var result = await _communicationService.DeleteCommunicationAsync(communicationId);
                if (result == null)
                {
                    _logger.LogInformation("Communication {communicationId} not found", communicationId);
                    return NotFound();
                }
                _logger.LogInformation("Deleted communication {communicationId}", communicationId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deleting communication {communicationId}", communicationId);
                return StatusCode(500, new { message = "Error deleting communication" });
            }
        }

        //get one communication's history by ID
        [HttpGet("{communicationId}/history")]
        public async Task<ActionResult<IEnumerable<CommunicationStatusHistoryItemDto>>> GetCommunicationHistory(Guid communicationId)
        {
            try
            {
                _logger.LogInformation("Getting history for communication {communicationId}", communicationId);
                var communication = await _communicationService.GetCommunicationByIdAsync(communicationId);
                if (communication == null)
                {
                    _logger.LogInformation("Tried to get status for not found communication {communicationId}", communicationId);
                    return NotFound();
                }
                var communicationHistory = await _communicationService.GetCommunicationStatusHistoryItems(communicationId);
                //if communication exists, it will always have at least one status history (created status)
                _logger.LogInformation("got history for communication {communicationId}", communicationId);
                return Ok(communicationHistory);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting history for communication {communicationId}", communicationId);
                return StatusCode(500, new { message = "Error getting communication history" });
            }
        }
    }
}
