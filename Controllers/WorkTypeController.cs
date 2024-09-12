using Microsoft.AspNetCore.Mvc;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.DataLogics.Masters;
using System.Web.Http.Description;
using TMT_Code_Migration1.Models.Masters.Work_Type;
using Microsoft.AspNetCore.Authorization;

namespace TMT_Code_Migration1.Controllers
{
    [Route("Master/WorkType")]
    public class WorkTypeController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly WorkTypeDL _workTypeDL;
        private readonly IConfiguration _configuration;

        public WorkTypeController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _workTypeDL = new WorkTypeDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GetWorkTypeData/{taskId}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetWorkTypeData(int taskId)
        {
            List<WorkTypeData> workTypeDatas = _workTypeDL.GetWorkTypeData(taskId);
            return Ok(workTypeDatas);
        }
        [Route("GetWorkTypeById/{wvalue}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetWorkTypeDataById(int wvalue)
        {
            List<WorkTypeData> workTypeDatas = _workTypeDL.GetWorkTypeDataById(wvalue);
            return Ok(workTypeDatas);
        }
        [Route("insertUpdateWork/")]
        [HttpPost]
        [Authorize]
        public IActionResult insertUpdateWork(InsUpdateWorkRequest rqst)
        {
            InsertUpdateWorkTypeData insUpdateResponse = _workTypeDL.insertUpdateWork(rqst);
            return Ok(insUpdateResponse);
        }
    }
}
