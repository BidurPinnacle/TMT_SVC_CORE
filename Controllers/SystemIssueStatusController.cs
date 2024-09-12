using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics.Masters;
using TMT_Code_Migration1.Models.Masters.System_Issue;
using TMT_Code_Migration1.DataLogics.Utility;
using Microsoft.AspNetCore.Authorization;

namespace TMT_Code_Migration1.Controllers
{
    [Route("Master/SystemIssueStatus")]
    public class SystemIssueStatusController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly SystemIssueStatusDL _systemIssueStatusDL;
        private readonly IConfiguration _configuration;

        public SystemIssueStatusController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _systemIssueStatusDL = new SystemIssueStatusDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GetSystemIssueStatusData")]
        [HttpGet]
        [Authorize]
        public IActionResult GetSystemIssueStatusData()
        {
            List<SystemIssueStatusData> systemIssueStatusDatas = _systemIssueStatusDL.GetSystemIssueStatusData();
            return Ok(systemIssueStatusDatas);
        }
        [Route("GetSystemIssueStatusDataById/{statusId}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetSystemIssueStatusDataById(int statusId)
        {
            List<SystemIssueStatusData> systemIssueStatusDatas = _systemIssueStatusDL.GetSystemIssueStatusDataById(statusId);
            return Ok(systemIssueStatusDatas);
        }
        [Route("insertUpdateSystemIssueStatus/")]
        [HttpPost]
        [Authorize]
        public IActionResult insertUpdateSystemIssueStatus(InsUpdateSystemIssueStatusRequest rqst)
        {
            InsertUpdateSystemIssueStatusData insertUpdateSystemIssueStatusData = _systemIssueStatusDL.insertUpdateSystemIssueStatus(rqst);
            return Ok(insertUpdateSystemIssueStatusData);
        }
    }
}
