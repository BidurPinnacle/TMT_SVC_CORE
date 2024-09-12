namespace TMT_Code_Migration1.Models.Masters.System_Issue
{
    public class SystemIssueStatusData
    {
        public int? statusId { get; set; }
        public string? statusName { get; set; }
        public string? statusActive { get; set; }
    }
    public class InsertUpdateSystemIssueStatusData
    {
        public int? returnCode { get; set; }
        public string? returnText { get; set; }
    }
    public class InsUpdateSystemIssueStatusRequest
    {
        public string? operationType { get; set; }
        public int? statusId { get; set; }
        public string? statusActive { get; set; }
        public string? statusName { get; set; }
    }
}
