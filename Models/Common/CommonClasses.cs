namespace TMT_Code_Migration1.Models.Common
{

    public class CaseStatusType
    {
        public int? STATUSID { get; set; }
        public string? STATUSNAME { get; set; }

    }
    public class ClintBatchRequest
    {
        public int? BatchID { get; set; }
    }
    public class TaskType
    {


        public int? TASKID { get; set; }
        public string? TASKTEXT { get; set; }

    }
    public class RootCause
    {
        public int? causeId { get; set; }
        public string? rootCause { get; set; }
    }
    public class BusinessType
    {
        public int? BID { get; set; }
        public string? BUSINESS_UNIT { get; set; }

    }
    public class WorkType
    {
        public int? worktypevalue { get; set; }
        public string? worktypename { get; set; }
        public string? labelAs { get; set; }

    }



    public class WorkTypeRequest
    {
        public int? taskID { get; set; }


    }
    public class rootCauseRequest
    {
        public int? taskId { get; set; }
        public int? workTypeId { get; set; }
        public int? statusId { get; set; }
    }
    public class BatcheListResponse
    {
        public int? BatchID { get; set; }
        public string? BatchName { get; set; }
    }

    public class BatchListRequest
    {
        public string? BatchStatus { get; set; }
    }
    public class workYears
    {
        public int? workyear { get; set; }
        public int? yearstatus { get; set; }

    }

    public class InvoiceListResponse
    {
        public string? INVOICE_NUMBER { get; set; }
        public int? INVOICE_ID { get; set; }
    }

    public class ErrorListResponse
    {
        public int? ErrorID { get; set; }
        public string? ErrorName { get; set; }
    }

    public class Notification_Request
    {
        public int? Nid { get; set; }
        public string? UserID { get; set; }
        public string? Statement { get; set; }

    }
    public class Notification_Response
    {
        public int? Nid { get; set; }
        public string? Premise { get; set; }
        public string? SERV_ORDER_NO { get; set; }
        public string? ETYPENAME { get; set; }
        public string? ESUBTYPENAME { get; set; }
        public string? NOTIFICATION_TEXT { get; set; }
        public string? NOTIFICATION_SEEN { get; set; }
        public string? NOTICE_SEND_BY { get; set; }
        public string? CREATED_DATE { get; set; }
        public string? card_address { get; set; }




    }
    public class Notification_count_Request
    {
        public string? userid { get; set; }
    }
    public class Notification_count_Response
    {
        public int? Notification_count { get; set; }
    }
    public class Role
    {
        public int? Roleid { get; set; }
        public string? RoleName { get; set; }
    }
    public class DashboardRequest
    {
        public string? role { get; set; }
        public string? ProfileID { get; set; }
        public string? FROMDATE { get; set; }
        public string? TODATE { get; set; }

    }
    public class DashboardBatchReports_Data_Request
    {
        public string? PROFILEID { get; set; }
        public string? FROMDATE { get; set; }
        public string? TODATE { get; set; }



    }
    public class DashboardBatchReports_Data_respose
    {
        public string? STATUS { get; set; }
        public int? CARDSCOUNT { get; set; }

    }
    public class DashboardIssueReports_Data
    {
        public string? ISSUENAME { get; set; }
        public int? CARDSCOUNT { get; set; }
        public int? ISSUEID { get; set; }

    }

    public class DashboardIssueReports_Data_Request
    {
        public string? PROFILEID { get; set; }
        public string? FROMDATE { get; set; }
        public string? TODATE { get; set; }


    }
    public class DashboardResponse_All
    {
        public int? total_card_process { get; set; }
        public int? total_qa_completed { get; set; }
        public int? total_card_draw_complete { get; set; }
        public int? total_card_process_lastweek { get; set; }
        public int? total_qa_completed_lastweek { get; set; }
        public int? total_card_draw_complete_lastweek { get; set; }
        public int? total_issue_card { get; set; }
        public int? total_issue_card_lastweek { get; set; }
        public DateTime? Estimated_last_day_of_work { get; set; }
        public Double? TOTALCOMPLETE_PERCENT { get; set; }
        public Double? TOTALISSUE_PERCENT { get; set; }
        public Double? TOTALPENDING_PERCENT { get; set; }
        public Double? RESOLVEDISSUE { get; set; }
        public Double? PENDINGISSUE { get; set; }

        public Double? TARGETPERWEEK { get; set; }

        public DateTime? Taget_completion_date { get; set; }

        public int? TOTAL_PROCESSED { get; set; }

        public int? TOTAL_DRAWN { get; set; }
        public int? QA_INPROGRESS_D { get; set; }
        public int? QA_COMPLETED_D { get; set; }

        public int? NOT_DRAWABLE { get; set; }
        public int? QA_INPROGRESS_ND { get; set; }
        public int? QA_COMPLETED_ND { get; set; }

        public int? TOTAL_REVIEWED { get; set; }
        public int? QA_COMPLETED_REVIEWED { get; set; }
        public int? QA_INPROGRESS_REVIEWED { get; set; }



        public int? TOTAL_PROCESSED_WEEKLY { get; set; }
        public int? TOTAL_DRAWN_WEEKLY { get; set; }
        public int? QA_INPROGRESS_D_WEEKLY { get; set; }
        public int? QA_COMPLETED_D_WEEKLY { get; set; }

        public int? NOT_DRAWABLE_WEEKLY { get; set; }
        public int? QA_COMPLETED_ND_WEEKLY { get; set; }
        public int? QA_INPROGRESS_ND_WEEKLY { get; set; }
        public int? TOTAL_REVIEWED_WEEKLY { get; set; }
        public int? QA_COMPLETED_REVIEWED_WEEKLY { get; set; }
        public int? QA_INPROGRESS_REVIEWED_WEEKLY { get; set; }
    }
    public class DashboardRequest_progressive
    {
        public string? role { get; set; }
        public string? ProfileID { get; set; }
        public string? Chartreport { get; set; }
        public string? datefrom { get; set; }
        public string? dateto { get; set; }
    }
    public class Dashboardresponse_Progressive
    {
        public int? TARGETPERWEEK { get; set; }
        public int? LASTWEEKCOMPLETED { get; set; }
        public int? WEEKLYCOMPLETE_PERCENT { get; set; }



        public int? ENTRYYEAR { get; set; }
        public int? TOTALCOUNT { get; set; }
        public int? TOTALCOMPLETED { get; set; }
        public int? TOTALPENDING { get; set; }


    }
    public class Dashboardresponse_Weekcount
    {
        public int? week { get; set; }
        public int? data { get; set; }
    }
    public class TASKSUMMARYCOUNT_RESPONSE
    {
        public ICollection<TASKSUMMARYCOUNT_DAY_RESPONSE>? _DAY_RESPONSEs { get; set; } = new List<TASKSUMMARYCOUNT_DAY_RESPONSE>();
        public ICollection<TASKSUMMARYCOUNT_WEEK_RESPONSE>? _WEEK_RESPONSEs { get; set; } = new List<TASKSUMMARYCOUNT_WEEK_RESPONSE>();
        public ICollection<TASKSUMMARYCOUNT_WEEK1_RESPONSE>? _WEEK1_RESPONSEs { get; set; } = new List<TASKSUMMARYCOUNT_WEEK1_RESPONSE>();
        public ICollection<TASKSUMMARYCOUNT_TOTAL_RESPONSE>? _TOTAL_RESPONSEs { get; set; } = new List<TASKSUMMARYCOUNT_TOTAL_RESPONSE>();


        public ICollection<TASKSUMMARYCOUNT_HEADER_DAY>? _HEADER_RESPONSEs_DAY { get; set; }
        public ICollection<TASKSUMMARYCOUNT_HEADER_WEEK>? _HEADER_RESPONSEs_WEEK { get; set; }

        public ICollection<TASKSUMMARYCOUNT_HEADER_WEEK1>? _HEADER_RESPONSEs_WEEK1 { get; set; }
        public ICollection<TASKSUMMARYCOUNT_HEADER_TOTAL>? _HEADER_RESPONSEs_TOTAL { get; set; }



    }
    public class TASKSUMMARYCOUNT_DAY_RESPONSE
    {
        public int? TASKID_DAY { get; set; }
        public int? TASKCOUNT_DAY { get; set; }
        public string? TASKTEXT_DAY { get; set; }
        public int? TASKCOUNTADDED_DAY { get; set; }
        public int? TOTAL_COUNT_DAY { get; set; }
    }

    public class TASKSUMMARYCOUNT_WEEK_RESPONSE
    {
        public int? TASKID_WEEK { get; set; }
        public int? TASKCOUNT_WEEK { get; set; }
        public string? TASKTEXT_WEEK { get; set; }
        public int? TASKCOUNTADDED_WEEK { get; set; }
        public int? TOTAL_COUNT_WEEK { get; set; }
    }
    public class WORKTYPESUMMARYCOUNT_RESQUEST
    {
        public int? TASKID { get; set; }
        public string? COUNT_TYPE { get; set; }

    }
    public class WORKTYPESUMMARYCOUNT_RESPONSE
    {
        public int? casecount { get; set; }
        public string? card_status { get; set; }
        public string? card_Description { get; set; }
    }
    public class TASKSUMMARYCOUNT_WEEK1_RESPONSE
    {
        public int? TASKID_WEEK1 { get; set; }
        public int? TASKCOUNT_WEEK1 { get; set; }
        public string? TASKTEXT_WEEK1 { get; set; }
        public int? TASKCOUNTADDED_WEEK1 { get; set; }
        public int? TOTAL_COUNT_WEEK1 { get; set; }
    }

    public class TASKSUMMARYCOUNT_TOTAL_RESPONSE
    {
        public int? TASKID_TOTAL { get; set; }
        public int? TASKCOUNT_TOTAL { get; set; }
        public string? TASKTEXT_TOTAL { get; set; }
        public int? TASKCOUNTADDED_TOTAL { get; set; }
        public int? TOTAL_COUNT_TOTAL { get; set; }
    }


    public class TASKSUMMARYCOUNT_HEADER_DAY
    {
        public int? TOTAL_TASKCOUNT_DAY { get; set; }
        public int? TOTAL_TASKCOUNTADDED_DAY { get; set; }

    }
    public class TASKSUMMARYCOUNT_HEADER_WEEK1
    {
        public int? TOTAL_TASKCOUNT_WEEK1 { get; set; }
        public int? TOTAL_TASKCOUNTADDED_WEEK1 { get; set; }

    }

    public class TASKSUMMARYCOUNT_HEADER_WEEK
    {
        public int? TOTAL_TASKCOUNT_WEEK { get; set; }
        public int? TOTAL_TASKCOUNTADDED_WEEK { get; set; }

    }
    public class TASKSUMMARYCOUNT_HEADER_TOTAL
    {
        public int? TOTAL_TASKCOUNT_TOTAL { get; set; }
        public int? TOTAL_TASKCOUNTADDED_TOTAL { get; set; }

    }
    public class Company
    {
        public int? CId { get; set; }
        public string? CName { get; set; }
        public string? CDescription { get; set; }
    }
    public class DashboardResponse
    {
        public string? CountedCard { get; set; }
        public string? Description { get; set; }
        public string? status { get; set; }
        public string? Editor { get; set; }
        public string? QCby { get; set; }

    }
    public class Issue
    {
        public int? ISSUEID { get; set; }
        public string? ISSUENAME { get; set; }
    }

    public class CardStatus
    {
        public string? Card_status { get; set; }
        public string? card_Description { get; set; }
    }
    public class CardStatus_request
    {
        public int? CardStatus_value { get; set; }

    }
}
