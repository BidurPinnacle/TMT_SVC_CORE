using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.DataLogics;
using TMT_Code_Migration1.Models.Masters.Project_Plan_Name;
using TMT_Code_Migration1.DataLogics.Masters;

namespace TMT_Code_Migration1.Controllers
{
    [Route("Master/ProjectPlan")]
    public class ProjectPlanNameController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly ProjectPlanNameDL _projectPlanNameDL;
        private readonly IConfiguration _configuration;

        public ProjectPlanNameController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _projectPlanNameDL = new ProjectPlanNameDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GetProjectPlanData")]
        [HttpGet]
        [Authorize]
        public IActionResult GetProjectPlanData()
        {
            List<ProjectPlanData> projectPlanDatas = _projectPlanNameDL.GetProjectPlanData();
            return Ok(projectPlanDatas);
        }
        [Route("GetProjectPlanDataById/{projectId}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetProjectPlanDataById(int projectId)
        {
            List<ProjectPlanData> projectPlanDatas = _projectPlanNameDL.GetProjectPlanDataById(projectId);
            return Ok(projectPlanDatas);
        }
        [Route("insertUpdateProject/")]
        [HttpPost]
        [Authorize]
        public IActionResult insertUpdateProject(InsUpdateProjectRequest rqst)
        {
            InsertUpdateProjectPlanData insertUpdateProjectPlanData = _projectPlanNameDL.insertUpdateProject(rqst);
            return Ok(insertUpdateProjectPlanData);
        }
    }
}
