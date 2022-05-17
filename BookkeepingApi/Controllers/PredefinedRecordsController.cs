using BookkeepingApi.Models;
using Microsoft.AspNetCore.Mvc;
using BookkeepingApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookkeepingApi.Models.Dtos;

namespace BookkeepingApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    
    public class PredefinedRecordsController : ControllerBase
    {
        private readonly IPredefinedRecordsService _predefinedRecordsService;

        public PredefinedRecordsController(IPredefinedRecordsService predefinedRecordsService)
        {
            _predefinedRecordsService = predefinedRecordsService;
        }

        [Route("AllByYear")]
        [HttpGet]
        public async Task<ActionResult> GetByYear([FromQuery] int year)
        {
            PredefinedIncomeCostDto list = await _predefinedRecordsService.GetAllPredefinedRecordsByYear(year);

            if (list == null) return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "Unable To Load", list = list });
            return StatusCode(StatusCodes.Status200OK, new { status = true, message = "Loaded Successfully", list = list });
        }


        [Route("All")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            List<PredefinedRecords> list = await _predefinedRecordsService.GetAllPredefinedRecords();

            if (list == null) return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "Unable To Load", list = list });
            return StatusCode(StatusCodes.Status200OK, new { status = true, message = "Loaded Successfully", list = list });
        }

        [Route("GetById")]
        [HttpGet]
        public async Task<ActionResult> GetById([FromQuery] int id)
        {
            PredefinedRecords model = await _predefinedRecordsService.GetPredefinedRecordById(id);

            if (model == null) return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "Unable To Load", model = model });
            return StatusCode(StatusCodes.Status200OK, new { status = true, message = "Loaded Successfully", model = model });
        }

        [Route("Create")]
        [HttpPatch]
        public async Task<ActionResult> Create([FromBody] PredefinedRecords model)
        {
            Response IsCreated = await _predefinedRecordsService.CreatePredefinedRecord(model);
            return StatusCode(IsCreated.StatusCode, new { status = IsCreated.Status, message = IsCreated.Message });
        }

        [Route("Update")]
        [HttpPatch]
        public async Task<ActionResult> Update([FromBody] PredefinedRecords model)
        {
            Response IsUpdated = await _predefinedRecordsService.UpdatePredefinedRecord(model);
            return StatusCode(IsUpdated.StatusCode, new { status = IsUpdated.Status, message = IsUpdated.Message });
        }

        [Route("Delete")]
        [HttpPatch]
        public async Task<ActionResult> Delete([FromBody] int id)
        {
            Response IsDeleted = await _predefinedRecordsService.DeletePredefinedRecord(id);
            return StatusCode(IsDeleted.StatusCode, new { status = IsDeleted.Status, message = IsDeleted.Message });
        }
    }
}
