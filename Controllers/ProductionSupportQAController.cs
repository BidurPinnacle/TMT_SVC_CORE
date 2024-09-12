using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.Models.Production_Support_QA;
using Microsoft.AspNetCore.Authorization;

namespace TMT_Code_Migration1.Controllers
{
    [Route("TMTService")]
    public class ProductionSupportQAController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly ProductionSupportQADL _productionSupportQADL;
        private readonly IConfiguration _configuration;

        public ProductionSupportQAController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _productionSupportQADL = new ProductionSupportQADL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GETCARDBYUSERQA_SEARCH")]
        [HttpPost]
        [Authorize]
        public IActionResult GETCARDBYUSERQA_SEARCH(QA_Search_request request)
        {
            var respo = _productionSupportQADL.GET_GETCARDBYUSERQA_SEARCH(request, "SP_GETCARDBYUSERQA_SEARCH");
            return Ok(respo);
        }
        [Route("GETCARDBYUSERQA_SEARCHByID/{request}")]
        [HttpGet]
        [Authorize]
        public IActionResult GETCARDBYUSERQA_SEARCHById(int request)
        {
            var respo = _productionSupportQADL.GET_GETCARDBYUSERQA_SEARCHById(request, "SP_GETCARDBYUSERQA_SEARCH_BY_ID");
            return Ok(respo);
        }
        [Route("GetStatusListForSupportQA")]
        [HttpGet]
        [Authorize]
        public IActionResult GETCARDBYUSERQA_SEARCHById()
        {
            var respo = _productionSupportQADL.GetCardStatusDL();
            return Ok(respo);
        }
        [Route("PostUpdateCardForQA")]
        [HttpPost]
        [Authorize]
        public IActionResult UpdateCardForQA(List<updateCardRequestForQA> rqst)
        {
            updateResponse respo = _productionSupportQADL.UpdateCardForQA(rqst);
            return Ok(respo);
        }
        [Route("GetAllQAEDByUsers/{loginUser}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetQAEDByUserController(string loginUser)
        {
            List<QAEDUsers> respo = _productionSupportQADL.GetQAEDByUserDL(loginUser);
            return Ok(respo);
        }
        [Route("GetDistinctQAStatus")]
        [HttpGet]
        [Authorize]
        public IActionResult GetDistinctQAStatus()
        {
            List<QAStatus> respo = _productionSupportQADL.GetDistinctQAStatus();
            return Ok(respo);
        }
    }
}
