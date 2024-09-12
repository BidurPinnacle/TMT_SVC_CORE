using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.DataLogics.Masters;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Reports;
using TMT_Code_Migration1.Views.Interfaces;

namespace TMT_Code_Migration1.Controllers
{
    [Route("TMTService")]
    public class ReportsController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly ReportsDL _reportsDL;
        private readonly IConfiguration _configuration;

        public ReportsController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _reportsDL = new ReportsDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("ReportCardDetails")]
        [HttpPost]
        [Authorize]
        public IActionResult ReportCardDetails(ReportsCardsDetailsRequest reportsCardsDetailsRequest)
        {
            List<ReportsCardsDetailsResponse> CardList = _reportsDL.GetCardsDetails(reportsCardsDetailsRequest);
            return Ok(CardList);
        }

        [Route("getWeeklyReport")]
        [HttpPost]
        [Authorize]
        public IActionResult GetReport(GetReportRequest request)
        {
            List<GetReportResponse> getReportResponses = _reportsDL.GetReport(request);
            return Ok(getReportResponses);
        }
        public interface IDependencyResolver : IDependencyScope, IDisposable
        {
            IDependencyScope BeginScope();
        }
        public interface IDependencyScope : IDisposable
        {
            object GetService(Type serviceType);
            IEnumerable<object> GetServices(Type serviceType);
        }


        [Route("getWeeklyClientReport")]
        [HttpPost]
        [Authorize]
        public IActionResult GetClientReport(GetClientReportRequest request)
        {
            List<GetClientReportResponse> getReportResponses = _reportsDL.GetClientReport(request);
            return Ok(getReportResponses);
        }
    }
}
