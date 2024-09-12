using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.DataLogics.Masters;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Common;
using TMT_Code_Migration1.Models.External_File;

namespace TMT_Code_Migration1.Controllers
{
    [Route("TMTService")]
    public class ExternalFileController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly ExternalFileDL _externalFileDL;
        private readonly IConfiguration _configuration;

        public ExternalFileController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _externalFileDL = new ExternalFileDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("upload_raw_excel")]
        [ResponseType(typeof(HttpResponseMessage))]
        [HttpPost]
        [Authorize]
        public IActionResult AddCard(External_File_request request)
        {
            Common response = new Common();
            try
            {
                response = _externalFileDL.Upload_excel(request);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while uploading the file.");
            }
            return Ok(response);
        }
    }
}
