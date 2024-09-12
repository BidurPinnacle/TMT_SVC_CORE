using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics.Masters;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.System_Issue;

namespace TMT_Code_Migration1.Controllers
{
    [Route("Master/SystemIssueList")]
    public class SysemIssueListController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly SystemIssueListDL _systemIssueListDL;
        private readonly IConfiguration _configuration;

        public SysemIssueListController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _systemIssueListDL = new SystemIssueListDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GetIssueListDatas")]
        [HttpGet]
        [Authorize]
        public IActionResult GetIssueListDatas()
        {
            List<SystemIssueListData> systemIssueListDatas = _systemIssueListDL.GetIssueListDatas();
            return Ok(systemIssueListDatas);
        }
        [Route("GetIssueListDatasById/{sid}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetIssueListDatasById(int sid)
        {
            List<SystemIssueListData> systemIssueListDatas = _systemIssueListDL.GetIssueListDatasById(sid);
            return Ok(systemIssueListDatas);
        }
        [Route("insertUpdateSystemIssue/")]
        [HttpPost]
        [Authorize]
        public IActionResult insertUpdateSystemIssue(InsUpdateSystemIssueRequest rqst)
        {
            InsertUpdateSystemIssueListData insertUpdateSystemIssueListData = _systemIssueListDL.insertUpdateSystemIssue(rqst);
            return Ok(insertUpdateSystemIssueListData);
        }
    }
}
