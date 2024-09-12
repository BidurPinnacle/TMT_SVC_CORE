using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics.Masters;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.Root_Cause;

namespace TMT_Code_Migration1.Controllers
{
    [Route("Master/rootCauseMapping")]
    public class MappingRootCauseController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly MappingRootCauseDL _mappingRootCauseDL;
        private readonly IConfiguration _configuration;

        public MappingRootCauseController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _mappingRootCauseDL = new MappingRootCauseDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GetAvailableRootCause/")]
        [HttpPost]
        [Authorize]
        public IActionResult GetAvailableDataInRootCause(RootCauseMappingRequest rqst)
        {
            List<RootCauseMappingResponse> rootCauseMappingResponses = _mappingRootCauseDL.GetAvailableDataInRootCause(rqst);
            return Ok(rootCauseMappingResponses);
        }
        [Route("GetMappedRootCause/")]
        [HttpPost]
        [Authorize]
        public IActionResult GetMappedDataInRootCause(RootCauseMappingRequest rqst)
        {
            List<RootCauseMappingResponse> rootCauseMappingResponses = _mappingRootCauseDL.GetMappedDataInRootCause(rqst);
            return Ok(rootCauseMappingResponses);
        }
        [Route("InsertDataToCauseMapping/")]
        [HttpPost]
        [Authorize]
        public IActionResult InsertDataToCauseMapping(List<RootCauseMappingInsertRequest> rqst)
        {
            List<RootCauseResponse>  rootCauseResponse = _mappingRootCauseDL.InsertDataToCauseMapping(rqst);
            return Ok(rootCauseResponse);
        }
        [Route("DeleteDataFromCauseMapping/")]
        [HttpPost]
        [Authorize]
        public IActionResult DeleteDataFromCauseMapping(List<RootCauseMappingDeleteRequest> rqst)
        {
            List<RootCauseResponse> rootCauseResponse = _mappingRootCauseDL.DeleteDataFromCauseMapping(rqst);
            return Ok(rootCauseResponse);
        }
    }
}
