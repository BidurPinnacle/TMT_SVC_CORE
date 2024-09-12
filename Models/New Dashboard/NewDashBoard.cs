namespace TMT_Code_Migration1.Models.New_Dashboard
{
    public class Assignement_Response
    {
        public int? AssignmentID { get; set; }
        public string? AssignmentName { get; set; }

    }
    public class Assignement_Request
    {
        public int? CompanyID { get; set; }

    }
    public class Projects_Response
    {
        public int? ProjectID { get; set; }
        public string? ProjectName { get; set; }
    }
    public class Projects_Request
    {
        public int? AssignmentID { get; set; }
    }
    public class WeekCount_Response
    {
        public int? ID { get; set; }
        public string? WeekCount { get; set; }
    }
    public class NewDashboard_request
    {
        public int? AssignmentID { get; set; }
        public string? projectID { get; set; }

        public DateTime? Date_from { get; set; }
        public DateTime? Date_To { get; set; }
        public int? WeekListindex { get; set; }
        public string? Status { get; set; }

        public int? client { get; set; }



    }
    public class DASHBOARD_DATA_ASSIGNMENT
    {
        public int? AssignmentID { get; set; }
        public string? projectID { get; set; }
        public DateTime? Date_from { get; set; }
        public DateTime? Date_To { get; set; }
        public int? WeekListindex { get; set; }
        public string? Status { get; set; }
        public int? client { get; set; }
    }

    public class Status_Request
    {
        public int? AssignmentID { get; set; }
    }
    public class Status_Response
    {
        public int? StatusID { get; set; }
        public string? StatusText { get; set; }
    }

    public class NewDashboard_Status_Request
    {
        public int? AssignmentID { get; set; }
        public int? projectID { get; set; }
        public DateTime? Date_from { get; set; }
        public DateTime? Date_To { get; set; }
        public int? WeekListindex { get; set; }
        public string? Status { get; set; }
    }
    public class NewDashboard_User_Request
    {
        public int? AssignmentID { get; set; }
        public int? projectID { get; set; }
        public DateTime? Date_from { get; set; }
        public DateTime? Date_To { get; set; }
        public int? WeekListindex { get; set; }
        public string? Status { get; set; }
    }

    public class DS_REQUEST
    {
        public int? AssignmentID { get; set; }
        public string? projectID { get; set; }
        public DateTime? Date_from { get; set; }
        public DateTime? Date_To { get; set; }
        public int? WeekListindex { get; set; }
        public string? Status { get; set; }
        public int? ClientID { get; set; }
    }
    public class AccessForComment
    {
        public int? AccessCode { get; set; }
    }
    public class DashboardCommentRequest
    {
        public DateTime? EntryDate { get; set; }
    }

    public class DashboardCommentResponse
    {
        public int? AutoId { get; set; }
        public string? CommentText { get; set; }
    }

    public class DashboardCommentInsUpdateRequest
    {
        public DateTime? EntryDate { get; set; }
        public string? CommentText { get; set; }
        public string? UserId { get; set; }
        public int? CompanyId { get; set; }
    }

    public class DashboardCommentInsUpdateResponse
    {
        public string? OutputMessage { get; set; }
    }
    public class DashboardCommentList
    {
        public DateTime? EntryDate { get; set; }
        public string? CommentText { get; set; }
    }
}
