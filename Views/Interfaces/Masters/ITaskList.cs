using TMT_Code_Migration1.Models.Masters.Task_List;

namespace TMT_Code_Migration1.Views.Interfaces.Masters
{
    public interface ITaskList
    {
        List<TaskListData> GetTaskListData();
        List<TaskListData> GetTaskDataById(int tid);
        InsertUpdateTaskListData insertUpdateTask(InsUpdateTaskRequest rqst);
        List<TasksListItems> GetTaskDataForWorkType();
    }
}
