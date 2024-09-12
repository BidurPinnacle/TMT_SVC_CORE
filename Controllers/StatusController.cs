using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics.Masters;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.Status;

namespace TMT_Code_Migration1.Controllers
{
    [Route("Master/status")]
    public class StatusController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly StatusDL _statusDL;
        private readonly IConfiguration _configuration;

        public StatusController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _statusDL = new StatusDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GetStatusData")]
        [HttpGet]
        [Authorize]
        public IActionResult GetStatusData()
        {
            List<StatusData> statusDatas = _statusDL.GetStatusData();
            return Ok(statusDatas);
        }
        [Route("GetStatusDataById/{statusVal}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetStatusDataById(int statusVal)
        {
            List<StatusData> statusDatas = _statusDL.GetStatusDataById(statusVal);
            return Ok(statusDatas);
        }
        [Route("insertUpdateStatus/")]
        [HttpPost]
        [Authorize]
        public IActionResult insertUpdateStatus(InsUpdateStatusRequest rqst)
        {
            InsertUpdateStatusData insertUpdateStatusData = _statusDL.insertUpdateStatus(rqst);
            return Ok(insertUpdateStatusData);
        }
    }
}
