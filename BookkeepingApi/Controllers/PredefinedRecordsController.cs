using BookkeepingApi.Models;
using Microsoft.AspNetCore.Mvc;
using BookkeepingApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookkeepingApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrgin")]
    [ApiController]
    
    public class PredefinedRecordsController : ControllerBase
    {
        private readonly IPredefinedRecordsService _predefinedRecordsService;

        public PredefinedRecordsController(PredefinedRecordsService PredefinedRecordsService)
        {
            _predefinedRecordsService = PredefinedRecordsService;
        }

        [Route("AllRecords")]
        [HttpGet]
        public async Task<ActionResult> GetPredefinedRecords(string year)
        {
            List<PredefinedRecords> list;
            list = await _predefinedRecordsService.GetAllPredefinedRecordsByYear(year);
            if (list == null) return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "UnableToLoad", SecurityPermissions = list });
            return StatusCode(StatusCodes.Status200OK, new { status = true, message = "LoadedSuccessfully", BookRecordList = list });
        }

        [Route("UpdateRecords")]
        [HttpPatch]
        public async Task<ActionResult> UpdatePredefinedRecords([FromForm] PredefinedRecords model)
        {
            Response IsUpdated = await _predefinedRecordsService.UpdatePredefinedRecords(model);
            return StatusCode(IsUpdated.StatusCode, new { status = IsUpdated.Status, message = IsUpdated.Message });
        }
    }
}
