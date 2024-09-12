using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.Models.Common;

namespace TMT_Code_Migration1.Controllers
{
    [Route("TMTService")]
    public class DashboardController : Controller
    {
        private readonly DashBoardDL _dashBoardDL;
        private readonly IConfiguration _config;

        public DashboardController(DashBoardDL dashBoardDL, [FromServices] IConfiguration configuration)
        {
            _dashBoardDL = dashBoardDL;
            _config = configuration;
        }
        [Route("Dashboard_Data")]
        [HttpPost]
        [Authorize]
        public IActionResult Dashboard_Data(DashboardRequest request)
        {
            DashboardResponse_All responses = _dashBoardDL.Dashboard_Data(request);
            return Ok(responses);
        }
        [Route("Dashboard_Batch_Data")]
        [HttpPost]
        [Authorize]
        public IActionResult Dashboard_Batch_Data(DashboardBatchReports_Data_Request _Data_Request)
        {
            List<DashboardBatchReports_Data_respose> responses = _dashBoardDL.Dashboard_Batch_Data(_Data_Request);
            return Ok(responses);
        }

        [Route("Dashboard_Issue_Data")]
        [HttpPost]
        [Authorize]
        public IActionResult Dashboard_Issue_Data(DashboardIssueReports_Data_Request _Data_Request)
        {
            List<DashboardIssueReports_Data> responses = _dashBoardDL.Dashboard_Issue_Data(_Data_Request);
            return Ok(responses);
        }


        [Route("Dashboard_progressive_data")]
        [HttpPost]
        [Authorize]
        public IActionResult DASHBOARD_PROGRESSIVE_CHART_DATA(DashboardRequest_progressive request)
        {
            List<Dashboardresponse_Progressive> dashboardResponse_ = _dashBoardDL.DASHBOARD_PROGRESSIVE_CHART_DATA(request);
            return Ok(dashboardResponse_);
        }

        [Route("LAST4WEEKDATA")]
        [HttpPost]
        [Authorize]
        public IActionResult LAST4WEEKDATA(DashboardRequest_progressive request)
        {
            List<Dashboardresponse_Weekcount> dashboardResponse_ = _dashBoardDL.LAST4WEEKDATA(request);
            return Ok(dashboardResponse_);
        }



        //////////////////////////////////////////////////////////////
        ///





        [Route("Dashboard_Data_hist")]
        [HttpPost]
        [Authorize]
        public IActionResult Dashboard_Data_Hist(DashboardRequest request)
        {
            DashboardResponse_All responses = _dashBoardDL.Dashboard_Data_Hist(request);
            return Ok(responses);
        }
        [Route("Dashboard_Batch_Data_hist")]
        [HttpPost]
        [Authorize]
        public IActionResult Dashboard_Batch_Data_Hist(DashboardBatchReports_Data_Request _Data_Request)
        {
            List<DashboardBatchReports_Data_respose> responses = _dashBoardDL.Dashboard_Batch_Data_Hist(_Data_Request);
            return Ok(responses);
        }

        [Route("Dashboard_Issue_Data_hist")]
        [HttpPost]
        [Authorize]
        public IActionResult Dashboard_Issue_Data_Hist(DashboardIssueReports_Data_Request _Data_Request)
        {
            List<DashboardIssueReports_Data> responses = _dashBoardDL.Dashboard_Issue_Data_HIST(_Data_Request);
            return Ok(responses);
        }


        [Route("Dashboard_progressive_data_Hist")]
        [HttpPost]
        [Authorize]
        public IActionResult DASHBOARD_PROGRESSIVE_CHART_DATA_hist(DashboardRequest_progressive request)
        {
            List<Dashboardresponse_Progressive> dashboardResponse_ = _dashBoardDL.DASHBOARD_PROGRESSIVE_CHART_DATA_HIST(request);
            return Ok(dashboardResponse_);
        }

        [Route("LAST4WEEKDATA_Hist")]
        [HttpPost]
        [Authorize]
        public IActionResult LAST4WEEKDATA_hist(DashboardRequest_progressive request)
        {
            List<Dashboardresponse_Weekcount> dashboardResponse_ = _dashBoardDL.LAST4WEEKDATA_HIST(request);
            return Ok(dashboardResponse_);
        }

        ////////////////////////////////for TMT-------------------------
        ///

        [Route("TASKSUMMARYCOUNT")]
        [HttpGet]
        [Authorize]
        public IActionResult TASKSUMMARYCOUNT()
        {
            TASKSUMMARYCOUNT_RESPONSE dashboardResponse_ = _dashBoardDL.TASKSUMMARYCOUNT();
            return Ok(dashboardResponse_);
        }

        [Route("WORKTYPESUMMARYCOUNT")]
        [HttpPost]
        [Authorize]
        public IActionResult WORKTYPESUMMARYCOUNT(WORKTYPESUMMARYCOUNT_RESQUEST rESQUEST)
        {
            List<WORKTYPESUMMARYCOUNT_RESPONSE> dashboardResponse_ = _dashBoardDL.WORKTYPESUMMARYCOUNT(rESQUEST);
            return Ok(dashboardResponse_);
        }
    }
}
