using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Editors;

namespace TMT_Code_Migration1.Controllers
{
    [Route("TMTService")]
    public class EditorController : Controller
    {
        private readonly EditorDL _editorDL;
        private readonly IConfiguration _config;

        public EditorController([FromServices] IConfiguration config, EditorDL editorDL)
        {
            _editorDL = editorDL;
            _config = config;
        }
        [Route("AddStatus")]
        [HttpPost]
        [Authorize]
        public IActionResult AddStatus(List<StatusRequest> request)
        {
            StatusResponse response = _editorDL.AddStatus(request);
            return Ok(response);
        }

        [Route("EditorsSubmit")]
        [HttpPost]
        [Authorize]
        public IActionResult EditorsSubmit(EditorssubmitRequest request)
        {
            StatusResponse response = _editorDL.EditorSubmit(request);
            return Ok(response);

            
        }
    }
}
