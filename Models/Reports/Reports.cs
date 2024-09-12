namespace TMT_Code_Migration1.Models.Reports
{
    public class ReportsCardsDetailsRequest
    {
        public string? datefrom { get; set; }
        public string? dateto { get; set; }
        public string? companyid { get; set; }
        public string? EditorName { get; set; }
        public string? worktype { get; set; }


    }
    public class ReportsCardsDetailsResponse
    {
        public string? Date { get; set; }
        public string? CompanyName { get; set; }
        public string? WorkType { get; set; }
        public string? Editor { get; set; }
        public string? Premise { get; set; }
        public string? Analyst { get; set; }
        public string? CardUrl { get; set; }
        public string? CardStatus { get; set; }
        public string? Comment { get; set; }

        public string? Address { get; set; }

        public string? CardsID { get; set; }
        public string? superreview { get; set; }

    }
    public class GetReportResponse
    {
        public DateTime COMPLETED_DATE { get; set; }
        public string PREMISE_NUMBER { get; set; }

        public string SERV_ORDER_NO { get; set; }
        public string CardUrl { get; set; }

        public string STREET { get; set; }
        public string STREET_SUFFIX { get; set; }
        public string HOUSE_NO { get; set; }
        public string WORK_TYPE { get; set; }
        public string FULLNAME { get; set; }
        public string CARD_DESCRIPTION { get; set; }
        public string ISSUENAME { get; set; }
        public string CITY { get; set; }

        public string CARD_URL_PR { get; set; }


    }
    public class GetClientReportResponse
    {
        public DateTime? COMPLETED_DATE { get; set; }
        public string? PREMISE_NUMBER { get; set; }

        public string? SERV_ORDER_NO { get; set; }
        public string? CardUrl { get; set; }
        public string? CardsID { get; set; }
        public string? STREET { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? WORK_TYPE { get; set; }
        public string? FULLNAME { get; set; }
        public string? CARD_DESCRIPTION { get; set; }
        public string? ISSUENAME { get; set; }
        public int? IMAGE_ID { get; set; }
        public string? CITY { get; set; }

    }
    public class GetClientReportRequest
    {
        public string? V_COMPANY { get; set; }
        public string? V_USER { get; set; }
        public string? VSTATUS { get; set; }
        public string? V_DATEFROM { get; set; }
        public string? V_DATETO { get; set; }
        public int? V_ISSUETYPE { get; set; }
        public int? V_WORK_TYPE { get; set; }
        public string? V_CITY { get; set; }
        public string? V_House_number { get; set; }
        public string? V_PREMISE { get; set; }
        public string? V_STREET { get; set; }



    }
    public class GetReportRequest
    {
        public string? V_COMPANY { get; set; }
        public string? V_USER { get; set; }

        public string? VSTATUS { get; set; }
        public string? V_DATEFROM { get; set; }
        public string? V_DATETO { get; set; }
        public int? V_ISSUETYPE { get; set; }
        public int? V_WORK_TYPE { get; set; }

    }
}
