using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using TMT_Code_Migration1.DataLogics.Masters;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.Task_List;

namespace TMT_Code_Migration1.Controllers
{
    [Route("Master/TaskList")]
    public class TaskListController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly TaskListDL _taskListDL;
        private readonly IConfiguration _configuration;

        public TaskListController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _taskListDL = new TaskListDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GetTaskListData")]
        [HttpGet]
        [Authorize]
        public IActionResult GetTaskListData()
        {
            List<TaskListData> taskListDatas = _taskListDL.GetTaskListData();
            return Ok(taskListDatas);
        }
        [Route("GetTaskDataId/{tid}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetTaskDataById(int tid)
        {
            List<TaskListData> taskListDatas = _taskListDL.GetTaskDataById(tid);
            return Ok(taskListDatas);
        }
        [Route("insertUpdateTask/")]
        [HttpPost]
        [Authorize]
        public IActionResult insertUpdateTask(InsUpdateTaskRequest rqst)
        {
            InsertUpdateTaskListData insUpdateResponse = _taskListDL.insertUpdateTask(rqst);
            return Ok(insUpdateResponse);
        }
        [Route("GetTaskDataForWorkType")]
        [HttpGet]
        [Authorize]
        public IActionResult GetTaskDataForWorkType()
        {
            List<TasksListItems> taskListDatas = _taskListDL.GetTaskDataForWorkType();
            return Ok(taskListDatas);
        }
    }
}
