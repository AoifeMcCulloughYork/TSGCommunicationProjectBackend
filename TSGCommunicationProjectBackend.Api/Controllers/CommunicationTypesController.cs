using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSGCommunicationProjectBackend.Common.Dtos;
using TSGCommunicationProjectBackend.Common.Dtos.Requests;
using TSGCommunicationProjectBackend.Common.Interfaces;

namespace TSGCommunicationProjectBackend.Api.Controllers
{
    [Route("api/communicationtypes")]
    [ApiController]
    public class CommunicationTypesController : ControllerBase
    {
        private readonly ICommunicationTypesService _communicationTypeService;
        private readonly ILogger<CommunicationTypesController> _logger;

        //construct a CommunicationTypesController based on the given typeservice and logger
        public CommunicationTypesController(
            ICommunicationTypesService communicationTypeService,
            ILogger<CommunicationTypesController> logger
        )
        {
            _communicationTypeService = communicationTypeService;
            _logger = logger;
        }
        //get all types
        //returns a Task (similar to js Promise) that will eventually have
        //either a status code or a iterable/sortable collection of communicationtypeDtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommunicationTypeDto>>> GetAllTypes()
        {
            try
            {
                _logger.LogInformation("Getting all communication types");
                var types = await _communicationTypeService.GetAllTypesAsync();
                return Ok(types);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting communication types");
                return StatusCode(500, new { message = "Error getting communication types" });
            }
        }

        //get type by ID
        //corresponds to a TypeCode int in DB
        [HttpGet("{typeCode}")]
        public async Task<ActionResult<CommunicationTypeDto>> GetTypeById(int typeCode)
        {
            try
            {
                _logger.LogInformation("Getting communication type {typeCode}", typeCode);
                var type = await _communicationTypeService.GetTypeByIdAsync(typeCode);
                if (type == null)
                {
                    _logger.LogInformation("Communication {typeCode} not found", typeCode);
                    return NotFound();
                }
                return Ok(type);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting communication type {typeCode}", typeCode);
                return StatusCode(500, new { message = $"Error getting communication type {typeCode}" });
            }
        }

        //Create communication type (Admin only)
        //returns the created type
        [HttpPost]
        public async Task<ActionResult<CommunicationTypeDto>> CreateType([FromBody] CreateCommunicationTypeRequest request)
        {
            try
            {
                _logger.LogInformation("Admin creating new communication type");
                var type = await _communicationTypeService.CreateTypeAsync(request);
                _logger.LogInformation("Admin created new type {type}", type);
                return type;
            }
            //catch (InvalidOperationException ioe)
            //{
            //    _logger.LogError(ioe, "Error creating communication type");
            //    return StatusCode(500, new { message = "Internal error creating communication type" });
            //}
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating communication type");
                return StatusCode(500, new { message = "Error creating communication type" });
            }
        }

        //Delete communication type (Admin only)
        [HttpDelete("{typeCode:int}")]
        public async Task<IActionResult> DeleteType([FromBody] DeleteCommunicationTypeRequest request)
        {
            try
            {
                var type = await _communicationTypeService.GetTypeByIdAsync(request.TypeCode);
                if (type == null)
                {
                    _logger.LogInformation("Communication for deletion {typeCode} not found", request.TypeCode);
                    return NotFound();
                }
                var success = await _communicationTypeService.DeleteTypeAsync(request);
                return Ok(success);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deleting communication type {typeCode}", request.TypeCode);
                return StatusCode(500, new { message = $"Error deleting communication type {request.TypeCode}. Type was not deleted!" });
            }
        }

        //Update communication type (Admin only)
        //Returns updated type
        [HttpPut("{typeCode}")]
        public async Task<ActionResult<CommunicationTypeDto>> UpdateType(int typeCode, [FromBody] UpdateCommunicationTypeRequest request)
        {
            try
            {
                _logger.LogInformation("Admin updating type {typeCode}", typeCode);
                if (_communicationTypeService.GetTypeByIdAsync(typeCode) == null)
                {
                    _logger.LogInformation("Communication for updating {typeCode} not found", typeCode);
                    return NotFound();
                }
                var type = await _communicationTypeService.UpdateTypeAsync(typeCode, request);
                return Ok(type);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating type {typeCode}", typeCode);
                return StatusCode(500, new { message = $"Error updating Communication type {typeCode}" });
            }
        }
    }
}
