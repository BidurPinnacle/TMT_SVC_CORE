namespace TMT_Code_Migration1.Models.caseAssignment
{
    public class ExcelUploadedDataResponse
    {
        public string? TaskText { get; set; }
        public string? WorkTypeName { get; set; }
        public string? Bpem { get; set; }
        public string? Installation { get; set; }
        public string? CardDescription { get; set; }
        public int? RecordCount { get; set; }
        public DateTime? DateAdded { get; set; }
        public string? Uploader { get; set; }
        public string? AssignedTo { get; set; }
    }
    public class TaskTeamUserResponse
    {
        public string? UserId { get; set; }
        public string? FullName { get; set; }
    }

    public class ExcelUploadedDataRequest
    {
        public int? TaskType { get; set; }
        public string? WorkTypeId { get; set; }
        public int? CardStatusId { get; set; }
        public string? UserName { get; set; }
        public string? InstallNum { get; set; }
        public string? BpemNum { get; set; }
    }
    public class ReassignUserRequest
    {
        public string? NewUser { get; set; }
        public string? ReassignUser { get; set; }
        public List<BpemInstallation>? JsonData { get; set; }
    }

    public class BpemInstallation
    {
        public string? Bpem { get; set; }
        public string? Installation { get; set; }
    }
}
