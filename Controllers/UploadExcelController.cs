using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Cards;
using TMT_Code_Migration1.Models.External_File;

namespace TMT_Code_Migration1.Controllers
{
    [Route("TMTService")]
    public class UploadExcelController : Controller
    {
        private readonly Related_To_Excel _related_To_Excel;
        private readonly IConfiguration _config;
        public UploadExcelController(Related_To_Excel relate, [FromServices] IConfiguration config)
        {
            _related_To_Excel = relate;
            _config = config;
        }
        [Route("upload_raw_excel")]
        [HttpPost]
        [Authorize]
        public IActionResult AddCard(External_File_request request)
        {
            Common response = _related_To_Excel.Upload_excel(request);
            return Ok(response);
        }
    }
}
