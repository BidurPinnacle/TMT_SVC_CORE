namespace TMT_Code_Migration1.Models.Masters.System_Issue
{
    public class SystemIssueListData
    {
        public int? issueId { get; set; }
        public string? issueText { get; set; }
        public string? issueDescription { get; set; }
        public string? issueStatus { get; set; }
    }
    public class InsertUpdateSystemIssueListData
    {
        public int? returnCode { get; set; }
        public string? returnText { get; set; }
    }
    public class InsUpdateSystemIssueRequest
    {
        public string? operationType { get; set; }
        public int? issueId { get; set; }
        public string? issueText { get; set; }
        public string? issueDescription { get; set; }
        public string? issueStatus { get; set; }
    }
}
