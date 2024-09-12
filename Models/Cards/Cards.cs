using TMT_Code_Migration1.Models.Common;

namespace TMT_Code_Migration1.Models.Cards
{
    public class ClintBatchRequest
    {
        public int BatchID { get; set; }
    }
    public class AddCardRequest
    {
        public int? CardsID { get; set; }
        public string? EntryDate { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public string? SessionName { get; set; }
        public string? CardStatus { get; set; }
        public string? Premise { get; set; }
        public string? WorkOrderNo { get; set; }
        public string? Work_Type { get; set; }
        public int? State { get; set; }
        public string? StatementType { get; set; }
        public int? CheckedUGP { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedBY { get; set; }
        public string? CardUrl { get; set; }

        public string? RecordCount { get; set; }
        public int? RootCauseId { get; set; }

    }
    public class AddCardResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }


    }
    public class GatAllCards
    {
        public int? CardsID { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public string? SessionName { get; set; }
        public string? Premise { get; set; }
        public int? State { get; set; }
        public string? CName { get; set; }
        public string? worktypename { get; set; }
        public int? CheckedUGP { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedBY { get; set; }
        public string? desc { get; set; }
        public int? CID { get; set; }
        public string? cardUrl { get; set; }
        public string? FullName { get; set; }
        public string? superreview { get; set; }
        public string? IssueDesc { get; set; }
        public int? IssueID { get; set; }
        public int? QC_FOR_CURRENT_EDITOR { get; set; }
        public string? EDITOR_NAME { get; set; }
        public int? Image_id { get; set; }
        public int? duplicateCard { get; set; }
        public string? serv_order_no { get; set; }
        public string? CARD_URL_PR { get; set; }

        public int? RecordCount { get; set; }

        public string? userCreated { get; set; }


    }
    public class GetCardRequest
    {
        public DateTime? entrydate { get; set; }
        public int? CompanyID { get; set; }

    }
    public class GetCardbyCid
    {
        public int? CardsID { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public string? SessionName { get; set; }
        public string? CardStatus { get; set; }
        public string? Premise { get; set; }
        public string? WorkOrderNo { get; set; }
        public string? Work_Type { get; set; }
        public int? State { get; set; }
        public string? StatementType { get; set; }
        public int? CheckedUGP { get; set; }
        public string? Remark { get; set; }
        public string? CardUrl { get; set; }
        public string? updatedby { get; set; }

        public int? RecordCount { get; set; }
        public int? rootCauseId { get; set; }
    }
    public class GetUser
    {
        public string? UserId { get; set; }
        public string? UserType { get; set; }
        public string? UserName { get; set; }
        public int? CompanyID { get; set; }
        public string? Company { get; set; }
        public string? firstname { get; set; }
        public string? lastName { get; set; }
        public int? roleid { get; set; }
        public int? status { get; set; }




    }
    public class GetUserForSearch
    {
        public string? USERID { get; set; }
        public string? FULLNAME { get; set; }
    }
    public class userresponse
    {
        public string? UserId { get; set; }
        public string? Role { get; set; }
        public string? UserName { get; set; }
        public string? Company { get; set; }



    }
    public class CardBYUserRequest
    {
        public string? UserId { get; set; }
        public string? start_date { get; set; }
        public string? end_date { get; set; }


    }
    public class CardBYUserRequestAndKeywords
    {
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public string? start_date { get; set; }
        public string? end_date { get; set; }

        public string? tasktype { get; set; }
        public string? wtype { get; set; }
        public string? premise { get; set; }
        public string? CardUrl { get; set; }
        public string? serviceorder { get; set; }
        public string? Resolutiontype { get; set; }
        public string? SessionName { get; set; }
        public string? remark { get; set; }


        public int? RootCauseId { get; set; }












    }
    public class CardBYQCRequest
    {
        public string? QCId { get; set; }
        public string? EditorID { get; set; }
        public string? Entrydate { get; set; }
        public string? CardStatus { get; set; }

        public string? FilterValue { get; set; }



    }
    public class AssignQARequest
    {
        public int? CardsID { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedBY { get; set; }
        public string? AssignTo { get; set; }
        public string? CardStatus { get; set; }
        public string? statementType { get; set; }
        public string? VENTRY_DATE { get; set; }

        public string? PROFILEID { get; set; }


    }
    public class Un_AssignQARequest
    {
        public string? UpdatedBY { get; set; }
        public string?  VENTRY_DATE { get; set; }
        public string? statementType { get; set; }
    }
    public class Deleterequest
    {
        public int? CardId { get; set; }


    }
    public class AssignQAResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }

    }
    public class Un_AssignQAResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }

    }
    public class FeedbackRequest
    {
        public int? CardId { get; set; }

    }
    public class FeedbackResponse
    {
        //  public int FeedbackID { get; set; }
        //public int CardsID { get; set; }
        //public int CommentID { get; set; }
        public string? FeedBackText { get; set; }
        //public string UpdatedBy { get; set; }
        //public DateTime UpdatedOn { get; set; }
        public ICollection<FeedbackDetailResponse>? FeedbackDetailResponse { get; set; }

    }
    public class FeedbackDetailRequest
    {
        public int? CardId { get; set; }

    }
    public class FeedbackDetailResponse
    {
        //public int CrossFeedID { get; set; }
        //public int FeedbackID { get; set; }      
        public string? CrossFeedText { get; set; }
        //public string UpdatedBy { get; set; }
        //public DateTime UpdatedOn { get; set; }

    }
    public class CommentDetailsRequest
    {
        public int? CardsID { get; set; }

    }

    public class batchQACommentTableRequest
    {
        public int? CardsID { get; set; }

    }



    public class batchQACommentTableResponse
    {
        public int? batchID { get; set; }
        public int? cardID { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? Comment { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class CommentDetailsResponse
    {
        public string? Comment { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? updatedOn { get; set; }
        public string? CardStatus { get; set; }
        public string? QcBy { get; set; }

    }
    public class GetCardsforsupervisorRequest
    {
        public string? StatementType { get; set; }
        public string? cardstatus { get; set; }
        public string? userid { get; set; }
        public string? companyid { get; set; }
    }
    public class GetCardsforsupervisorResponse
    {
        public DateTime? Submiton { get; set; }
        public string? EditorName { get; set; }
        public int? CardCount { get; set; }
        public string? EditorId { get; set; }
        public string? Cardstatus { get; set; }
        public string? QcBy { get; set; }
        public int? Companyid { get; set; }
        public string? superreview { get; set; }
    }
    public class ExcelDataResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }

    }
    enum Responsecode
    {
        Successfull,
        Issuefound,
    }
    public class pushExcelDataRequest
    {
        public object? spdshtData { get; set; }
        public int? CompanyID { get; set; }

    }
    public class CSV_EXCEL_data
    {
        public string? PREMISE_NO { get; set; }
        public string? SERV_ORDER_NO { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? STREET { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? CITY { get; set; }
        public string? URL_LINK { get; set; }
        public string? SERVICE_LINE_INSTALL_DT { get; set; }
        public string? SUBDIVISION { get; set; }
        public string? ZIP_CODE { get; set; }

        public string? SERVICE_LINE_STATUS_CD { get; set; }
        public int? FREQ_CODE { get; set; }
        public string? INSP_TYPE { get; set; }
        public string? COMPANY_NO { get; set; }
        public int? LO_OFFICE { get; set; }

        public string? ROUTE_NO { get; set; }
        public string? READ_SEQ { get; set; }
        public string? SPECIAL_INST { get; set; }
        public string? MTR_READ_INST2 { get; set; }
        public string? MTR_LOCATION { get; set; }
        public string? DIRECTION { get; set; }
        public string? DIRECTION_SUFFIX { get; set; }
        public string? APARTMENT_TYPE { get; set; }
        public string? APARTMENT { get; set; }
        public string? ADDRESS_OVERFLOW { get; set; }
        public string? METER_STATUS { get; set; }
        public string? METER_ID { get; set; }
        public string? ACCOUNT_NO { get; set; }

        public string? LAT { get; set; }
        public string? LONG1 { get; set; }
        public string? GAS_LEAK_SURVEY_DT { get; set; }
        public string? SUBDIVISION_DESC { get; set; }
        public string? WORK_TYPE { get; set; }


    }
    public class ExcelDataRequest
    {
        public string? CARD_PREMISE_NO { get; set; }
        public string? CARD_SERV_ORDER_NO { get; set; }
        public string? CARD_HOUSE_NO { get; set; }
        public string? CARD_STREET { get; set; }
        public string? CARD_STREET_SUFFIX { get; set; }
        public string? CARD_CITY { get; set; }
        public string? CARD_URL_LINK { get; set; }
        public string? CARD_SERVICE_LINE_INSTALL_DT { get; set; }
        public string? CARD_SUBDIVISION { get; set; }
        public string? CARD_ZIP_CODE { get; set; }

        public string? CARD_SERVICE_LINE_STATUS_CD { get; set; }
        public int? CARD_FREQ_CODE { get; set; }
        public string? CARD_INSP_TYPE { get; set; }
        public string? CARD_COMPANY_NO { get; set; }
        public int? CARD_LO_OFFICE { get; set; }

        public string? CARD_ROUTE_NO { get; set; }
        public string? CARD_READ_SEQ { get; set; }
        public string? CARD_SPECIAL_INST { get; set; }
        public string? CARD_MTR_READ_INST2 { get; set; }
        public string? CARD_MTR_LOCATION { get; set; }
        public string? CARD_DIRECTION { get; set; }
        public string? CARD_DIRECTION_SUFFIX { get; set; }
        public string? CARD_APARTMENT_TYPE { get; set; }
        public string? CARD_APARTMENT { get; set; }
        public string? CARD_ADDRESS_OVERFLOW { get; set; }
        public string? CARD_METER_STATUS { get; set; }
        public string? CARD_METER_ID { get; set; }
        public string? CARD_ACCOUNT_NO { get; set; }

        public string? CARD_LAT { get; set; }
        public string? CARD_LONG1 { get; set; }
        public string? CARD_GAS_LEAK_SURVEY_DT { get; set; }
        public string? CARD_SUBDIVISION_DESC { get; set; }

    }
    public class AssgnEditorRequest
    {
        public string? Editorid { get; set; }
        public string? CARD_PREMISE_NO { get; set; }
        public string? CARD_SERV_ORDER_NO { get; set; }
        public string? STATEMENTTYPE { get; set; }
        public string? PROFILEID { get; set; }
        public int? RAWID { get; set; }

    }
    public class AssgnEditorResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
    }
    public class GetRawDataRequest
    {
        public int? CompanyID { get; set; }
        public int? rowcount { get; set; }
        public string? status { get; set; }
        public string? city { get; set; }
        public string? housenumber { get; set; }
        public string? street { get; set; }
        public int? year { get; set; }
        public int? wtype { get; set; }
        public string? userid { get; set; }
        public string? Card_status { get; set; }



    }
    public class GetRawDataResponse
    {
        public string? CARD_PREMISE_NO { get; set; }
        public string? CARD_SERV_ORDER_NO { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? STREET { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? ZIP_CODE { get; set; }
        public string? SUBDIVISION_DESC { get; set; }

        public string? CARD_URL_LINK { get; set; }
        public string? CITY { get; set; }
        public string? WORK_TYPE { get; set; }
        public string? CARD_STATUS { get; set; }
        public int? CARD_YEAR { get; set; }

        public string? FULLNAME { get; set; }
        public int? UNASSIGNEDCOUNT { get; set; }
        public string? CARD_DESCRIPTION { get; set; }
        public int? RAW_DATA_ID { get; set; }
        public string? CARD_URL_PR { get; set; }
    }
    public class GetRawDataRequestUser
    {
        public string? _UId { get; set; }
        public string? _CardStatus { get; set; }

        public string? _city { get; set; }
        public string? _housenumber { get; set; }
        public string? _street { get; set; }
        public int? _year { get; set; }
        public int? _worktype { get; set; }
        public string? _Premise { get; set; }
    }
    public class GetRawDataResponseUser
    {
        public string? CARD_PREMISE_NO { get; set; }
        public string? CARD_SERV_ORDER_NO { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? STREET { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? ZIP_CODE { get; set; }
        public string? SUBDIVISION_DESC { get; set; }
        public string? CARD_URL_LINK { get; set; }
        public string? CITY { get; set; }
        public string? WORK_TYPE { get; set; }
        public string? CARD_STATUS { get; set; }
        public DateTime? CARD_YEAR { get; set; }
        public string? CARD_DESCRIPTION { get; set; }
        public int? cardsid { get; set; }
        public string? SUPERREVIEW { get; set; }
        public string? SessionName { get; set; }
        public string? ISSUENAME { get; set; }
        public string? CARDURL { get; set; }
        public string? CARD_URL_PR { get; set; }
        public string? BILLED { get; set; }
        public int? CARD_COUNT { get; set; }
        public int? IMAGE_ID { get; set; }




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
    public class GetReportResponse
    {
        public DateTime? COMPLETED_DATE { get; set; }
        public string? PREMISE_NUMBER { get; set; }

        public string? SERV_ORDER_NO { get; set; }
        public string? CardUrl { get; set; }

        public string? STREET { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? WORK_TYPE { get; set; }
        public string? FULLNAME { get; set; }
        public string? CARD_DESCRIPTION { get; set; }
        public string? ISSUENAME { get; set; }
        public string? CITY { get; set; }

        public string? CARD_URL_PR { get; set; }


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
    public class AssgnEditorBucketResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
    }
    public class AssgnEditorbucketRequest
    {
        public string? EDITORID { get; set; }
        public string? PREMISE_NO { get; set; }
        public string? SERV_ORDER_NO { get; set; }
        public string? CARD_STATUS { get; set; }
        public int? ISSUEID { get; set; }
        public string? FeedbackText { get; set; }
        public string? SessionText { get; set; }
        public int? cardsid { get; set; }

    }
    public class UserCardCount_Request
    {
        public string? userid { get; set; }
    }
    public class UserCardCount_Response
    {
        public int? Cardcount { get; set; }

    }
    public class CardTransfer_Request
    {
        public string? FromUser { get; set; }
        public string? ToUser { get; set; }
        public string? ChangeBy { get; set; }
        public int? CardCount { get; set; }

    }
    public class CardTransfer_Response
    {
        public int? Code { get; set; }
        public string? message { get; set; }


    }
    public class PremiseDetails_Request
    {
        public string? premiseQuery { get; set; }

    }
    public class PremiseDetails_Response
    {
        public string? CARD_PREMISE_NO { get; set; }
        public string? CARD_SERV_ORDER_NO { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? STREET { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? ZIP_CODE { get; set; }
        public string? SUBDIVISION_DESC { get; set; }
        public string? CARD_URL_LINK { get; set; }
        public string? CITY { get; set; }
        public string? WORK_TYPE { get; set; }
        public string? CARD_STATUS { get; set; }
        public DateTime? CARD_YEAR { get; set; }
        public string? CARD_DESCRIPTION { get; set; }
        public int? cardsid { get; set; }
        public string? SessionName { get; set; }
        public string? SUPERREVIEW { get; set; }
        public string? ISSUENAME { get; set; }
        public String? CARDURL { get; set; }
        public string? EditorName { get; set; }
    }
    public class Batch_update_request
    {
        public int? BATCH_ID { get; set; }
        public string? BatchStatus { get; set; }

        public string? ProfileID { get; set; }
        public string? BatchComment { get; set; }


    }
    public class Batch_response
    {
        public int? BATCH_ID { get; set; }
        public string? BATCH_NAME { get; set; }
        public string? BATCH_STATUS { get; set; }
        public string? CARD_DESCRIPTION { get; set; }


        public DateTime? CREATED_DATE { get; set; }
        //public string NOTIFICATION_SENT_ON { get; set; }
        //public string APPROVED_ON { get; set; }
        //public string RETURNED_ON { get; set; }
        //public string REGENERATE_ON { get; set; }
        //public string BILLED_DATE { get; set; }
        public string? BILLED { get; set; }






    }
    public class Batch_request
    {
        public string? BatchStatus { get; set; }
        public string? profileid { get; set; }
    }
    public class Supervisor_delete_request
    {
        public int? CardId { get; set; }
        public String? DeletedBY { get; set; }

    }
    public class Batch_summary_response
    {
        public int? V_COMPLETED { get; set; }
        public int? V_VERIFIED { get; set; }
        public int? V_REVIEWED { get; set; }
        public int? V_RETURNED { get; set; }

        public int? V_FIXED { get; set; }


    }
    public class Batch_summary_request
    {
        public string? ProfileID { get; set; }
        public int? BatchID { get; set; }


    }
    public class Batch_process_request
    {
        public string? profile { get; set; }
        public int? ROWS_FOR_BATCH { get; set; }
        public int? YEAR { get; set; }


    }
    public class New_batch_cards_request
    {
        public string? profileid { get; set; }
    }
    public class New_batch_cards_response
    {
        public string? premise { get; set; }
        public string? SERV_ORDER_NO { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? STREET { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? city { get; set; }
        public string? ZIP_CODE { get; set; }

        public string? COMPLETED_DATE { get; set; }
        public string? card_Description { get; set; }
        public string? ISSUENAME { get; set; }
        public string? cardComment { get; set; }
    }
    public class Update_Supervisor_Request
    {
        public string? EditedBy { get; set; }
        public string? CardUrl { get; set; }
        public DateTime? SERVICE_LINE_INSTALL_DT { get; set; }
        public string? CITY { get; set; }
        public string? ZIP_CODE { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? ADDRESS_OVERFLOW { get; set; }
        public string? STREET { get; set; }
        public string? SUBDIVISION_DESC { get; set; }
        public string? PremiseNumber { get; set; }
        public string? ServiceOrder { get; set; }
        public int? CARDSID { get; set; }
    }
    public class cardTransferFetchDetailsRequest
    {
        public int? CardsID { get; set; }
    }
    public class cardTransferFetchDetailsResponse
    {

        public string? CardUrl { get; set; }
        public DateTime? SERVICE_LINE_INSTALL_DT { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? STREET { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? CITY { get; set; }
        public string? ZIP_CODE { get; set; }
        public string? SUBDIVISION_DESC { get; set; }
        public string? SERV_ORDER_NO { get; set; }
        public string? Premise { get; set; }
    }
    public class ExcelDetails_Response
    {
        public int? cardsid { get; set; }
        public string? CardUrl { get; set; }
        public string? Premise { get; set; }
        public string? SERV_ORDER_NO { get; set; }
        public DateTime? SERVICE_LINE_INSTALL_DT { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? STREET { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? CITY { get; set; }
        public string? ZIP_CODE { get; set; }
        public string? SUBDIVISION_DESC { get; set; }
        public string? CARD_DESCRIPTION { get; set; }
        public string? CARD_STATUS { get; set; }
        public string? BATCH_QA_BY { get; set; }
        public string? COMMENT { get; set; }
        public string? MAP_URL { get; set; }
        public string? FULL_MAP_URL { get; set; }
        public string? ISSUENAME { get; set; }
        public int? IMAGE_ID { get; set; }


    }
    public class ExcelDetails_Request
    {
        public int? BatchId { get; set; }
        public String? ProfileID { get; set; }
    }
    public class BatchDetails_for_QA_Request
    {
        public int? BatchId { get; set; }

    }
    public class singleCardTransferRequest
    {
        public int? CARDSID { get; set; }
        public string? ToUser { get; set; }
        public string? ChangeBy { get; set; }
    }
    public class singleCardTransferResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
    }
    public class supervisorAddCardResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
    }
    public class supervisorAddCardRequest
    {
        public DateTime? SERVICE_LINE_INSTALL_DT { get; set; }
        public string? Work_Type { get; set; }
        public string? Premise { get; set; }
        public string? WorkOrderNo { get; set; }
        public string? CardUrl { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? STREET { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? CITY { get; set; }
        public string? ZIP_CODE { get; set; }
        public string? SUBDIVISION_DESC { get; set; }
        public string? UpdatedBy { get; set; }

        public string? ChangeBY { get; set; }
    }
    public class cardDetailsRequest
    {
        public string? currentUserID { get; set; }
        public int? byqa { get; set; }
    }
    public class cardDetailsResponse
    {
        public int? CardsID { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public string? SessionName { get; set; }
        public string? Premise { get; set; }
        public int? State { get; set; }
        public string? CName { get; set; }
        public string? worktypename { get; set; }
        public bool? CheckedUGP { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedBY { get; set; }
        public string? desc { get; set; }
        public int? CID { get; set; }
        public string? cardUrl { get; set; }
        public string? FullName { get; set; }
        public string? superreview { get; set; }
        public string? IssueDesc { get; set; }
        public string? QCBYNAME { get; set; }
        public string? card_url_pr { get; set; }
        public string? serv_order_no { get; set; }


    }
    public class clientcardResponse
    {
        public int? CardsID { get; set; }
        public string? EntryDate { get; set; }
        public string? card_address { get; set; }
        public string? CName { get; set; }
        public int? card_state { get; set; }
        // public string Work_Type { get; set; }
        public string? worktypename { get; set; }
        public string? Premise { get; set; }
        public string? CardUrl { get; set; }
        public string? SERV_ORDER_NO { get; set; }
        public string? CardStatus { get; set; }
        public string? card_Description { get; set; }
        // public string CATEGORY_ID { get; set; }
        public string? ISSUENAME { get; set; }
        public string? CITY { get; set; }
        public string? ZIP_CODE { get; set; }
        public string? HOUSE_NO { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? ADDRESS_OVERFLOW { get; set; }
        public string? STREET { get; set; }
        public string? SUBDIVISION_DESC { get; set; }
        public string? COMPLETED_DATE { get; set; }
        public string? Comment { get; set; }


    }

    public class Batchdata_update_request
    {
        public string? PROFILE_ID { get; set; }
        public string? BATCH_CARDID { get; set; }
        public string? REMARK { get; set; }
        public string? CARDSTATUS { get; set; }

    }

    public class batchQACommentSubmitRequest
    {
        public int? batchID { get; set; }
        public string? remark { get; set; }
        public string? updatedBy { get; set; }
        public string? cardStatus { get; set; }
    }

    public class batchQACommentSubmitResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
    }

    public class batchAssignQARequest
    {
        public int? batchID { get; set; }
        public string? assignTo { get; set; }
    }

    public class batchAssignQAResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
    }
    public class GETINVOICE_DETAILS_REQUEST
    {
        public string? BATCHID { get; set; }
        public string? PROFILE { get; set; }

        public int? INVOICEID { get; set; }
    }



    public class GETINVOICE_PRINT_DETAILS_REQUEST
    {
        public string? REFERENCENUMBER { get; set; }
        public string? PROFILE { get; set; }
        public int? INVOICE_NUMBER { get; set; }
    }

    public class GETINVOICE_DETAILS_RESPONSE
    {

        public string? Batchname { get; set; }
        public int? Drawn { get; set; }
        public int? nodrawn { get; set; }



    }

    public class GET_CLIENT_BATCH_INVOICE_DATA
    {

        public ICollection<GETINVOICE_DETAILS_RESPONSE> GETINVOICE_DETAILS_s { get; set; } = new List<GETINVOICE_DETAILS_RESPONSE>();
        public ICollection<GETINVOICE_DATA_DETAILS_RESPONSE> GETINVOICE_DATA_s { get; set; } = new List<GETINVOICE_DATA_DETAILS_RESPONSE>();
    }


    public class GET_INVOICE_PRINT_DATA_RESPONSE
    {

        public ICollection<GETINVOICE_DETAILS_RESPONSE> GETINVOICE_DETAILS_s { get; set; } = new List<GETINVOICE_DETAILS_RESPONSE>();
        public GETINVOICE_PRINT_DETAIL_RESPONSE PRINT_DETAIL { get; set; } = new GETINVOICE_PRINT_DETAIL_RESPONSE();
    }

    public class GETINVOICE_DATA_DETAILS_RESPONSE
    {
        public string? PREMISE { get; set; }
        public string? SERV_ORDER_NO { get; set; }
        public string? CITY { get; set; }
        public string? STREET { get; set; }
        public string? STREET_SUFFIX { get; set; }
        public string? HOUSE_NO { get; set; }

        public string? ZIP_CODE { get; set; }
        public string? ISSUENAME { get; set; }
        public string? COMMENT { get; set; }

        public string? URL { get; set; }
        public string? ACTION { get; set; }
        public string? BATCH_NAME { get; set; }


    }

    public class GETINVOICE_PRINT_DETAIL_RESPONSE
    {

        public string? invoiceNumber { get; set; }
        public string? BillingAddress1 { get; set; }
        public string? BillingAddress2 { get; set; }
        public string? BillingAddress3 { get; set; }
        public string? BillingAddress4 { get; set; }

        public string? COMPANY { get; set; }
        public string? FROMDATE { get; set; }
        public string? TODATE { get; set; }
        public string? GENERATEDATE { get; set; }
        public string? REFERENCENUMBER { get; set; }
        public string? DrawnRate { get; set; }
        public string? NoNDrawnRate { get; set; }

        public string? PayAddress1 { get; set; }
        public string? PayAddress2 { get; set; }
        public string? PayAddress3 { get; set; }
        public string? ceo { get; set; }


    }
    public class LAST_REVIEWED_RESPONSE
    {
        public int? CARDSID { get; set; }
    }
    public class LAST_REVIEWED_REQUEST
    {
        public int? BATCHID { get; set; }
    }




    public class Store_image_Request
    {
        public int? ID { get; set; }
        public string? STORED_FILE_NAME { get; set; }
        public string? STORED_FILE_PATH { get; set; }
        public string? UDPATED_BY { get; set; }

    }

    public class Get_Store_image_Request
    {
        public int? Issue_id { get; set; }
    }




    public class quickReportResponse
    {
        public string? PNAME { get; set; }
        public string? PVALUE { get; set; }
    }
    public class fetchQuickReporParamtRequest
    {
        public int? companyid { get; set; }
        public int? roleid { get; set; }
        public string? userid { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? reportHead { get; set; }

    }
    public class BPEMDetailRequest
    {
        public int? cardsid { get; set; }


    }


    public class InvestigationReportRequest
    {
        public int? companyid { get; set; }
        public int? roleid { get; set; }
        public string? userid { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? House { get; set; }
        public string? toprows { get; set; }
        public string? CARDSTATUS { get; set; }
        public string? PREMISENUMBER { get; set; }
        public string? SERVICE { get; set; }

    }
    public class InvestigationReportResponse
    {
        public string? CARD_ADDRESS { get; set; }
        public string? serviceurl { get; set; }
        public string? premiseurl { get; set; }
        public string? PREMISE_NO { get; set; }
        public string? serv_order_no { get; set; }
        public string? WORK_TYPE { get; set; }
        public string? CARD_YEAR { get; set; }
        public string? fullname { get; set; }
        public string? card_Description { get; set; }
        public string? CardsID { get; set; }
        public string? Lastcomment { get; set; }


    }


    public class sendInvoiceResponse
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
    }


    public class commonCardCountsRequest
    {
        public string? profileID { get; set; }
    }

    public class commonCardCountsResponse
    {
        public int? todaysEditorCards { get; set; }
        public int? todayQACards { get; set; }
    }


    public class editorError
    {
        public int? errorID { get; set; }
        public string? errorName { get; set; }
    }

    public class fetchQuickReportRequest
    {
        public string? reportName { get; set; }
    }


    public class fetchDashboardDetailsReportRequest
    {
        public string? reportName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Profile { get; set; }


        public int? IssueID { get; set; }


    }
    public class fetchDashboardDetailsReportResponse
    {
        public string? premise { get; set; }
        public string? serviceorder { get; set; }
        public string? cardurl { get; set; }
        public string? address { get; set; }




    }



    public class insertEditorErrorRequest
    {
        public string? errorTypeName { get; set; }
        public string? errorSubtypeName { get; set; }
        public string? createdBy { get; set; }
        public string? subtypeStatus { get; set; }
        public string? errorTypeDesc { get; set; }
        public string? errorSubtypeDesc { get; set; }
        public string? requestType { get; set; }
        public int? subtypeEditID { get; set; }
        public int? companyID { get; set; }
    }


    public class fetchEditorErrorsForQARequest
    {
        public int? companyID { get; set; }
    }


    public class editorErrorListResponse
    {
        public int? errorID { get; set; }
        public int? subtypeID { get; set; }
        public string? errorName { get; set; }
        public string? subtypeName { get; set; }
        public string? errorDesc { get; set; }
        public string? subtypeDesc { get; set; }
        public string? subtypeStatus { get; set; }
        public int? companyID { get; set; }
    }



    public class subtypeArray
    {
        public string? subtypeName { get; set; }
        public int? subtypeID { get; set; }

    }


    public class errorNestedJson
    {
        public string? errorName { get; set; }
        public List<subtypeArray>? subtypeArray { get; set; }
    }


    public class showEditorErrorRequest
    {
        public int? cardid { get; set; }
    }



    public class qaPageEditorErrorRequest
    {
        public int? card_id { get; set; }
        public string? editorID { get; set; }
        public int[]? errorArray { get; set; }
        public string? txtRemark { get; set; }
        public string? QC_BY { get; set; }
        public string? CardStatus { get; set; }
        public string? LoggedInUser { get; set; }

    }



    public class systemErrorDropdownList
    {
        public int? ISSUE_ID { get; set; }
        public string? ISSUE_NAME { get; set; }

    }
    public class taskListResponse
    {
        public int? TASKID { get; set; }
        public string? TASKTEXT { get; set; }

    }
    public class ppnmResponse
    {
        public int? PPNM_ID { get; set; }
        public string? plan_name { get; set; }
    }
    public class taskDropdownList
    {
        public int? TASKID { get; set; }
        public string? TASKTEXT { get; set; }


    }

    public class caseDropdownList
    {
        public int? TASKID { get; set; }
        public string? TASKTEXT { get; set; }
        public string? TASKCOUNT { get; set; }

    }

    public class masterSetupList
    {
        public string? tablename { get; set; }
    }
    public class caseRecordRequest
    {
        public int? Type { get; set; }
        public string? Fromdate { get; set; }
        public string? Todate { get; set; }

    }
    public class caseRecordResponse
    {
        public int? RECORD_TASK_ID { get; set; }
        public int? TASK_ID { get; set; }
        public int? RECORD_ID { get; set; }
        public int? RECORD_COUNT { get; set; }
        public string? RECORD_ADDITION_DATE { get; set; }
        //   public string CREATION_DATE { get; set; }

    }
    public class executionTestResponseById
    {
        public int? id { get; set; }
        public DateTime? entry_date { get; set; }
        public int? project_name { get; set; }
        public int? task_id { get; set; }
        public string? test_plan_name { get; set; }

        public int? total_num_steps { get; set; }
        public int? num_steps_executed { get; set; }
        public int? num_defects_logged { get; set; }
        public int? num_sev3_defects { get; set; }
        public int? num_sev2_defects { get; set; }
        public int? num_sev1_defects { get; set; }
        public decimal? time_spent_in_hours { get; set; }
        public string? created_by { get; set; }
        public int? bid { get; set; }
        public int? StatusID { get; set; }
        public string? remark { get; set; }

    }
    public class executionTestResponse
    {
        public int? Et_Id { get; set; }

        public string? Entry_Date { get; set; }
        public string? Project_Name { get; set; }
        public int? Task_Id { get; set; }
        public string? Task_Text { get; set; }
        public string? Test_Plan_Name { get; set; }
        public int? Total_Num_Steps { get; set; }
        public int? Num_Steps_Executed { get; set; }
        public int? Num_Defects_Logged { get; set; }
        public int? Num_Sev1_Defects { get; set; }
        public int? Num_Sev2_Defects { get; set; }
        public int? Num_Sev3_Defects { get; set; }
        public decimal? Time_Spent_In_Hours { get; set; }
        public string? Remark { get; set; }
        public string? created_by { get; set; }
        public string? BusinessUnit { get; set; }
        public int? STATE_ID { get; set; }

        public int? StatusID { get; set; }
        public string? StatusName { get; set; }


    }
    public class testScriptingResponseById
    {
        public int? TS_ID { get; set; }
        public DateTime? TS_DATE { get; set; }
        public string? PROJECTNAME { get; set; }
        public string? TASK_ID { get; set; }
        public string? TESTPLANNAME { get; set; }
        public int? NUM_STEPS_ADDED_REVIEWED { get; set; }
        public int? NUM_CONFIG_ADDED_REVIEWED { get; set; }
        public decimal? TIME_SPENT_IN_HOURS { get; set; }
        public string? REMARK { get; set; }
        public int? STATE_ID { get; set; }
        public int? TEST_CASE_STATUSID { get; set; }
    }
    public class testScriptingResponse
    {
        public int? TS_ID { get; set; }
        public string? TS_DATE { get; set; }
        public string? PROJECTNAME { get; set; }
        public string? TASK_ID { get; set; }
        public string? TESTPLANNAME { get; set; }
        public int? NUM_STEPS_ADDED_REVIEWED { get; set; }
        public int? NUM_CONFIG_ADDED_REVIEWED { get; set; }
        public decimal? TIME_SPENT_IN_HOURS { get; set; }
        public string? REMARK { get; set; }
        public string? CREATED_BY { get; set; }
        public string? BusinessUnit { get; set; }
        public int? STATE_ID { get; set; }
        public int? StatusID { get; set; }
        public string? StatusName { get; set; }

    }
    public class workTypeResponse
    {
        public int? worktypevalue { get; set; }
        public string? worktypename { get; set; }
        public int? TASKID { get; set; }
        public string? WORKTYPEDESCRIPTION { get; set; }
        public int? slnumber { get; set; }


    }

    public class systemIssueStatusrDropdownList
    {
        public int? SYID { get; set; }
        public string? SName { get; set; }

    }

    public class TaskTypeInsertUpdateRequest
    {
        public string? TASKTEXT { get; set; }
        public string? STATEMENT { get; set; }
        public int? TID { get; set; }

    }

    public class PpnmIppnmInsertUpdateRequest
    {
        public int? ppnm_id { get; set; }
        public string? plan_name { get; set; }
        public string? statement { get; set; }



    }
    public class ExecutionTestInsertUpdateRequest
    {
        public string? entry_date { get; set; }
        public string? statement { get; set; }
        public int? task_id { get; set; }
        public int? et_id { get; set; }
        public string? project_name { get; set; }
        public string? test_plan_name { get; set; }
        public int? total_num_steps { get; set; }
        public int? num_steps_executed { get; set; }
        public int? num_defects_logged { get; set; }
        public int? num_sev1_defects { get; set; }
        public int? num_sev2_defects { get; set; }
        public int? num_sev3_defects { get; set; }
        public int? Bid { get; set; }
        public decimal? time_spent_in_hours { get; set; }
        public string? remark { get; set; }
        public string? created_by { get; set; }
        public int? Test_case_statusID { get; set; }

    }

    public class WorkTypeInsertUpdateRequest
    {
        public string? worktypename { get; set; }
        public string? WORKTYPEDESCRIPTION { get; set; }
        public string? STATEMENT { get; set; }
        public int? TASKID { get; set; }
        public int? WTV { get; set; }

    }

    public class CaseRecordInsertUpdateRequest
    {
        public int? record_task_id { get; set; }
        public int? record_id { get; set; }
        public int? task_id { get; set; }
        public int? record_count { get; set; }
        public string? statement { get; set; }

        public string? record_addition_date { get; set; }

        public int? rtaskid { get; set; }
    }
    public class ppnmInsertUpdateRequest
    {
        public int? PPNM_ID { get; set; }
        public string? PLAN_NAME { get; set; }
        public string? STATEMENT { get; set; }


    }
    public class testScriptingRequest
    {
        public string? created_by { get; set; }
        public int? PROJECTNAME { get; set; }
        public int? TASK_ID { get; set; }
        public string? TESTPLANNAME { get; set; }
        public int? NUM_STEPS_ADDED_REVIEWED { get; set; }
        public int? NUM_CONFIG_ADDED_REVIEWED { get; set; }
        public int? TIME_SPENT_IN_HOURS { get; set; }
        public string? REMARK { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public int? status { get; set; }
        public int? businessUnit { get; set; }
        public string? userSelection { get; set; }

    }

    public class executionTestRequestForSearch
    {
        public string? created_by { get; set; }
        public int? project_name { get; set; }
        public int? task_id { get; set; }
        public int? Test_case_statusID { get; set; }
        public int? time_spent_in_hours { get; set; }
        public string? remark { get; set; }
        public int? Bid { get; set; }
        public int? num_sev3_defects { get; set; }
        public int? num_sev2_defects { get; set; }
        public int? num_sev1_defects { get; set; }
        public int? num_defects_logged { get; set; }
        public int? num_steps_executed { get; set; }
        public int? total_num_steps { get; set; }
        public string? test_plan_name { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public string? userSearch { get; set; }
    }
    //public class getExecutionById
    //{
    //    public int execId { get; set; }
    //}
    public class executionTestRequest
    {
        public string? created_by { get; set; }
    }

    public class TestScriptingInsertUpdateRequest
    {
        public int? TS_ID { get; set; }
        public string? TS_DATE { get; set; }
        public string? PROJECTNAME { get; set; }
        public int? TASK_ID { get; set; }
        public string? TESTPLANNAME { get; set; }
        public int? NUM_STEPS_ADDED_REVIEWED { get; set; }
        public int? NUM_CONFIG_ADDED_REVIEWED { get; set; }
        public string? STATEMENT { get; set; }
        public string? REMARK { get; set; }
        public decimal? TIME_SPENT_IN_HOURS { get; set; }
        public string? created_by { get; set; }
        public int? BID { get; set; }
        public int? Test_case_statusID { get; set; }


    }
    public class systemIssueInformation
    {
        public int? IssueId { get; set; }
        public string? UserEmail { get; set; }
        public string? IssueRegisterDate { get; set; }
        public string? IssueDescription { get; set; }
        public string? IssuePriority { get; set; }
        public string? IssueType { get; set; }
        public string? UserCreated { get; set; }
        public string? CompanyName { get; set; }
        public string? IssueStatus { get; set; }
        public string? FirstName { get; set; }
    }
    public class SystemErrorInsertRequest
    {
        public int? issueID { get; set; }
        public int? IssueType { get; set; }
        public string? status { get; set; }
        public string? IssueRaisedBy { get; set; }
        public int? Priority { get; set; }
        public int? CompanyId { get; set; }
        public string? IssueDesc { get; set; }
        public string? AssignTo { get; set; }
        public string? operation { get; set; }
    }

    public class getSystemIssueGridRequest
    {
        public string? UserId { get; set; }
    }
    public class getSystemIssueGridResponse
    {

        public int? ISSUE_TYPE { get; set; }
        public int? ISSUE_ID { get; set; }
        public string? ISSUE_RAISED_BY { get; set; }
        public String? ISSUE_NAME { get; set; }
        public string? PRIORITY_ISSUE { get; set; }
        public int? COMPANY { get; set; }
        public string? SYSTEM_STATUS { get; set; }
        public string? SName { get; set; }
        public string? IssueRegisterdate { get; set; }
        public string? ISSUE_DESC { get; set; }
        public string? ISSUE_ASSIGN_TO { get; set; }
        public string? FullName { get; set; }
        public string? CName { get; set; }
        public string? firstName { get; set; }
        public int? slnumber { get; set; }
        public int? countOfPicures { get; set; }
    }

    public class updateCardIssueByQARequest
    {
        public int? cardId { get; set; }
        public int? issueId { get; set; }
        public string? updatedBy { get; set; }
    }

    public class dash
    {
        public int? cardId { get; set; }
        public int? issueId { get; set; }
        public string? updatedBy { get; set; }
    }

    //public class CasesTotalsResponse
    //{
    //    public int? TodayTotal { get; set; }
    //    public int? WeekTotal { get; set; }
    //}
    //public class CountDataRequest
    //{
    //    public string PageName { get; set; }
    //    public string User { get; set; }
    //    public DateTime? FromDate { get; set; }
    //    public DateTime? ToDate { get; set; }
    //}

    public class CountDataRequest
    {
        public string? User { get; set; }
        public string? SearchUser { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? TaskId { get; set; }
        public int? WorkId { get; set; }
        public string? StatusId { get; set; }
    }

    public class CasesTotalsResponse
    {
        public DateTime? TodayDate { get; set; }
        public DateTime? StartingWeekDate { get; set; }
        public DateTime? EndingWeekDate { get; set; }
        public int? TodayCount { get; set; }
        public int? WeeklyCount { get; set; }
    }
    public class QaNumbersRequest
    {
        public string? SelfUser { get; set; }
        public string? User { get; set; }
        public string? QaUser { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? TaskId { get; set; }
        public string? WorkId { get; set; }
        public string? InstallationNum { get; set; }
    }
    public class Common
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
    public class Store_image_Response : Common
    {


    }
    public class Get_Store_image_Response : Common
    {
        public int CARDSID { get; set; }
        public string STORED_FILE_NAME { get; set; }
        public string STORED_FILE_PATH { get; set; }

    }
}
