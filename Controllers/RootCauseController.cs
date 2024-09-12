using Microsoft.AspNetCore.Mvc;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.DataLogics.Masters;
using System.Web.Http.Description;
using TMT_Code_Migration1.Models.Masters.Root_Cause;
using Microsoft.AspNetCore.Authorization;

namespace TMT_Code_Migration1.Controllers
{
    [Route("Master/rootCause")]
    public class RootCauseController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly RootCauseDL _rootCauseDL;
        private readonly IConfiguration _configuration;

        public RootCauseController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _rootCauseDL = new RootCauseDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GetRootCauseForMasterData")]
        [HttpGet]
        [Authorize]
        public IActionResult GetRootCauseForMasterData()
        {
            List<RootCauseForMasterData> rootCauseForMasterDatas = _rootCauseDL.GetRootCauseForMasterData();
            return Ok(rootCauseForMasterDatas);
        }
        [Route("GetRootCauseForMasterDataById/{causeId}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetRootCauseForMasterDataById(int causeId)
        {
            List<RootCauseForMasterDataById> rootCauseForMasterDatas = _rootCauseDL.GetRootCauseForMasterDataById(causeId);
            return Ok(rootCauseForMasterDatas);

        }
        [Route("insertUpdateRootCauseForMaster/")]
        [HttpPost]
        [Authorize]
        public IActionResult insertUpdateCompanyForMaster(InsUpdateRootCauseForMasterRequest rqst)
        {
            InsertUpdateRootCauseForMasterData insertUpdateRootCauseForMasterData = _rootCauseDL.insertUpdateRootCauseForMaster(rqst);
            return Ok(insertUpdateRootCauseForMasterData);
        }
    }
}
