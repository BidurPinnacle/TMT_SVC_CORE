namespace TMT_Code_Migration1.Models.Production_Support_QA
{
    public class QA_Search_request
    {
        public string? Logged_in_user { get; set; }
        public string? SearchUser { get; set; }
        public int? TaskTypeID { get; set; }
        public int? WorkTypeID { get; set; }
        public string? Fromdate { get; set; }
        public string? Todate { get; set; }
        public int? cardsid { get; set; }
        public int? searchType { get; set; }
        public string? qaedUser { get; set; }
        public string? V_Installation { get; set; }
        public int? QASts { get; set; }
    }
    public class QA_Search_Request_By_Id
    {
        public int? CardId { get; set; }
    }
    public class StatusForQAResponse
    {
        public int? CARD_STATUS { get; set; }
        public string? CARD_DESCRIPTION { get; set; }
    }
    public class updateResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
    }
    public class updateCardRequestForQA
    {
        public int? cardId { get; set; }
        public int? statusId { get; set; }
        public string? RemarkValue { get; set; }
        public string? userId { get; set; }
    }

    public class QAEDUsers
    {
        public string? userId { get; set; }
        public string? userName { get; set; }
    }
    public class QAStatus
    {
        public string? CardDescription { get; set; }
        public int? QAStatuses { get; set; }
    }
}
