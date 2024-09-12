using Microsoft.AspNetCore.Mvc;
using TMT_Code_Migration1.DataLogics.Utility;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Authorization;
using TMT_Code_Migration1.Models.Masters.CompanyForMaster;
using TMT_Code_Migration1.DataLogics.Masters;

namespace TMT_Code_Migration1.Controllers
{
    [Route("Master/company")]
    public class CompanyForMasterController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly CompanyForMasterDL _companyForMasterDL;
        private readonly IConfiguration _configuration;

        public CompanyForMasterController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _companyForMasterDL = new CompanyForMasterDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GetCompanyForMasterData")]
        [HttpGet]
        [Authorize]
        public IActionResult GetCompanyForMasterData()
        {
            List<CompanyForMasterData> companyForMasterDatas = _companyForMasterDL.GetCompanyForMasterData();
            return Ok(companyForMasterDatas);
        }
        [Route("GetCompanyForMasterDataById/{companyId}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetCompanyForMasterDataById(int companyId)
        {

            List<CompanyForMasterData> companyForMasterDatas = _companyForMasterDL.GetCompanyForMasterDataById(companyId);
            return Ok(companyForMasterDatas);
        }
        [Route("insertUpdateCompanyForMaster/")]
        [HttpPost]
        [Authorize]
        public IActionResult insertUpdateCompanyForMaster(InsUpdateCompanyForMasterRequest rqst)
        {
            InsertUpdateCompanyForMasterData insertUpdateCompanyForMasterData = _companyForMasterDL.insertUpdateCompanyForMaster(rqst);
            return Ok(insertUpdateCompanyForMasterData);
        }
    }
}
