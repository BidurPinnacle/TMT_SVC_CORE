using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics.Masters;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters;

namespace TMT_Code_Migration1.Controllers
{
    [Route("Master")]
    public class MastersController : Controller
    {
        private readonly MastersDL _mastersDL;
        public MastersController([FromServices]CommonDl commonDl)
        {
            _mastersDL = new MastersDL(commonDl);
        }
        [Route("GetTableNames")]
        [HttpGet]
        [Authorize]
        public IActionResult GetTableNames()
        {
            List<TableNames>  tableNames = _mastersDL.GetTableNames();
            return Ok(tableNames);
        }
    }
}
