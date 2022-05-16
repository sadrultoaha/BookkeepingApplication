using BookkeepingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookkeepingApi.Services;

namespace BookkeepingApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    
    public class ReconciliationRecordsController : ControllerBase
    {
       private readonly IReconciliationRecordsService _reconciliationRecordsService;

        public ReconciliationRecordsController(IReconciliationRecordsService reconciliationRecordsService)
        {
            _reconciliationRecordsService = reconciliationRecordsService;
        }

        [Route("AllByYear")]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] int year)
        {
            List<ReconciliationRecords> list;
            list = await _reconciliationRecordsService.GetAllReconciliationsByYear(year);

            if (list == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "Unable To Load.", SecurityPermissions = list });
            }

            return StatusCode(StatusCodes.Status200OK, new { status = true, message = "Loaded Successfully.", BookRecordList = list });
        }

        [Route("Reconcile")]
        [HttpPatch]
        public async Task<ActionResult> Update([FromForm] ReconciliationRecords model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { status = false, message = "Model NotFound." });
            }
            Response IsReconciled = await _reconciliationRecordsService.Reconcile(model.ReconciliationRecordList);
            return StatusCode(IsReconciled.StatusCode, new { status = IsReconciled.Status, message = IsReconciled.Message });
        }
    }
}
