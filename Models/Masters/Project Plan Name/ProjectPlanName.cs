namespace TMT_Code_Migration1.Models.Masters.Project_Plan_Name
{
    public class ProjectPlanData
    {
        public int? projectId { get; set; }
        public string? projectPlanName { get; set; }
        public string? projectStatus { get; set; }
    }
    public class InsertUpdateProjectPlanData
    {
        public int? returnCode { get; set; }
        public string? returnText { get; set; }
    }
    public class InsUpdateProjectRequest
    {
        public string? operationType { get; set; }
        public int? projectId { get; set; }
        public string? status { get; set; }
        public string? planName { get; set; }
    }
}
