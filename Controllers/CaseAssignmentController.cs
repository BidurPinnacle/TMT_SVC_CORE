using Microsoft.AspNetCore.Mvc;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.DataLogics;
using System.Web.Http.Description;
using TMT_Code_Migration1.Models.caseAssignment;
using Microsoft.AspNetCore.Authorization;

namespace TMT_Code_Migration1.Controllers
{
    [Route("userAssign")]
    public class CaseAssignmentController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly CaseAssignmentDL _caseAssignmentDL;
        private readonly IConfiguration _configuration;

        public CaseAssignmentController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _caseAssignmentDL = new CaseAssignmentDL(commonDl, configuration);
            _configuration = configuration;
        }

        [Route("getAllExcelData")]
        [HttpPost]
        [Authorize]
        public IActionResult GetExcelUploadedData(ExcelUploadedDataRequest rqst)
        {
            List<ExcelUploadedDataResponse> excelUploadedDataResponses = _caseAssignmentDL.GetExcelUploadedData(rqst);
            return Ok(excelUploadedDataResponses);
        }

        [Route("getAllTaskTeamData")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllTaskTeamUsers()
        {
            List<TaskTeamUserResponse> taskTeamUserResponses = _caseAssignmentDL.GetAllTaskTeamUsers();
            return Ok(taskTeamUserResponses);
        }

        [Route("reassignCase")]
        [HttpPost]
        [Authorize]
        public IActionResult ReassignUser(ReassignUserRequest rqst)
        {
            string result = _caseAssignmentDL.ReassignUser(rqst);
            return Ok(result);
        }
    }
}
