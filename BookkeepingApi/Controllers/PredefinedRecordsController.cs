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
    [EnableCors("AllowOrgin")]
    [ApiController]
    
    public class PredefinedRecordsController : ControllerBase
    {
        private readonly IPredefinedRecordsService _predefinedRecordsService;

        public PredefinedRecordsController(IPredefinedRecordsService PredefinedRecordsService)
        {
            _predefinedRecordsService = PredefinedRecordsService;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            PredefinedIncomeCostDto list = await _predefinedRecordsService.GetAllPredefinedRecordsByYear(2020);

            if (list == null) return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "Unable To Load", PredefinedRecordList = list });
            return StatusCode(StatusCodes.Status200OK, new { status = true, message = "Loaded Successfully", PredefinedRecordList = list });
        }

        [Route("GetById")]
        [HttpGet]
        public async Task<ActionResult> GetById([FromQuery] int id)
        {
            PredefinedRecords model = await _predefinedRecordsService.GetPredefinedRecordById(id);

            if (model == null) return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "Unable To Load", PredefinedRecord = model });
            return StatusCode(StatusCodes.Status200OK, new { status = true, message = "Loaded Successfully", PredefinedRecord = model });
        }

        [Route("CreateRecords")]
        [HttpPatch]
        public async Task<ActionResult> Create([FromForm] PredefinedRecords model)
        {
            Response IsCreated = await _predefinedRecordsService.CreatePredefinedRecord(model);
            return StatusCode(IsCreated.StatusCode, new { status = IsCreated.Status, message = IsCreated.Message });
        }

        [Route("UpdateRecords")]
        [HttpPatch]
        public async Task<ActionResult> Update([FromForm] PredefinedRecords model)
        {
            Response IsUpdated = await _predefinedRecordsService.UpdatePredefinedRecord(model);
            return StatusCode(IsUpdated.StatusCode, new { status = IsUpdated.Status, message = IsUpdated.Message });
        }

        [Route("DeleteRecords")]
        [HttpPatch]
        public async Task<ActionResult> Delete([FromForm] int id)
        {
            Response IsDeleted = await _predefinedRecordsService.DeletePredefinedRecord(id);
            return StatusCode(IsDeleted.StatusCode, new { status = IsDeleted.Status, message = IsDeleted.Message });
        }
    }
}
