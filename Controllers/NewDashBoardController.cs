using Microsoft.AspNetCore.Mvc;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.DataLogics;
using System.Web.Http.Description;
using TMT_Code_Migration1.Models.New_Dashboard;
using TMT_Code_Migration1.Views.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace TMT_Code_Migration1.Controllers
{
    [Route("TMTService")]
    public class NewDashBoardController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly NewDashBoardDL _newDashBoardDL;
        private readonly IConfiguration _configuration;

        public NewDashBoardController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _newDashBoardDL = new NewDashBoardDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GetAssignements")]
        [HttpPost]
        [Authorize]
        public IActionResult GetAssignements(Assignement_Request request)
        {
            List<Assignement_Response>  responses = _newDashBoardDL.GetAssignements(request);
            return Ok(responses);
        }

        [Route("GetProjects")]
        [HttpPost]
        [Authorize]
        public IActionResult GetProjects(Projects_Request request)
        {
            List<Projects_Response> responses = _newDashBoardDL.GetProjects(request);
            return Ok(responses);
        }

        [Route("GetWeek")]
        [HttpPost]
        [Authorize]
        public IActionResult GetWeek(Projects_Request request)
        {
            List<WeekCount_Response> responses = _newDashBoardDL.GetWeek();
            return Ok(responses);
        }

        [Route("GetNewDashboardData")]
        [HttpPost]
        public async Task<IActionResult> GetNewDashboardData(NewDashboard_request request)
        {
            var respo = await _newDashBoardDL.GetNewDashboard_Data(request);
            return Ok(respo);
        }


        [Route("GetDashboardStatus")]
        [HttpPost]
        public IActionResult GetDashboardStatus(Status_Request request)
        {
            var respo = _newDashBoardDL.GetStatus(request);
            return Ok(respo);
        }

        [Route("GetNewDashboardUserData")]
        [HttpPost]
        public IActionResult GetNewDashboardUserData(NewDashboard_User_Request request)
        {
            var respo = _newDashBoardDL.GetNewDashboard_Data_User(request);
            return Ok(respo);
        }

        [Route("GetNewDashboardStatusData")]
        [HttpPost]
        public IActionResult GetNewDashboardStatusData(NewDashboard_Status_Request request)
        {
            var respo = _newDashBoardDL.GetNewDashboard_Data_Status(request);
            return Ok(respo);
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>


        [Route("GET_DS_PROD_SUPPORT_QA")]
        [HttpPost]
        [Authorize]
        public IActionResult GET_DS_PROD_SUPPORT_QA(DS_REQUEST request)
        {
            var respo = _newDashBoardDL.GET_Dashboard_all_tbl_data(request, "SP_DS_PROD_SUPPORT_QA");
            return Ok(respo);
        }

        [Route("GET_DS_PROD_SUPPORT_NONQA")]
        [HttpPost]
        [Authorize]
        public IActionResult GET_DS_PROD_SUPPORT_NONQA(DS_REQUEST request)
        {
            var respo = _newDashBoardDL.GET_Dashboard_all_tbl_data(request, "SP_DS_PROD_SUPPORT_NONQA");
            return Ok(respo);
        }
        [Route("GET_DS_PROJECT_DETAILS")]
        [HttpPost]
        [Authorize]
        public IActionResult GET_DS_PROJECT_DETAILS(DS_REQUEST request)
        {
            var respo = _newDashBoardDL.GET_Dashboard_all_tbl_data(request, "SP_DS_PROJECT_DETAILS");
            return Ok(respo);
        }

        [Route("GET_DS_PROJECT_ISSULIST")]
        [HttpPost]
        [Authorize]
        public IActionResult GET_DS_PROJECT_ISSULIST(DS_REQUEST request)
        {
            var respo = _newDashBoardDL.GET_Dashboard_all_tbl_data(request, "SP_DS_PROJECT_ROOTCAUSE");
            return Ok(respo);
        }



        [Route("GET_DS_TEST_SCRIPTING")]
        [HttpPost]
        [Authorize]
        public IActionResult GET_DS_TEST_SCRIPTING(DS_REQUEST request)
        {
            var respo = _newDashBoardDL.GET_Dashboard_all_tbl_data(request, "SP_DS_TEST_SCRIPTING");
            return Ok(respo);
        }


        [Route("GET_DS_TEST_EXECUTION")]
        [HttpPost]
        [Authorize]
        public IActionResult GET_DS_TEST_EXECUTION(DS_REQUEST request)
        {
            var respo = _newDashBoardDL.GET_Dashboard_all_tbl_data(request, "SP_DS_TEST_EXECUTION");
            return Ok(respo);
        }

        [Route("GET_DS_TEST_EXECUTION_DEFFECTS")]
        [HttpPost]
        [Authorize]
        public IActionResult GET_DS_TEST_EXECUTION_DEFFECTS(DS_REQUEST request)
        {
            var respo = _newDashBoardDL.GET_Dashboard_all_tbl_data(request, "SP_DS_TEST_EXECUTION_DEFFECTS");
            return Ok(respo);
        }


        [Route("GET_DS_GRAPH")]
        [HttpPost]
        [Authorize]
        public IActionResult GET_DS_GRAPH(DS_REQUEST request)
        {
            var respo = _newDashBoardDL.GET_Dashboard_all_tbl_data(request, "SP_PRODUCTIVITY_COEFFICIENT_DATE");
            return Ok(respo);
        }

        [Route("GetCommentAccess/{userId}")]
        [HttpGet]
        [Authorize]

        public IActionResult GetCommentAccess(string userId)
        {
            var respo = _newDashBoardDL.GetAccessCodeForComment(userId);
            return Ok(respo);
        }

        [Route("GetCommentByEntryDate/")]
        [HttpPost]
        [Authorize]

        public IActionResult GetDashboardComment(DashboardCommentRequest rqst)
        {
            var respo = _newDashBoardDL.GetDashboardComment(rqst);
            return Ok(respo);
        }
        [Route("PostDashboardComment/")]
        [HttpPost]
        [Authorize]

        public IActionResult InsertUpdateDashboardComment(DashboardCommentInsUpdateRequest rqst)
        {
            var respo = _newDashBoardDL.InsertUpdateDashboardComment(rqst);
            return Ok(respo);
        }
        [Route("GetAllDashboardComments/")]
        [HttpGet]
        [Authorize]

        public IActionResult GetAllDashboardComments()
        {
            var respo = _newDashBoardDL.GetAllDashboardComments();
            return Ok(respo);
        }
        [Route("DeleteDashboardComment/")]
        [HttpPost]
        [Authorize]

        public IActionResult DeleteDashboardComment(DashboardCommentRequest rqst)
        {
            var respo = _newDashBoardDL.DeleteDashboardComment(rqst);
            return Ok(respo);
        }
    }
}
