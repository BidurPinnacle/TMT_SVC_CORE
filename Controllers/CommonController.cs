using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Common;

namespace TMT_Code_Migration1.Controllers
{
    [Route("TMTService")]
    public class CommonController : Controller
    {
        private readonly CommonDL _commonDL;
        private readonly IConfiguration _config;

        public CommonController(CommonDL commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDL = commonDl;
            _config = configuration;
        }
        [Route("GetAllTasktype")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllTasktype(bool includeAddress = false)

        {
            List<TaskType> taskTypes = _commonDL.GetTaskTypeDL();
            return Ok(taskTypes);
        }
        [Route("GetAllRootCause")]
        [HttpPost]
        [Authorize]
        public IActionResult GetRootCauseDL(rootCauseRequest rqst)

        {
            List<RootCause> rootCauses = _commonDL.GetRootCauseDL(rqst);
            return Ok(rootCauses);
        }

        [Route("GetAllCaseStatusType/{Assignid}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllCaseStatusType(int Assignid)
        {
            List<CaseStatusType> taskTypes = _commonDL.GetCaseStatusTypeDL(Assignid);
            return Ok(taskTypes);
        }



        [Route("GetTaskTypeDLForExecutionTest")]
        [HttpGet]
        [Authorize]
        public IActionResult GetTaskTypeDLForExecutionTest(bool includeAddress = false)
        {
            List<TaskType> taskTypes = _commonDL.GetTaskTypeDLForExecutionTest();
            return Ok(taskTypes);
        }

        [Route("GetTaskTypeDLForScriptingTest")]
        [HttpGet]
        [Authorize]
        public IActionResult GetTaskTypeDLForScriptingTest(bool includeAddress = false)
        {
            List<TaskType> taskTypes = _commonDL.GetTaskTypeDLForScriptingTest();
            return Ok(taskTypes);
        }

        [Route("GetAllworktype")]
        [HttpPost]
        [Authorize]
        public IActionResult GetAllworktype(WorkTypeRequest typeRequest)
        {
            List<WorkType> worktypelist = _commonDL.GetWorkTypeDL(typeRequest);
            return Ok(worktypelist);
        }

        [Route("getAllWorkYears")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllWorkYears()
        {
            List<workYears> workYearList = _commonDL.GetWorkYearsDL();
            return Ok(workYearList);
        }

        [Route("GetAllCompany")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllCompany(bool includeAddress = false)
        {
            List<Company> companies = _commonDL.GetCompanyDL();
            return Ok(companies);
        }

        [Route("GetDashboard")]
        [HttpPost]
        [Authorize]
        public IActionResult GetDashboard(DashboardRequest request)
        {
            List<DashboardResponse> responses = _commonDL.GetDashboard(request);
            return Ok(responses);
        }

        [Route("GetRole")]
        [HttpGet]
        [Authorize]
        public IActionResult GetRole()
        {
            List<Role> roles = _commonDL.GetRoleListDL();
            return Ok(roles);
        }

        [Route("GetAllIssue")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllIssue(bool includeAddress = false)
        {
            List<Issue> issues = _commonDL.GetIssue();
            return Ok(issues);
        }


        [Route("GetAllStatus")]
        [HttpPost]
        [Authorize]
        public IActionResult GetAllStatus(CardStatus_request request)
        {
            List<CardStatus> statuses = _commonDL.GetStatus(request);
            return Ok(statuses);
        }


        [Route("GetBatchDL")]
        [HttpPost]
        [Authorize]
        public IActionResult GetBatchDL(BatchListRequest request)
        {
            List<BatcheListResponse> dashboardResponse_ = _commonDL.GetBatchDL(request);
            return Ok(dashboardResponse_);
        }

        [Route("GetInvoiceList")]
        [HttpGet]
        [Authorize]
        public IActionResult GetInvoiceList()
        {
            List<InvoiceListResponse> invoiceLists = _commonDL.GetInvoicelist();
            return Ok(invoiceLists);
        }

        [Route("GetErrorList")]
        [HttpGet]
        [Authorize]
        public IActionResult GetErrorList()
        {
            List<ErrorListResponse> errorListResponses = _commonDL.GetErrorlist();
            return Ok(errorListResponses);
        }
        [Route("CRU_Notification")]
        [HttpPost]
        [Authorize]
        public IActionResult CRU_Notification(Notification_Request _Request)
        {
            List<Notification_Response> Notifications = _commonDL.CRU_Notification(_Request);
            return Ok(Notifications);
        }


        [Route("Notification_count")]
        [HttpPost]
        [Authorize]
        public IActionResult Notification_Count(Notification_count_Request request)
        {
            Notification_count_Response Notifications = _commonDL.Notification_Count(request);
            return Ok(Notifications);
        }

        [Route("GetworktypeLabelText")]
        [HttpPost]
        [Authorize]
        public IActionResult GetworktypeLabelText(WorkTypeRequest typeRequest)
        {
            List<WorkType> worktypelist = _commonDL.GetWorkTypeDL(typeRequest);
            return Ok(worktypelist);
        }

        [Route("GetBusinessUnit")]
        [HttpGet]
        [Authorize]
        public IActionResult GetBusinessUnit()
        {
            List<BusinessType> BusinessType = _commonDL.GetBusinessUnitDL();
            return Ok(BusinessType);
        }
    }
}
