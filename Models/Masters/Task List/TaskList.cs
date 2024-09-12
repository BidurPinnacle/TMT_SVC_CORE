namespace TMT_Code_Migration1.Models.Masters.Task_List
{
    public class TaskListData
    {
        public int? TASKID { get; set; }
        public string? TASKTEXT { get; set; }
        public string? labelAs { get; set; }
        public string? ACTIVE { get; set; }
        public int? priority { get; set; }
        public string? assignmentStatus
        { get; set; }
    }
    public class InsertUpdateTaskListData
    {
        public int? returnCode { get; set; }
        public string? returnText { get; set; }
    }
    public class InsUpdateTaskRequest
    {
        public string? operationType { get; set; }
        public int? taskId { get; set; } // Null Checking
        public string? taskText { get; set; }
        public string? active { get; set; }
        public int? priority { get; set; } // Null Checking
        public string? labels { get; set; }
        public string? assignmentStatus { get; set; }
    }
    public class AssignmentPageData
    {
        public string? AssignmentVal { get; set; }
        public string? AssignmentText { get; set; }
    }
    public class TasksListItems
    {
        public int? taskId { get; set; }
        public string? taskText { get; set; }
    }
}
