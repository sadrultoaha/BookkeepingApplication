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
    
    public class RecordTypesController : ControllerBase
    {
        private readonly IRecordTypesService _recordTypesService;

        public RecordTypesController(IRecordTypesService recordTypesService)
        {
            _recordTypesService = recordTypesService;
        }

        [Route("All")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            List<RecordTypes> list = await _recordTypesService.GetAllRecordTypes();

            if (list == null) return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "Unable To Load", list = list });
            return StatusCode(StatusCodes.Status200OK, new { status = true, message = "Loaded Successfully", list = list });
        }

        [Route("GetById")]
        [HttpGet]
        public async Task<ActionResult> GetById([FromQuery] int id)
        {
            RecordTypes model = await _recordTypesService.GetRecordTypeById(id);

            if (model == null) return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "Unable To Load", model = model });
            return StatusCode(StatusCodes.Status200OK, new { status = true, message = "Loaded Successfully", model = model });
        }

        [Route("GetActionWiseTypes")]
        [HttpGet]
        public async Task<ActionResult> GetTypes()
        {
            List<RecordTypes> list = await _recordTypesService.GetActionWiseTypes();

            if (list == null) return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "Unable To Load", list = list });
            return StatusCode(StatusCodes.Status200OK, new { status = true, message = "Loaded Successfully", list = list });
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] RecordTypes model)
        {
            Response IsCreated = await _recordTypesService.CreateRecordType(model);
            return StatusCode(IsCreated.StatusCode, new { status = IsCreated.Status, message = IsCreated.Message });
        }

        [Route("Update")]
        [HttpPatch]
        public async Task<ActionResult> Update([FromBody] RecordTypes model)
        {
            Response IsUpdated = await _recordTypesService.UpdateRecordType(model);
            return StatusCode(IsUpdated.StatusCode, new { status = IsUpdated.Status, message = IsUpdated.Message });
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            Response IsDeleted = await _recordTypesService.DeleteRecordType(id);
            return StatusCode(IsDeleted.StatusCode, new { status = IsDeleted.Status, message = IsDeleted.Message });
        }
    }
}
